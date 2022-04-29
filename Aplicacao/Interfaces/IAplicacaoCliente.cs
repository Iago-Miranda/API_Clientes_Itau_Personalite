using Aplicacao.Interfaces.Genericos;
using Aplicacao.Models;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoCliente: IAplicacaoGenerica<Cliente>
    {
        public Task<bool> VerificaUsuarioExiste(Expression<Func<Usuario, bool>> exUsuario);
        public Task<UsuarioUI> BuscarUsuarioUIPorId(Guid id);
        public Task<List<UsuarioUI>> ListarClientesPorGerenteId();
    }
}
