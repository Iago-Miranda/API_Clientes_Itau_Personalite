using Aplicacao.Interfaces;
using Aplicacao.Models;
using Dominio.Interfaces.InterfacesDeServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoCliente : IAplicacaoCliente
    {
        private readonly IServicoCliente _IServicoCliente;

        public AplicacaoCliente(IServicoCliente IServicoCliente)
        {
            _IServicoCliente = IServicoCliente;
        }

        public Task Adicionar(Cliente Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClienteUi>> BuscarClientesPorGerenteId(string gerenteId)
        {
            var teste = await _IServicoCliente.ListarTodosClientes("teste");

            throw new NotImplementedException();
        }

        public Task Editar(Cliente Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cliente>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public Task<List<ClienteUi>> ListarTodosClientes()
        {
            throw new NotImplementedException();
        }
    }
}
