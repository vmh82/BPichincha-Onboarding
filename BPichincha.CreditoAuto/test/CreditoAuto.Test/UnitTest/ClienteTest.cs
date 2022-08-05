
using CreditoAuto.Entities.Models;
using CreditoAuto.Repository;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CreditoAuto.Test.UnitTest
{
    public class ClienteTest : CreditoAutoBaseTest
    {
        private ClienteRepository clienteRepository;
        public override void SetUp()
        {
            base.SetUp();
            clienteRepository = new ClienteRepository(this.context);
            Cliente cliente = new Cliente
            {
                Identificacion = "1724389756",
                Nombres = "ClienteTest",
                Apellidos = "ClienteTest",
                EstadoCivil = "Soltero",
                Direccion = "Las casas",
                Edad = 29,
                FechaNacimiento = Convert.ToDateTime("28/11/1992"),
                NombreConyugue = "NA",
                SujetoCredito = "SujetoACredito",
                Telefono = "3030668",
                IdentificacionConyugue = "123456"
            };
            clienteRepository.CrearCliente(cliente).Wait();
        }

        [Test]
        public async Task Crear_Nuevo_Cliente_Deberia_Retornar_Cliente()
        {
            Cliente cliente = new Cliente
            {
                Identificacion = "1725394786",
                Nombres = "Victor Fabian",
                Apellidos = "Maldonado Hernandez",
                EstadoCivil = "Soltero",
                Direccion = "Las casas",
                Edad = 29,
                FechaNacimiento = Convert.ToDateTime("28/11/1992"),
                NombreConyugue = "NA",
                SujetoCredito = "SujetoACredito",
                Telefono = "3030668",
                IdentificacionConyugue = "123456"
            };

            int esFinTransaccion = await clienteRepository.CrearCliente(cliente);
            Assert.AreEqual(1, esFinTransaccion);
        }

        [Test]
        public async Task Consultar_Cliente_Deberia_Retornar_Cliente()
        {
            string identifacion = "1724389756";
            Cliente cliente = await clienteRepository.ConsultarCliente(identifacion);
            Assert.NotNull(cliente);
            Assert.AreEqual("ClienteTest", cliente.Apellidos);
        }
    }
}
