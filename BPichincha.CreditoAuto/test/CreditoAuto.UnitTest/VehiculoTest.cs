using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Mapper;
using CreditoAuto.Entities.Models;
using CreditoAuto.Repository;
using CreditoAuto.Repository.Context;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CreditoAuto.UnitTest
{
    [TestFixture]
    public class VehiculoTest
    {
        private IMapper _mapper;
        private Mock<IVehiculoRepository> _mockRepository;
        private CreditoAutoDbContext mocKContext;
        private IConfiguration _configuration;
        [OneTimeSetUp]
        public void Setup()
        {
            TypeAdapterConfig configMapper = MapperConfig.ConfigurarMapper();
            _mapper = new Mapper(configMapper);
            _mockRepository = new Mock<IVehiculoRepository>();
            mocKContext = new MockCreditoAutoDbContext().InicializarContexto();
            _configuration = new MockConfiguracionDocumentos().InicializarConfiguration();
            new InicializarDocumentos(mocKContext, _configuration).Inicializar();
        }

        [Test]
        public async Task Crear_Vehiculo()
        {
            ///preparacion
            Vehiculo vehiculo = new Vehiculo
            {
                MarcaId = 1,
                Modelo = "Verna",
                Cilindraje = 1.455M,
                Avaluo = 17500M,
                NumeroChasis = "123123123132",
                Placa = "PSP665",
                Tipo = "Sedan"
            };
            //prueba
            VehiculoRepository vehiculoRepository = new VehiculoRepository(mocKContext);
            int esFinTransaccion = await vehiculoRepository.Crear(vehiculo);
            //verificacion
            Assert.AreEqual(1, esFinTransaccion);
            //prueba
            vehiculo = await vehiculoRepository.Consultar(vehiculo.Placa);
            Assert.NotNull(vehiculo);
            Assert.AreEqual("Chevrolet", vehiculo.Marca.Descripcion);
        }
    }
}
