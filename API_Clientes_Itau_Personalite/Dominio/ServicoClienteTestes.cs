using Dominio.Interfaces;
using Dominio.Servicos;
using Entidades.Entidades;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task Dadas_CredenciaisValidas_RetornaStatusDeLoginValido()
        {
            //Arrange

            //Configura mocks da comunicacao de rede
            var mockHttpClientFac = new Mock<IHttpClientFactory>();
            var mockHttpClient = new Mock<HttpClient>();
            var mockHttpContent = new Mock<HttpContent>();

            var dummyContent = "{\n    \"id\": \"25370aa8-faa2-4237-f41d-08da2bc99d85\",\n    \"nome\": \"string\",\n    \"email\": \"string@teste.com\"\n}";
            byte[] byteArray = Encoding.UTF8.GetBytes(dummyContent);
            Stream dummyStream = new MemoryStream(byteArray);

            mockHttpContent.Setup(conteudo => conteudo.ReadAsStreamAsync()).Returns(Task.FromResult(dummyStream));

            var httpContent = mockHttpContent.Object;

            var dummyHttpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = httpContent
            };

            mockHttpClient.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(Task.FromResult(dummyHttpResponse));

            var httpClient = mockHttpClient.Object;

            mockHttpClientFac.Setup(httpFac => httpFac.CreateClient()).Returns(httpClient);

            var httpClientFac = mockHttpClientFac.Object;

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

            var servicoCliente = new ServicoCliente(clienteRepo, httpClientFac, configuration);

            //Act
            var resultado = await servicoCliente.Adicionar(clienteDummy, string.Empty);

            //Assert
            Assert.True(resultado);
        }
    }
}
