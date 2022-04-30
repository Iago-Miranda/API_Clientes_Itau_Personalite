using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioCliente : RepositorioGenerico<Cliente>, ICliente
    {
        private readonly ContextoClientesPersonalite _banco;

        public RepositorioCliente(ContextoClientesPersonalite banco) : base(banco)
        {
            _banco = banco;
        }

        public async Task<List<Cliente>> RetornaListaClientes(Expression<Func<Cliente, bool>> exCliente)
        {
            return await _banco.Clientes.Where(exCliente).AsNoTracking().ToListAsync();
        }
    }
}
