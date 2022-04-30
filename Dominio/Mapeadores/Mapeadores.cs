using AutoMapper;
using Dominio.Models;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public static class Mapeadores
    {
        public static Mapper ObterMapeadorClienteClienteDto()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteDto>()
                .ForMember(d => d.Gerente, opt => opt.Ignore());
                cfg.CreateMap<Endereco, EnderecoDto>();
                cfg.CreateMap<GerenteDto, ClienteDto>()
                .ForMember(d => d.Gerente, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Nome, opt => opt.Ignore())
                .ForMember(d => d.Endereco, opt => opt.Ignore())
                .ForMember(d => d.LimiteCredito, opt => opt.Ignore());
            });
            mappingConfig.AssertConfigurationIsValid();

            return new Mapper(mappingConfig);
        }

        public static List<ClienteDto> MapeiaClientesMesmoGerenteparaDto(GerenteDto gerente, List<Cliente> listaClientes)
        {
            Mapper mapper = ObterMapeadorClienteClienteDto();

            var clienteDtoMapeado = mapper.Map<List<Cliente>, List<ClienteDto>>(listaClientes);

            clienteDtoMapeado.ForEach(cliente => mapper.Map(gerente, cliente));

            return clienteDtoMapeado;
        }        

        public static ClienteDto MapeiaClienteEGerenteparaDto(GerenteDto gerente, Cliente cliente)
        {
            Mapper mapper = ObterMapeadorClienteClienteDto();

            var clienteDtoMapeado = mapper.Map<Cliente, ClienteDto>(cliente);

            mapper.Map(gerente, clienteDtoMapeado);

            return clienteDtoMapeado;
        }
    }
}
