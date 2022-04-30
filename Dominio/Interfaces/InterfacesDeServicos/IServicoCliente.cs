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
        public Task<bool> Adicionar(Cliente cliente, string authToken);

        public Task<bool> Editar(Cliente cliente, string authToken);

        public Task<List<ClienteDto>> ListarTodosClientes(string authToken);

        public Task<ClienteDto> BuscarClientePorId(int id, string authToken);

        public Task<List<ClienteDto>> BuscarClientesPorGerenteId(string gerenteId, string authToken);
    }
}
