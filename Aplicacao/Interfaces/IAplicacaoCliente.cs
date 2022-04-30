using Aplicacao.Interfaces.Genericos;
using Aplicacao.Models;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoCliente: IAplicacaoGenerica<Cliente>
    {
        public Task<List<ClienteUi>> ListarTodosClientes();
        public Task<List<ClienteUi>> BuscarClientesPorGerenteId(string gerenteId);
    }
}
