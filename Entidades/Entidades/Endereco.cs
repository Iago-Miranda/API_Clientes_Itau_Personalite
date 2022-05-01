using System.ComponentModel.DataAnnotations;

namespace Entidades.Entidades
{
    public class Endereco
    {
        public int Id { get; set; }

        [MaxLength(512)]
        public string Rua { get; set; }

        public int Numero { get; set; }

        [MaxLength(512)]
        public string Bairro { get; set; }

        [MaxLength(512)]
        public string Cidade { get; set; }

        [MaxLength(9)]
        public string CEP { get; set; }

        [MaxLength(64)]
        public string Estado { get; set; }
    }
}
