﻿using Aplicacao.Models;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoCliente
    {
        public Task<dynamic> Adicionar(Cliente cliente, string authToken);

        public Task<dynamic> Editar(Cliente cliente, string authToken);

        public Task<List<ClienteUi>> ListarTodosClientes(string authToken);

        public Task<List<ClienteUi>> BuscarClientesPorGerenteId(string gerenteId, string authToken);
    }
}
