using Aplicacao.Interfaces;
using Aplicacao.Models;
using Aplicacao.Validadores;
using Entidades.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIClientes.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IAplicacaoCliente _AplicacaoCliente;

        public ClienteController(IAplicacaoCliente AplicacaoCliente)
        {
            _AplicacaoCliente = AplicacaoCliente;
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cabecalhos = Request.Headers;

            var authToken = StringValues.Empty;

            if(cabecalhos.ContainsKey("Authorization"))
            { 
                cabecalhos.TryGetValue("Authorization",out authToken);
            }

            if (authToken == StringValues.Empty)
            {
                return Unauthorized();
            }
            else
            {
                var clientes = await _AplicacaoCliente.ListarTodosClientes(authToken);

                return Ok(clientes);
            }
        }

        [Produces("application/json")]
        [HttpGet("{gerenteId}")]
        public async Task<IActionResult> BuscaClientesPorGerenteId([FromRoute] string gerenteId)
        {
            var cabecalhos = Request.Headers;

            var authToken = StringValues.Empty;

            if (cabecalhos.ContainsKey("Authorization"))
            {
                cabecalhos.TryGetValue("Authorization", out authToken);
            }

            if (authToken == StringValues.Empty)
            {
                return Unauthorized();
            }
            else
            {
                var clientes = await _AplicacaoCliente.BuscarClientesPorGerenteId(gerenteId, authToken);

                return Ok(clientes);
            }
        }

        [Produces("application/json")]
        [HttpPost]
        public async Task<IActionResult> AdicionarNovoCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            ValidadorCliente validador = new ValidadorCliente();

            var resultadoValidacao = validador.Validate(cliente);

            if (!resultadoValidacao.IsValid || cliente.Id != 0)
            {
                return BadRequest(resultadoValidacao.Errors);
            }

            var cabecalhos = Request.Headers;

            var authToken = StringValues.Empty;

            if (cabecalhos.ContainsKey("Authorization"))
            {
                cabecalhos.TryGetValue("Authorization", out authToken);
            }

            if (authToken == StringValues.Empty)
            {
                return Unauthorized();
            }
            else
            {
                var clienteAdicionado = await _AplicacaoCliente.Adicionar(cliente,authToken);

                if (clienteAdicionado.Id == 0)
                    return BadRequest();

                return Ok(clienteAdicionado);
            }
        }

        [Produces("application/json")]
        [HttpPut]
        public async Task<IActionResult> EditarCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            ValidadorCliente validador = new ValidadorCliente();

            var resultadoValidacao = validador.Validate(cliente);

            if (!resultadoValidacao.IsValid)
            {
                return BadRequest(resultadoValidacao.Errors);
            }

            var cabecalhos = Request.Headers;

            var authToken = StringValues.Empty;

            if (cabecalhos.ContainsKey("Authorization"))
            {
                cabecalhos.TryGetValue("Authorization", out authToken);
            }

            if (authToken == StringValues.Empty)
            {
                return Unauthorized();
            }
            else
            {
                var clienteEditado = await _AplicacaoCliente.Editar(cliente, authToken);

                return Ok(clienteEditado);
            }
        }
    }
}
