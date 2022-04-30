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
            var teste = await _IServicoCliente.BuscarClientesPorGerenteId("150A29CA-BAB6-42B4-0DCB-08DA2A3E1BA7","teste");

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
