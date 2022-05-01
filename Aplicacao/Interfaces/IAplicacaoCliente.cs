using Aplicacao.Models;
using Entidades.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoCliente
    {
        public Task<ClienteUi> Adicionar(Cliente cliente, string authToken);

        public Task<ClienteUi> Editar(Cliente cliente, string authToken);

        public Task<List<ClienteUi>> ListarTodosClientes(string authToken);

        public Task<List<ClienteUi>> BuscarClientesPorGerenteId(string gerenteId, string authToken);
    }
}
