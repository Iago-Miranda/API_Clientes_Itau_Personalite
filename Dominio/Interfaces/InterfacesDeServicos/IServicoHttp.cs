using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfacesDeServicos
{
    public interface IServicoHttp
    {
        public Task<GerenteDto> BuscaUsuarioNaApi(string gerenteId, string authToken, string enderecoApi, string endpointApi);

        public Task<IEnumerable<GerenteDto>> BuscaUsuariosNaApi(string authToken, string enderecoApi, string endpointApi);
    }
}
