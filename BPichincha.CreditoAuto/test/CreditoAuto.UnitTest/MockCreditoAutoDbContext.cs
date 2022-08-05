using CreditoAuto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.UnitTest
{
    public class MockCreditoAutoDbContext
    {
        public CreditoAutoDbContext InicializarContexto()
        {
            string? nombreBaseDatosEnMemoria = $"CreditoAuto_{DateTime.Now.ToFileTimeUtc()}";
            DbContextOptions<CreditoAutoDbContext> options = new DbContextOptionsBuilder<CreditoAutoDbContext>().UseInMemoryDatabase(nombreBaseDatosEnMemoria).Options;
            return new CreditoAutoDbContext(options);
        }
    }
}
