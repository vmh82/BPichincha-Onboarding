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
    public class SolicitudCreditoServiceTest
    {
        private HttpClient _httpClient;
        public SolicitudCreditoDto solicitudCreditoDto;

        public SolicitudCreditoServiceTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [Test]
        public async Task Crear_SolicitudCredito_ClienteNoExistente_Deberia_Devolver_Mensaje_ClienteNoExiste()
        {
            solicitudCreditoDto = new SolicitudCreditoDto
            {
                IdentificacionCliente = "1724389799",
                NumeroPuntoVenta = 1,
                Placa = "PSP665",
                MesesPlazo = 24,
                Cuotas = 5,
                Entrada = 1250M,
                IdentificacionEjecutivo = "1726394781",
                Observacion = "ssss"
            };
            string jsonCliente = JsonConvert.SerializeObject(solicitudCreditoDto);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/solicitudcredito/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<SolicitudCreditoDto> solicitudResponse = JsonConvert.DeserializeObject<Response<SolicitudCreditoDto>>(stringResult);
            Assert.AreEqual("Cliente no encontrado", solicitudResponse.Mensaje);
        }

        [Test]
        public async Task Crear_SolicitudCredito_PlacaNoExistente_Deberia_Devolver_Mensaje_VehiculoNoEncontrado()
        {
            solicitudCreditoDto = new SolicitudCreditoDto
            {
                IdentificacionCliente = "1724389745",
                NumeroPuntoVenta = 1,
                Placa = "PSP659",
                MesesPlazo = 24,
                Cuotas = 5,
                Entrada = 1250M,
                IdentificacionEjecutivo = "1726394781",
                Observacion = "ssss"
            };
            string jsonCliente = JsonConvert.SerializeObject(solicitudCreditoDto);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/solicitudcredito/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<SolicitudCreditoDto> solicitudResponse = JsonConvert.DeserializeObject<Response<SolicitudCreditoDto>>(stringResult);
            Assert.AreEqual("Vehiculo no encontrado", solicitudResponse.Mensaje);
        }

        [Test]
        public async Task Crear_SolicitudCredito_PuntoVentaNoExistente_Deberia_Devolver_Mensaje_PatioNoEncontrado()
        {
            solicitudCreditoDto = new SolicitudCreditoDto
            {
                IdentificacionCliente = "1724389745",
                NumeroPuntoVenta = 265,
                Placa = "PSP667",
                MesesPlazo = 24,
                Cuotas = 5,
                Entrada = 1250M,
                IdentificacionEjecutivo = "1726394781",
                Observacion = "ssss"
            };
            string jsonCliente = JsonConvert.SerializeObject(solicitudCreditoDto);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/solicitudcredito/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<SolicitudCreditoDto> solicitudResponse = JsonConvert.DeserializeObject<Response<SolicitudCreditoDto>>(stringResult);
            Assert.AreEqual("Patio no encontrado", solicitudResponse.Mensaje);
        }

        [Test]
        public async Task Crear_SolicitudCredito_ValidarVehiculo_Deberia_Devolver_Mensaje_VehiculoYaReservado()
        {
            solicitudCreditoDto = new SolicitudCreditoDto
            {
                IdentificacionCliente = "171496423",
                NumeroPuntoVenta = 1,
                Placa = "PSP665",
                MesesPlazo = 24,
                Cuotas = 5,
                Entrada = 1250M,
                IdentificacionEjecutivo = "1726394781",
                Observacion = "ssss"
            };
            string jsonCliente = JsonConvert.SerializeObject(solicitudCreditoDto);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/solicitudcredito/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<SolicitudCreditoDto> solicitudResponse = JsonConvert.DeserializeObject<Response<SolicitudCreditoDto>>(stringResult);
            Assert.AreEqual("El vehiculo ya se encuentra reservado", solicitudResponse.Mensaje);
        }

        [Test]
        public async Task Crear_SolicitudCredito_Deberia_Devolver_Mensaje_SolicitudCreada()
        {
            solicitudCreditoDto = new SolicitudCreditoDto
            {
                IdentificacionCliente = "1724389646",
                NumeroPuntoVenta = 1,
                Placa = "PSP667",
                MesesPlazo = 24,
                Cuotas = 5,
                Entrada = 1250M,
                IdentificacionEjecutivo = "1726394781",
                Observacion = "NA"
            };
            string jsonCliente = JsonConvert.SerializeObject(solicitudCreditoDto);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/solicitudcredito/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<SolicitudCreditoDto> solicitudResponse = JsonConvert.DeserializeObject<Response<SolicitudCreditoDto>>(stringResult);
            Assert.AreEqual("Solicitud creada exitosamente", solicitudResponse.Mensaje);
        }


        [Test]
        public async Task Crear_SolicitudCredito_ValidarSolicitud_Deberia_Devolver_Mensaje_YaExisteUnaSolicitud()
        {
            solicitudCreditoDto = new SolicitudCreditoDto
            {
                IdentificacionCliente = "1724389646",
                NumeroPuntoVenta = 1,
                Placa = "PSP667",
                MesesPlazo = 24,
                Cuotas = 5,
                Entrada = 1250M,
                IdentificacionEjecutivo = "1726394781",
                Observacion = "ssss"
            };
            string jsonCliente = JsonConvert.SerializeObject(solicitudCreditoDto);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/solicitudcredito/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<SolicitudCreditoDto> solicitudResponse = JsonConvert.DeserializeObject<Response<SolicitudCreditoDto>>(stringResult);
            Assert.AreEqual("El cliente ya cuenta con una solicitud en proceso", solicitudResponse.Mensaje);
        }
    }
}
