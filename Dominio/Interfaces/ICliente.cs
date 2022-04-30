using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ICliente : IGenericos<Cliente>
    {
        public Task<List<Cliente>> RetornaListaClientes(Expression<Func<Cliente, bool>> exCliente);
    }
}
