using Dominio.Interfaces;
using Dominio.Interfaces.InterfacesDeServicos;
using Dominio.Models;
using Dominio.Servicos;
using Entidades.Entidades;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API_Usuarios_Itau_Personalite.Testes
{
    public class ServicoClienteTestes
    {
        [Fact]
        public async Task Dado_ClienteValidoParaSerCadastrado_RetornaStatusDeCadastroBemSucedido()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            var gerenteDummy = new GerenteDto()
            {
                Id = "teste-id",
                Email = "dummy@teste.com",
                Nome = "gerente Dummy"
            };

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var enderecoDummy = new Endereco
            {
                Id = 1,
                Rua = "rua dummy",
                Bairro = "bairro dummy",
                Cidade = "bairro dummy",
                CEP = "00000-000",
                Numero = 123
            };

            var clienteDummy = new Cliente
            {
                Id = 1,
                GerenteId = "id-Dummy",
                Nome = "clienteDummy",
                LimiteCredito = 1000,
                Endereco = enderecoDummy
            };

            mockClienteRepo.Setup(repo => repo.Adicionar(It.IsAny<Cliente>())).Returns(Task.FromResult(clienteDummy));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.Adicionar(clienteDummy, string.Empty);

            //Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task Dado_ClienteParaSerCadastradoComUsuarioInexistente_RetornaStatusDeCadastroMalSucedido()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            GerenteDto gerenteDummy = null;

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var enderecoDummy = new Endereco
            {
                Id = 1,
                Rua = "rua dummy",
                Bairro = "bairro dummy",
                Cidade = "bairro dummy",
                CEP = "00000-000",
                Numero = 123
            };

            var clienteDummy = new Cliente
            {
                Id = 1,
                GerenteId = "id-Dummy",
                Nome = "clienteDummy",
                LimiteCredito = 1000,
                Endereco = enderecoDummy
            };

            mockClienteRepo.Setup(repo => repo.Adicionar(It.IsAny<Cliente>())).Returns(Task.FromResult(clienteDummy));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.Adicionar(clienteDummy, string.Empty);

            //Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task Dado_ErroAoCriarCliente_RetornaStatusDeCadastroMalSucedido()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            var gerenteDummy = new GerenteDto()
            {
                Id = "teste-id",
                Email = "dummy@teste.com",
                Nome = "gerente Dummy"
            };

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var clienteDummy = new Cliente{};

            mockClienteRepo.Setup(repo => repo.Adicionar(It.IsAny<Cliente>())).Returns(Task.FromResult(clienteDummy));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.Adicionar(clienteDummy, string.Empty);

            //Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task Dado_PedidoDeEdicaoCorretamenteConfigurado_RetornaStatusDeEdicaoBemSucedida()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            var gerenteDummy = new GerenteDto()
            {
                Id = "teste-id",
                Email = "dummy@teste.com",
                Nome = "gerente Dummy"
            };

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var enderecoDummy = new Endereco
            {
                Id = 1,
                Rua = "rua dummy",
                Bairro = "bairro dummy",
                Cidade = "bairro dummy",
                CEP = "00000-000",
                Numero = 123
            };

            var clienteDummy = new Cliente
            {
                Id = 1,
                GerenteId = "id-Dummy",
                Nome = "clienteDummy",
                LimiteCredito = 1000,
                Endereco = enderecoDummy
            };

            var clienteDummyEdicao = new Cliente
            {
                Id = 1,
                GerenteId = "id-Dummy",
                Nome = "clienteDummy",
                LimiteCredito = 3000,
                Endereco = enderecoDummy
            };

            mockClienteRepo.Setup(repo => repo.Editar(It.IsAny<Cliente>())).Returns(Task.FromResult(clienteDummy));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.Editar(clienteDummyEdicao, string.Empty);

            //Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task Dado_ClienteParaSerEditadoComUsuarioInexistente_RetornaStatusDeEdicaoMalSucedida()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            GerenteDto gerenteDummy = null;

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var clienteDummyEdicao = new Cliente
            {
                Id = 1,
                GerenteId = "id-Dummy",
                Nome = "clienteDummy",
                LimiteCredito = 3000,
                Endereco = null
            };

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.Editar(clienteDummyEdicao, string.Empty);

            //Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task Dado_idDeClienteCadastrado_RetornaClienteDto()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            var gerenteDummy = new GerenteDto()
            {
                Id = "teste-id",
                Email = "dummy@teste.com",
                Nome = "gerente Dummy"
            };

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var enderecoDummy = new Endereco
            {
                Id = 1,
                Rua = "rua dummy",
                Bairro = "bairro dummy",
                Cidade = "bairro dummy",
                CEP = "00000-000",
                Numero = 123
            };

            var clienteDummy = new Cliente
            {
                Id = 1,
                GerenteId = "id-Dummy",
                Nome = "clienteDummy",
                LimiteCredito = 1000,
                Endereco = enderecoDummy
            };

            mockClienteRepo.Setup(repo => repo.BuscarPorId(It.IsAny<int>())).Returns(Task.FromResult(clienteDummy));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.BuscarClientePorId(clienteDummy.Id, string.Empty);

            //Assert
            Assert.Equal(clienteDummy.Id,resultado.Id);
        }

        [Fact]
        public async Task Dado_idDeClienteCadastradoComGerenteIncorreto_RetornaClienteDtoVazio()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            GerenteDto gerenteDummy = null;

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var enderecoDummy = new Endereco
            {
                Id = 1,
                Rua = "rua dummy",
                Bairro = "bairro dummy",
                Cidade = "bairro dummy",
                CEP = "00000-000",
                Numero = 123
            };

            var clienteDummy = new Cliente
            {
                Id = 1,
                GerenteId = "id-Dummy",
                Nome = "clienteDummy",
                LimiteCredito = 1000,
                Endereco = enderecoDummy
            };

            mockClienteRepo.Setup(repo => repo.BuscarPorId(It.IsAny<int>())).Returns(Task.FromResult(clienteDummy));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.BuscarClientePorId(clienteDummy.Id, string.Empty);

            //Assert
            Assert.Equal(0, resultado.Id);
        }

        [Fact]
        public async Task Dado_idDeClienteNaoCadastrado_RetornaClienteDtoVazio()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            GerenteDto gerenteDummy = null;

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.BuscarClientePorId(1, string.Empty);

            //Assert
            Assert.Equal(0, resultado.Id);
        }

        [Fact]
        public async Task Dada_SolicitacaoDeListaDeClientesPorGerenteId_RetornaListaDeClientesDtoDoGerente()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            var gerenteDummy = new GerenteDto()
            {
                Id = "dummy-gerente-id",
                Email = "dummy@teste.com",
                Nome = "gerente Dummy"
            };

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var enderecoDummy = new Endereco
            {
                Id = 1,
                Rua = "rua dummy",
                Bairro = "bairro dummy",
                Cidade = "bairro dummy",
                CEP = "00000-000",
                Numero = 123
            };

            var clienteDummy = new Cliente
            {
                Id = 1,
                GerenteId = "dummy-gerente-id",
                Nome = "clienteDummy",
                LimiteCredito = 1000,
                Endereco = enderecoDummy
            };

            var listaClientes = new List<Cliente>()
            {
                clienteDummy
            };

            mockClienteRepo.Setup(repo => repo.RetornaListaClientes(It.IsAny<Expression<Func<Cliente, bool>>>()))
                                                                        .Returns(Task.FromResult(listaClientes));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.BuscarClientesPorGerenteId(gerenteDummy.Id, string.Empty);

            //Assert
            Assert.NotEmpty(resultado);
        }

        [Fact]
        public async Task Dada_SolicitacaoDeListaDeClientesPorGerenteIdComGerenteIdInvalido_RetornaListaDeClientesDtoVazia()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            GerenteDto gerenteDummy = null;

            mockServicoHttp.Setup(servico => servico.BuscaUsuarioNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(gerenteDummy));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var enderecoDummy = new Endereco
            {
                Id = 1,
                Rua = "rua dummy",
                Bairro = "bairro dummy",
                Cidade = "bairro dummy",
                CEP = "00000-000",
                Numero = 123
            };

            var clienteDummy = new Cliente
            {
                Id = 1,
                GerenteId = "dummy-gerente-id",
                Nome = "clienteDummy",
                LimiteCredito = 1000,
                Endereco = enderecoDummy
            };

            var listaClientes = new List<Cliente>()
            {
                clienteDummy
            };

            mockClienteRepo.Setup(repo => repo.RetornaListaClientes(It.IsAny<Expression<Func<Cliente, bool>>>()))
                                                                        .Returns(Task.FromResult(listaClientes));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.BuscarClientesPorGerenteId(clienteDummy.GerenteId, string.Empty);

            //Assert
            Assert.Empty(resultado);
        }

        [Fact]
        public async Task Dada_SolicitacaoDeListaDeClientes_RetornaListaDeClientesDto()
        {
            //Arrange
            var mockServicoHttp = new Mock<IServicoHttp>();

            var gerenteDummy = new GerenteDto()
            {
                Id = "dummy-gerente-id",
                Email = "dummy@teste.com",
                Nome = "gerente Dummy"
            };

            var listaGerentes = new List<GerenteDto>()
            { 
                gerenteDummy
            };

            IEnumerable<GerenteDto> listaGerentesEnumerable = listaGerentes;

            mockServicoHttp.Setup(servico => servico.BuscaUsuariosNaApi(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                    .Returns(Task.FromResult(listaGerentesEnumerable));

            var servicoHttp = mockServicoHttp.Object;

            //Configura mocks do repositorio de clientes
            var mockClienteRepo = new Mock<ICliente>();

            var enderecoDummy = new Endereco
            {
                Id = 1,
                Rua = "rua dummy",
                Bairro = "bairro dummy",
                Cidade = "bairro dummy",
                CEP = "00000-000",
                Numero = 123
            };

            var clienteDummy = new Cliente
            {
                Id = 1,
                GerenteId = "dummy-gerente-id",
                Nome = "clienteDummy",
                LimiteCredito = 1000,
                Endereco = enderecoDummy
            };

            var listaClientes = new List<Cliente>()
            {
                clienteDummy
            };

            mockClienteRepo.Setup(repo => repo.ListarTodos()).Returns(Task.FromResult(listaClientes));

            var clienteRepo = mockClienteRepo.Object;

            //Configura mocks da comunicacao de rede
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(repo => repo.GetSection(It.IsAny<string>()).Value).Returns(string.Empty);

            var configuration = mockConfiguration.Object;

            var servicoCliente = new ServicoCliente(clienteRepo, servicoHttp, configuration);

            //Act
            var resultado = await servicoCliente.ListarTodosClientes(string.Empty);

            //Assert
            Assert.NotEmpty(resultado);
        }
    }
}
