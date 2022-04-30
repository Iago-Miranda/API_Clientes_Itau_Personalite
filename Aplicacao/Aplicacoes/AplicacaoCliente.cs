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
        private readonly ICliente _ICliente; 

        public AplicacaoCliente(IServicoCliente IServicoCliente, ICliente ICliente)
        {
            _IServicoCliente = IServicoCliente;
            _ICliente = ICliente;
        }

        public async Task<dynamic> Adicionar(Cliente cliente, string authToken)
        {
            ValidadorCliente validador = new ValidadorCliente();

            var resultadoValidacao = validador.Validate(cliente);

            if (!resultadoValidacao.IsValid)
            {
                return resultadoValidacao.Errors;
            }

            if (!(await _IServicoCliente.GerenteExiste(cliente.GerenteId, authToken)))
                return new ClienteUi();

            await _ICliente.Adicionar(cliente);

            if (cliente.Id == 0)
                return new ClienteUi();

            return await BuscarClientePorId(cliente.Id, authToken);
        }

        public async Task<dynamic> Editar(Cliente cliente, string authToken)
        {
            var resultadoValidacao = await ClienteValido(cliente, authToken);

            if (!resultadoValidacao.Item1)
                return resultadoValidacao.Item2;

            await _ICliente.Adicionar(cliente);

            if (cliente.Id == 0)
                return new ClienteUi();

            return BuscarClientePorId(cliente.Id, authToken);
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

        private async Task<Tuple<bool,dynamic>> ClienteValido(Cliente cliente, string authToken)
        {
            ValidadorCliente validador = new ValidadorCliente();

            var resultadoValidacao = validador.Validate(cliente);

            if (!resultadoValidacao.IsValid)
            {
                return new Tuple<bool, dynamic>(false,resultadoValidacao.Errors);
            }

            if (!(await _IServicoCliente.GerenteExiste(cliente.GerenteId, authToken)))
                return new Tuple<bool, dynamic>(false, "Gerente não Existe na base de dados de usuários");
            else
                return new Tuple<bool, dynamic>(true, null);
        }

        private async Task<ClienteUi> BuscarClientePorId(int id, string authToken)
        {
            var clienteDto = await _IServicoCliente.BuscarClientePorId(id, authToken);

            Mapper mapper = Mapeadores.ObterMapeadorClienteDtoClienteUi();

            return mapper.Map<ClienteUi>(clienteDto);
        }
    }
}
