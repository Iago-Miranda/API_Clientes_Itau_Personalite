using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfacesDeServicos
{
    interface IServicoClientes
    {
        public Task<List<Cliente>> ListarClientesPorGerenteId(string gerenteId);
    }
}
