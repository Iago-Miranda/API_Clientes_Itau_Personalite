using Dominio.Interfaces;
using Dominio.Interfaces.InterfacesDeServicos;
using Dominio.Models;
using Entidades.Entidades;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoCliente : IServicoCliente
    {
        private readonly ICliente _ICliente;
        private readonly IHttpClientFactory _httpClientFactory;

        public ServicoCliente(ICliente ICliente, IHttpClientFactory httpClientFactory)
        {
            _ICliente = ICliente;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ClienteDto>> BuscarClientesPorGerenteId(string gerenteId)
        {
            var teste = await BuscaUsuarioNaApi("150A29CA-BAB6-42B4-0DCB-08DA2A3E1BA7");

            throw new NotImplementedException();
        }        

        public Task<List<ClienteDto>> ListarTodosClientes()
        {
            throw new NotImplementedException();
        }

        private async Task<Stream> BuscaUsuarioNaApi(string gerenteId)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches")
            {
                Headers =
            {
                { HeaderNames.Accept, "application/vnd.github.v3+json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsStreamAsync();
            }
            else
            {
                return Stream.Null;
            }
        }
    }
}
