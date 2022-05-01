using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public string GerenteId { get; set; }

        public Endereco Endereco { get; set; }

        public int EnderecoId { get; set; }

        [MaxLength(256)]
        public string Nome { get; set; }

        [Column(TypeName = "Money")]
        public decimal LimiteCredito { get; set; }
    }
}
