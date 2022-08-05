using CreditoAuto.Repository;
using CreditoAuto.Repository.Context;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Test
{
    public class CreditoAutoBaseTest
    {
        public CreditoAutoDbContext context;
        public IConfiguration _configuration;

        [OneTimeSetUp]
        public virtual void SetUp()
        {
            context = new CreditoAutoDbContextTest().InicializarContexto();
            _configuration = new ConfigurationPropertiesTest().InicializarConfiguration();
            new InicializarDocumentos(context, _configuration).Inicializar();
        }
    }
}
