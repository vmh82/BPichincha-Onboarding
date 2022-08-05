using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Mapper;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;
using CreditoAuto.Infraestructure.Services;
using CreditoAuto.Repository;
using CreditoAuto.Repository.Context;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CreditoAuto.UnitTest
{
    [TestFixture]
    public class ClienteTest
    {
        private IMapper _mapper;
        private Mock<ILogger<ClienteService>> _mockLogger;
        private Mock<IClienteRepository> _mockRepository;
        private CreditoAutoDbContext mocKContext;
        private IConfiguration _configuration;

        [OneTimeSetUp]
        public void Setup()
        {
            TypeAdapterConfig configMapper = MapperConfig.ConfigurarMapper();
            _mapper = new Mapper(configMapper);
            _mockLogger = new Mock<ILogger<ClienteService>>();
            _mockRepository = new Mock<IClienteRepository>();
            mocKContext = new MockCreditoAutoDbContext().InicializarContexto();
            _configuration = new MockConfiguracionDocumentos().InicializarConfiguration();
            new InicializarDocumentos(mocKContext, _configuration).Inicializar();
        }
        [Test]
        public async Task Consultar_Cliente_No_Existente()
        {
            ClienteService clienteService = new ClienteService(_mapper, _mockLogger.Object, _mockRepository.Object);
            Response<ClienteDto>? clienteResponse = await clienteService.ConsultarCliente("1724389615");
            Assert.IsNotNull(clienteResponse.Mensaje);
            Assert.IsNull(clienteResponse.Data.Identificacion);
            Assert.AreEqual("Cliente no encontrado", clienteResponse.Mensaje);
            Assert.AreEqual(false, clienteResponse.EsError);
        }

        [Test]
        public async Task Error_Crear_Cliente()
        {
            //preparacion
            ClienteDto clienteDto = new ClienteDto
            {
                Identificacion = "5698743569",
                Nombres = "Victor Fabian",
                Apellidos = "Maldonado Hernandez",
                EstadoCivil = "Soltero",
                Direccion = "Las casas",
                Edad = 29,
                FechaNacimiento = "28/11/1992",
                NombreConyugue = "NA",
                SujetoCredito = "SujetoACredito",
                Telefono = "3030668",
                IdentificacionConyugue = "123456"
            };
            //prueba
            ClienteService clienteService = new ClienteService(_mapper, _mockLogger.Object, _mockRepository.Object);
            Response<ClienteDto>? clienteResponse = await clienteService.CrearCliente(clienteDto);
            //verificacion
            Assert.AreEqual(200, (int)clienteResponse.Status);
            Assert.IsNull(clienteResponse.Data.Identificacion);
        }

        [Test]
        public async Task Crear_Cliente()
        {
            ///preparacion
            Cliente cliente = new Cliente
            {
                Identificacion = "5698743569",
                Nombres = "Victor Fabian",
                Apellidos = "Maldonado Hernandez",
                EstadoCivil = "Soltero",
                Direccion = "Las casas",
                Edad = 29,
                FechaNacimiento = DateTime.Parse("28/11/1992"),
                NombreConyugue = "NA",
                SujetoCredito = "SujetoACredito",
                Telefono = "3030668",
                IdentificacionConyugue = "123456"
            };
            //prueba
            ClienteRepository clienteRepository = new ClienteRepository(mocKContext);
            int esFinTransaccion = await clienteRepository.CrearCliente(cliente);
            //verificacion
            Assert.AreEqual(1, esFinTransaccion);
            Cliente clienteConsulta = await clienteRepository.ConsultarCliente(cliente.Identificacion);
            Assert.NotNull(clienteConsulta);
            Assert.AreEqual(cliente.Identificacion, clienteConsulta.Identificacion);
        }

    }
}
