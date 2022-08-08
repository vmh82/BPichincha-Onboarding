using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Test.IntegrationTest
{
    public class ClienteServiceTest : CreditoAutoBaseTest
    {
        private HttpClient _httpClient;
        public ClienteDto clienteDto;

        public ClienteServiceTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();

            clienteDto = new ClienteDto
            {
                Identificacion = "1724389615",
                Nombres = "ClienteTest",
                Apellidos = "ClienteTest",
                EstadoCivil = "Soltero",
                Direccion = "Las casas",
                Edad = 29,
                FechaNacimiento = "28/11/1992",
                NombreConyugue = "NA",
                SujetoCredito = "SujetoACredito",
                Telefono = "3030668",
                IdentificacionConyugue = "123456"
            };
        }

        [Test]
        public async Task Crear_Cliente_Existente_Deberia_Devolver_Mensaje_YaRegistrado()
        {
          
            string jsonCliente = JsonConvert.SerializeObject(this.clienteDto);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/cliente/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<ClienteDto> clienteResponse = JsonConvert.DeserializeObject<Response<ClienteDto>>(stringResult);
            Assert.AreEqual("El cliente ya se encuentra registrado", clienteResponse.Mensaje);
        }

        [Test]
        public async Task Consultar_Cliente_NoExistente_Deberia_Devolver_Mensaje_NoEncontrado()
        {
            string identificacion = "1726974563";
            var response = await _httpClient.GetAsync($"api/cliente/consultar/?identificacion={identificacion}");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<ClienteDto> clienteResponse = JsonConvert.DeserializeObject<Response<ClienteDto>>(stringResult);
            Assert.AreEqual("Cliente no encontrado", clienteResponse.Mensaje);
        }

        [Test]
        public async Task Crear_Cliente_Nuevo_Deberia_Devolver_MensajeCreado()
        {
            string identificacion = string.Format("{0}{1}{2}{3}", "171496", new Random().Next(1, 10).ToString(),
                    new Random().Next(1, 10).ToString(),
                    new Random().Next(1, 10).ToString());
            this.clienteDto = new ClienteDto()
            {
                Identificacion = identificacion,
                Nombres = "ClienteTest",
                Apellidos = identificacion,
                EstadoCivil = "Soltero",
                Direccion = "Las casas",
                Edad = 29,
                FechaNacimiento = "28/11/1992",
                NombreConyugue = "NA",
                SujetoCredito = "SujetoACredito",
                Telefono = "3030668",
                IdentificacionConyugue = "123456"
            };
            string jsonCliente = JsonConvert.SerializeObject(this.clienteDto);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/cliente/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<ClienteDto> clienteResponse = JsonConvert.DeserializeObject<Response<ClienteDto>>(stringResult);
            Assert.AreEqual("Cliente Creado Correctamente", clienteResponse.Mensaje);
        }
    }
}
