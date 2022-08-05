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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CreditoAuto.UnitTest
{
    [TestFixture]
    public class AsignacionClienteTest
    {
        private IMapper _mapper;
        private Mock<ILogger<AsignacionClienteService>> _mockLogger;
        private Mock<IAsignacionClienteRepository> _mockRepository;
        private CreditoAutoDbContext mocKContext;
        private Mock<IClienteService> _mockClienteService;
        private Mock<IPatioService> _mockPatioService;
        private IConfiguration _configuration;

        [OneTimeSetUp]
        public void Setup()
        {
            TypeAdapterConfig configMapper = MapperConfig.ConfigurarMapper();
            _mapper = new Mapper(configMapper);
            _mockLogger = new Mock<ILogger<AsignacionClienteService>>();
            _mockRepository = new Mock<IAsignacionClienteRepository>();
            _mockClienteService = new Mock<IClienteService>();
            _mockPatioService = new Mock<IPatioService>();
            mocKContext = new MockCreditoAutoDbContext().InicializarContexto();
            _configuration = new MockConfiguracionDocumentos().InicializarConfiguration();
            new InicializarDocumentos(mocKContext, _configuration).Inicializar();
        }

        [Test]
        public async Task  ValidarCreacion_Cliente_Patio_NoExiste()
        {
            AsignacionClienteService asignacionService = new AsignacionClienteService(_mapper, _mockLogger.Object, _mockRepository.Object, _mockClienteService.Object, _mockPatioService.Object);
            Response<ClientePatioDto>? asignacionResponse = await asignacionService.Consultar("1724389745");
            Assert.IsNotNull(asignacionResponse.Mensaje);
            Assert.IsNull(asignacionResponse.Data.Identificacion);
            Assert.AreEqual("Cliente no encontrado", asignacionResponse.Mensaje);
            Assert.AreEqual(false, asignacionResponse.EsError);
        }

        [Test]
        public async Task ValidarCreacion_Asignacion()
        {
            //preparacion
            AsignacionCliente asignacion = new AsignacionCliente
            {
                Identificacion = "1724389741",
                FechaAsignacion = DateTime.Now,
                NumeroPuntoVenta = 1
            };
            //prueba
            AsignacionClienteRepository asignacionRepository = new AsignacionClienteRepository(mocKContext);
            int esFinTransaccion = await asignacionRepository.Crear(asignacion);
            //verificacion
            Assert.AreEqual(1, esFinTransaccion);
            AsignacionCliente asignacionConsulta = await asignacionRepository.Consultar(asignacion.Identificacion);
            Assert.NotNull(asignacionConsulta);
            Assert.AreEqual(asignacionConsulta.Identificacion, asignacionConsulta.Identificacion);
        }
    }
}
