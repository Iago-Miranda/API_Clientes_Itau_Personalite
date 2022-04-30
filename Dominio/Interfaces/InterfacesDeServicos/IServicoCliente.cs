using Dominio.Models;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfacesDeServicos
{
    public interface IServicoCliente
    {
        public Task<List<ClienteDto>> ListarTodosClientes();
        public Task<List<ClienteDto>> BuscarClientesPorGerenteId(string gerenteId);
    }
}
