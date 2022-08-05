using CreditoAuto.Entities.Models;
using CreditoAuto.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Test.UnitTest
{
    public class MarcaTest:CreditoAutoBaseTest
    {
        private MarcaRepository marcaRepository;
        public Marca marca;
        public override void SetUp()
        {
            base.SetUp();
            marca = new Marca
            {
                Descripcion = "Mazda",
            };
            marcaRepository = new MarcaRepository(this.context);
            marcaRepository.Crear(marca).Wait();
        }

        [Test]
        public async Task Crear_Nueva_Marca_Deberia_Retornar_Marca()
        {
            Marca marca = new Marca
            {
                Descripcion = "Mitsubishi",
            };
            int esFinTransaccion = await marcaRepository.Crear(marca);
            Assert.AreEqual(1, esFinTransaccion);
        }

        [Test]
        public async Task Consultar_Marca_Deberia_Retornar_Marca()
        {
            int marcaId = 1;
             Marca marca = await marcaRepository.Consultar(marcaId);
            Assert.AreEqual("Mazda", marca.Descripcion);
        }

    }
}
