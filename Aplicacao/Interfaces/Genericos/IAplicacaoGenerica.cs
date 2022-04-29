using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces.Genericos
{
    public interface IAplicacaoGenerica <T> where T :  class
    {
        public Task Adicionar(T Objeto);

        public Task Editar(T Objeto);

        public Task<List<T>> ListarTodos();
    }
}
