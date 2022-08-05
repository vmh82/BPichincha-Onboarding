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
    public class SolicitudCreditoTest : CreditoAutoBaseTest
    {
        private SolicitudCreditoRepository solicitudRepository;
        public  SolicitudCredito solicitud;
        public override void SetUp()
        {
            base.SetUp();
            solicitudRepository = new SolicitudCreditoRepository(this.context);
            solicitud = new SolicitudCredito
            {
                IdentificacionCliente = "1724389745",
                NumeroPuntoVenta = 1,
                Placa = "PSP665",
                MesesPlazo = 24,
                Cuotas = 5,
                Entrada = 1250M,
                IdentificacionEjecutivo = "1726394781",
                Observacion = "ssss",
                Estado = 1,
            };
            
            solicitudRepository.Crear(solicitud).Wait();
        }
        [Test]
        public async Task Consultar_Solicitud_Deberia_Devolver_Solicitud()
        {
            string identificacionCliente = "1724389745";            
           SolicitudCredito solicitud = await solicitudRepository.Consultar(identificacionCliente);
            Assert.IsNotNull(solicitud);
        }
        [Test]
        public async Task Crear_Solicitud_Deberia_Devolver_Solicitud()
        {
            solicitudRepository = new SolicitudCreditoRepository(this.context);
            solicitud = new SolicitudCredito
            {
                IdentificacionCliente = "1724389746",
                NumeroPuntoVenta = 1,
                Placa = "PSP665",
                MesesPlazo = 24,
                Cuotas = 5,
                Entrada = 1250M,
                IdentificacionEjecutivo = "1726394781",
                Observacion = "ssss",
                Estado = 1,
            };
            int esFinTransaccion = await solicitudRepository.Crear(solicitud);
            Assert.AreEqual(1, esFinTransaccion);
        }

        [Test]
        public async Task Validar_Solicitud_Por_Dia_DeberiaDevolver_Solicitud()
        {
            SolicitudCredito solicitudCredito = await solicitudRepository.ValidarSolicitudPorDia(solicitud);
            Assert.NotNull(solicitudCredito);
        }
    }
}
