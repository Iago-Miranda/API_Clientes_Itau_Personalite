using Entidades.Entidades;
using FluentValidation;

namespace Entidades.Validadores
{
    public class ValidadorCliente : AbstractValidator<Cliente>
    {
        public ValidadorCliente()
        {
            RuleFor(cliente => cliente.Nome)
                .NotNull().WithMessage("O nome do cliente é obrigatório!")
                .MinimumLength(3).WithMessage("O campo nome deve conter ao menos 3 caracteres!");

            RuleFor(cliente => cliente.LimiteCredito)
                .NotNull().WithMessage("O limite de crédito é obrigatório!");

            RuleFor(cliente => cliente.Endereco.Rua)
                .NotNull().WithMessage("O nome da rua no endereço é obrigatório!")
                .MinimumLength(3).WithMessage("O nome da rua deve conter ao menos 3 caracteres!");

            RuleFor(cliente => cliente.Endereco.Numero)
                .NotNull().WithMessage("O número do endereço é obrigatório!");

            RuleFor(cliente => cliente.Endereco.Bairro)
                .NotNull().WithMessage("O nome do bairro no endereço é obrigatório!")
                .MinimumLength(3).WithMessage("O nome do bairro deve conter ao menos 3 caracteres!");

            RuleFor(cliente => cliente.Endereco.Cidade)
                .NotNull().WithMessage("O nome da cidade no endereço é obrigatório!")
                .MinimumLength(3).WithMessage("O nome da cidade deve conter ao menos 3 caracteres!");

            RuleFor(cliente => cliente.Endereco.Estado)
                .NotNull().WithMessage("O nome do estado no endereço é obrigatório!")
                .MinimumLength(3).WithMessage("O nome do estado deve conter ao menos 3 caracteres!");

            RuleFor(cliente => cliente.Endereco.CEP)
                .NotNull().WithMessage("O CEP no endereço é obrigatório!")
                .Matches(@"^\d{5}(?:[-\s]\d{3})?$").WithMessage("O formato do CEP está incorreto, siga o padrão: '00000-000'.");
        }
    }
}
