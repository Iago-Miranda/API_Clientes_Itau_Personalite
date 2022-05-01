namespace Dominio.Models
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public GerenteDto Gerente { get; set; }

        public EnderecoDto Endereco { get; set; }

        public string Nome { get; set; }

        public decimal LimiteCredito { get; set; }
    }
}
