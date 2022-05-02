using Dominio.Interfaces.InterfacesDeServicos;
using Dominio.Models;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoHttp : IServicoHttp
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServicoHttp(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GerenteDto> BuscaUsuarioNaApi(string gerenteId, string authToken, string enderecoApi, string endpointApi)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $@"{enderecoApi}/{endpointApi}/{gerenteId}"
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
                GerenteDto gerenteDto = await JsonSerializer.DeserializeAsync
                                                            <GerenteDto>(conteudo, opcoesDesserializacao);

                return gerenteDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<GerenteDto>> BuscaUsuariosNaApi(string authToken, string enderecoApi, string endpointApi)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $@"{enderecoApi}/{endpointApi}"
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
