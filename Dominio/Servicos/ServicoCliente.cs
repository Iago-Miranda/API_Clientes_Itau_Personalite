using AutoMapper;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfacesDeServicos;
using Dominio.Models;
using Entidades.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoCliente : IServicoCliente
    {
        private readonly ICliente _ICliente;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _IConfiguration;

        public ServicoCliente(ICliente ICliente, IHttpClientFactory httpClientFactory, IConfiguration IConfiguration)
        {
            _ICliente = ICliente;
            _httpClientFactory = httpClientFactory;
            _IConfiguration = IConfiguration;
        }

        public async Task<List<ClienteDto>> BuscarClientesPorGerenteId(string gerenteId, string authToken)
        {
            var gerente = await BuscaUsuarioNaApi(gerenteId, authToken);

            if (gerente is null)
                return new List<ClienteDto>();

            var listaClientes = await _ICliente.RetornaListaClientes(cliente => cliente.GerenteId == gerenteId);

            return Mapeadores.MapeiaClientesMesmoGerenteparaDto(gerente, listaClientes);
        }

        public async Task<List<ClienteDto>> ListarTodosClientes(string authToken)
        {
            var gerentes = await BuscaUsuariosNaApi(authToken);

            var listaClientes = await _ICliente.ListarTodos();

            var listaClientesDto = new List<ClienteDto>();

            foreach (var cliente in listaClientes)
            {
                var gerenteResponsavel = gerentes.FirstOrDefault(gerente => gerente.Id.ToUpper() == cliente.GerenteId.ToUpper());

                if(!(gerenteResponsavel is null))
                    listaClientesDto.Add(Mapeadores.MapeiaClienteEGerenteparaDto(gerenteResponsavel, cliente));
            }

            return listaClientesDto;
        }

        public async Task<bool> GerenteExiste(string gerenteId, string authToken)
        {
            return await BuscaUsuarioNaApi(gerenteId, authToken) != null;
        }

        private async Task<GerenteDto> BuscaUsuarioNaApi(string gerenteId, string authToken)
        {
            var enderecoApiUsuarios = _IConfiguration.GetSection("DominioConfig:EnderecoApiUsuarios").Value;

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $@"{enderecoApiUsuarios}/Usuario/{gerenteId}"
                )
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.Authorization, authToken }
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var conteudo =  await httpResponseMessage.Content.ReadAsStreamAsync();

                var opcoesDesserializacao = new JsonSerializerOptions();
                opcoesDesserializacao.PropertyNameCaseInsensitive = true;
                GerenteDto gerenteDto = await JsonSerializer.DeserializeAsync
                                                            <GerenteDto>(conteudo, opcoesDesserializacao);

                return gerenteDto;
            }
            else
            {
                return null;
            }
        }

        private async Task<IEnumerable<GerenteDto>> BuscaUsuariosNaApi(string authToken)
        {
            var enderecoApiUsuarios = _IConfiguration.GetSection("DominioConfig:EnderecoApiUsuarios").Value;

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $@"{enderecoApiUsuarios}/Usuario"
                )
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.Authorization, authToken }
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await httpResponseMessage.Content.ReadAsStreamAsync();

                var opcoesDesserializacao = new JsonSerializerOptions();
                opcoesDesserializacao.PropertyNameCaseInsensitive = true;
                IEnumerable<GerenteDto> gerentesDto = await JsonSerializer.DeserializeAsync
                                                            <IEnumerable<GerenteDto>>(conteudo, opcoesDesserializacao);

                return gerentesDto;
            }
            else
            {
                return null;
            }
        }
    }
}
