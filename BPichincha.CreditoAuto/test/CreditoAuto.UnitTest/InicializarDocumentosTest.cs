using CreditoAuto.Repository;
using CreditoAuto.Repository.Context;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.UnitTest
{
    public class InicializarDocumentosTest
    {
        private CreditoAutoDbContext mocKContext;
        private IConfiguration _configuration;
        private InicializarDocumentos inicializarDocumentos;

        [OneTimeSetUp]
        public void Setup()
        {
            _configuration = new MockConfiguracionDocumentos().InicializarConfiguration();
            mocKContext = new MockCreditoAutoDbContext().InicializarContexto();
            inicializarDocumentos = new InicializarDocumentos(mocKContext, _configuration);
        }

        [Test]
        public async Task CargarClientes()
        {
            Assert.DoesNotThrow(()=>inicializarDocumentos.CargarClientes());
        }

        [Test]
        public async Task CargarMarca()
        {
            Assert.DoesNotThrow(() => inicializarDocumentos.CargarMarcas());
        }

        [Test]
        public async Task CargarPatios()
        {
            Assert.DoesNotThrow(() => inicializarDocumentos.CargarPatios());
        }

        [Test]
        public async Task CargarEjecutivos()
        {
            Assert.DoesNotThrow(() => inicializarDocumentos.CargarEjecutivos());
        }
    }
}
