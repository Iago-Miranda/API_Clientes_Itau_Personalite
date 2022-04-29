using Dominio.Interfaces;
using Dominio.Interfaces.InterfacesDeServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    class ServicoClientes : IServicoClientes
    {
        private readonly ICliente _ICliente;

        public ServicoClientes(ICliente ICliente)
        {
            _ICliente = ICliente;
        }

        public async Task<List<Cliente>> ListarClientesPorGerenteId(string gerenteId)
        {
            var listaClientes = await _ICliente.ListarTodos();

            return listaClientes.Where(cliente => cliente.GerenteId == gerenteId).ToList();
        }
    }
}
