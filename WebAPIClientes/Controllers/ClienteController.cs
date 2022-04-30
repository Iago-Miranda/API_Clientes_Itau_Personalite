using Aplicacao.Interfaces;
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
    }
}
