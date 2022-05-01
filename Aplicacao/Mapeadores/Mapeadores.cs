using Aplicacao.Models;
using AutoMapper;
using Dominio.Models;

namespace Aplicacao
{
    public static class Mapeadores
    {
        public static Mapper ObterMapeadorClienteDtoClienteUi()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteDto, ClienteUi>();
                cfg.CreateMap<GerenteDto, GerenteUi>();
                cfg.CreateMap<EnderecoDto, EnderecoUi>();
            });
            mappingConfig.AssertConfigurationIsValid();

            var mapper = new Mapper(mappingConfig);
            return mapper;
        }
    }
}
