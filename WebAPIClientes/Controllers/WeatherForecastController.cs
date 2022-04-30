using Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IAplicacaoCliente _AplicacaoCliente;

        public WeatherForecastController(IAplicacaoCliente AplicacaoCliente)
        {
            _AplicacaoCliente = AplicacaoCliente;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teste = await _AplicacaoCliente.BuscarClientesPorGerenteId("teste");

            return Ok();
        }
    }
}
