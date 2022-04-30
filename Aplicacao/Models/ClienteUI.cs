using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aplicacao.Models
{
    public class ClienteUi
    {
        public int Id { get; set; }
        public GerenteUi Gerente { get; set; }

        public EnderecoUi Endereco { get; set; }

        public string Nome { get; set; }

        public decimal LimiteCredito { get; set; }
    }
}
