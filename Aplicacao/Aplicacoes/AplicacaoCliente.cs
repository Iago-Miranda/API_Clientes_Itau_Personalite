using Aplicacao.Interfaces;
using Aplicacao.Models;
using Aplicacao.Validadores;
using AutoMapper;
using Dominio.Interfaces;
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

        public AplicacaoCliente(IServicoCliente IServicoCliente, ICliente ICliente)
        {
            _IServicoCliente = IServicoCliente;
        }

        public async Task<ClienteUi> Adicionar(Cliente cliente, string authToken)
        {          
            await _IServicoCliente.Adicionar(cliente, authToken);

            if (cliente.Id == 0)
                return new ClienteUi();

            return await BuscarClientePorId(cliente.Id, authToken);
        }

        public async Task<ClienteUi> Editar(Cliente cliente, string authToken)
        {
            await _IServicoCliente.Editar(cliente, authToken);

            return await BuscarClientePorId(cliente.Id, authToken);
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

        private async Task<ClienteUi> BuscarClientePorId(int id, string authToken)
        {
            var clienteDto = await _IServicoCliente.BuscarClientePorId(id, authToken);

            Mapper mapper = Mapeadores.ObterMapeadorClienteDtoClienteUi();

            return mapper.Map<ClienteUi>(clienteDto);
        }
    }
}
