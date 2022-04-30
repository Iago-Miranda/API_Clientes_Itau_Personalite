using Dominio.Interfaces.Genericos;
using Infraestrutura.Configuracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio.Genericos
{
    public class RepositorioGenerico<T> : IGenericos<T>, IDisposable where T : class
    {
        private readonly ContextoClientesPersonalite _banco; 

        public RepositorioGenerico(ContextoClientesPersonalite banco)
        {
            _banco = banco;
        }          

        public async Task Adicionar(T Objeto)
        {
            await _banco.Set<T>().AddAsync(Objeto);
            await _banco.SaveChangesAsync();
        }

        public async Task Editar(T Objeto)
        {
            _banco.Set<T>().Update(Objeto);
            await _banco.SaveChangesAsync();
        }

        public async Task<T> BuscarPorId(int id)
        {
            return await _banco.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ListarTodos()
        {
            return await _banco.Set<T>().AsNoTracking().ToListAsync();
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion
    }
}
