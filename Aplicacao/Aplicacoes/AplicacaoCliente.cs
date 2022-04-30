using Aplicacao.Interfaces;
using Aplicacao.Models;
using AutoMapper;
using Dominio.Interfaces.InterfacesDeServicos;
using Dominio.Models;
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

        public Task Editar(Cliente Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cliente>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClienteUi>> ListarTodosClientes(string authToken)
        {
            var clientesDto = await _IServicoCliente.ListarTodosClientes(authToken);

            Mapper mapper = Mapeadores.ObterMapeadorClienteDtoClienteUi();

            return mapper.Map<List<ClienteUi>>(clientesDto);
        }

        public async Task<List<ClienteUi>> BuscarClientesPorGerenteId(string gerenteId, string authToken)
        {
            var clientesDto = await _IServicoCliente.BuscarClientesPorGerenteId(gerenteId,authToken);

            Mapper mapper = Mapeadores.ObterMapeadorClienteDtoClienteUi();

            return mapper.Map<List<ClienteUi>>(clientesDto);
        }

    }
}
