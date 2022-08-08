using CreditoAuto.Entities.Utils;
using CreditoAuto.Repository;
using CreditoAuto.Repository.Context;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CreditoAuto.Test.UnitTest
{
    public class CargaDocumentosTest
    {
        private CreditoAutoDbContext context;
        private IConfiguration _configuration;
        private InicializarDocumentos inicializarDocumentos;

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new CreditoAutoDbContextTest().InicializarContexto();
            _configuration = new ConfigurationPropertiesTest().InicializarConfiguration();
            inicializarDocumentos = new InicializarDocumentos(context, _configuration);
        }

        [Test]
        public async Task Cargar_Clientes_NoDeberia_Lanzar_Error()
        {
            Assert.DoesNotThrow(() => inicializarDocumentos.CargarClientes());
        }

        [Test]
        public async Task CargarMarca_NoDeberia_Lanzar_Error()
        {
            Assert.DoesNotThrow(() => inicializarDocumentos.CargarMarcas());
        }

        [Test]
        public async Task CargarPatios_NoDeberia_Lanzar_Error()
        {
            Assert.DoesNotThrow(() => inicializarDocumentos.CargarPatios());
        }

        [Test]
        public async Task CargarEjecutivos_NoDeberia_Lanzar_Error()
        {
            Assert.DoesNotThrow(() => inicializarDocumentos.CargarEjecutivos());
        }

        [Test]
        public async Task Cargar_ClientesDuplicados_Deberia_Lanzar_ExcepcionControlada()
        {
            Assert.Throws<CreditoAutoException>(() => inicializarDocumentos.CargarClientes("clientesduplicados"));
        }

        [Test]
        public async Task Cargar_ClientesDuplicados_DocumentoAbiero_Deberia_Lanzar_ExcepcionControlada()
        {
            Assert.Throws<CreditoAutoException>(() => inicializarDocumentos.CargarClientes());
        }
    }
}
