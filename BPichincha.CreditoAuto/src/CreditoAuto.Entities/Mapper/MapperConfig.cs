using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using Mapster;

namespace CreditoAuto.Entities.Mapper
{
    /// <summary>
    /// Clase para realizar el mapeo de datos entre objetos dto y entidades
    /// </summary>
    public class MapperConfig
    {
        /// <summary>
        /// Configuracion mapeador
        /// </summary>
        /// <returns></returns>
        public static TypeAdapterConfig ConfigurarMapper()
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<ClienteDto, Cliente>()
                .Map(dst => dst.Identificacion, org => org.Identificacion)
                .Map(dst => dst.Nombres, org => org.Nombres)
                .Map(dst => dst.Apellidos, org => org.Apellidos)
                .Map(dst => dst.Edad, org => org.Edad)
                .Map(dst => dst.FechaNacimiento, org => org.FechaNacimiento)
                .Map(dst => dst.Direccion, org => org.Direccion)
                .Map(dst => dst.Telefono, org => org.Telefono)
                .Map(dst => dst.EstadoCivil, org => org.EstadoCivil)
                .Map(dst => dst.IdentificacionConyugue, org => org.IdentificacionConyugue)
                .Map(dst => dst.NombreConyugue, org => org.NombreConyugue)
                .Map(dst => dst.SujetoCredito, org => org.SujetoCredito)
                .IgnoreNonMapped(true);

            config.NewConfig<Cliente, ClienteDto>()
                  .Map(dst => dst.Identificacion, org => org.Identificacion)
                  .Map(dst => dst.Nombres, org => org.Nombres)
                  .Map(dst => dst.Apellidos, org => org.Apellidos)
                  .Map(dst => dst.Edad, org => org.Edad)
                  .Map(dst => dst.FechaNacimiento, org => string.Format("{0:dd/MM/yyyy}",org.FechaNacimiento))
                  .Map(dst => dst.Direccion, org => org.Direccion)
                  .Map(dst => dst.Telefono, org => org.Telefono)
                  .Map(dst => dst.EstadoCivil, org => org.EstadoCivil)
                  .Map(dst => dst.IdentificacionConyugue, org => org.IdentificacionConyugue)
                  .Map(dst => dst.NombreConyugue, org => org.NombreConyugue)
                  .Map(dst => dst.SujetoCredito, org => org.SujetoCredito)
                  .IgnoreNonMapped(true);

            config.NewConfig<Ejecutivo, EjecutivoDto>()
                .Map(dst => dst.Identificacion, org => org.Identificacion)
                .Map(dst => dst.Nombres, org => org.Nombres)
                .Map(dst => dst.Apellidos, org => org.Apellidos)
                 .Map(dst => dst.Direccion, org => org.Direccion)
                .Map(dst => dst.TelefonoConvencional, org => org.TelefonoConvencional)
                .Map(dst => dst.Celular, org => org.Celular)
                .Map(dst => dst.NumeroPuntoVenta, org => org.NumeroPuntoVenta)
                .IgnoreNonMapped(true);

            config.NewConfig<AsignacionClienteDto, AsignacionCliente>()
                .Map(dst => dst.Identificacion, org => org.Identificacion)
                .Map(dst => dst.NumeroPuntoVenta, org => org.NumeroPuntoVenta)
                .Map(dst => dst.FechaAsignacion, org => org.FechaAsignacion)
                .IgnoreNonMapped(true);

            config.NewConfig<AsignacionCliente, ClientePatioDto>()
                 .Map(dst => dst.Identificacion, org => org.Identificacion)
                 .Map(dst => dst.Nombres, org => string.Format("{0} {1}",org.Cliente.Nombres, org.Cliente.Apellidos))
                 .Map(dst => dst.DireccionPatio, org => org.Patio.Direccion)
                 .Map(dst => dst.NombrePatio, org => org.Patio.Nombre)
                 .Map(dst => dst.NumeroPuntoVenta, org => org.Patio.NumeroPuntoVenta)
                 .Map(dst => dst.FechaAsignacion, org => string.Format("{0:dd/MM/yyyy hh:mm:ss}", org.FechaAsignacion))
                 .IgnoreNonMapped(true);

            config.NewConfig<SolicitudCreditoDto, SolicitudCredito>()
                 .Map(dst => dst.IdentificacionCliente, org => org.IdentificacionCliente)
                 .Map(dst => dst.IdentificacionEjecutivo, org => org.IdentificacionEjecutivo)
                 .Map(dst => dst.NumeroPuntoVenta, org => org.NumeroPuntoVenta)
                 .Map(dst => dst.Placa, org => org.Placa)
                 .Map(dst => dst.MesesPlazo, org => org.MesesPlazo)
                 .Map(dst => dst.Cuotas, org => org.Cuotas)
                 .Map(dst => dst.Cuotas, org => org.Cuotas)
                 .Map(dst => dst.Entrada, org => org.Entrada)
                 .Map(dst => dst.Observacion, org => org.Observacion)
                 .IgnoreNonMapped(true);

            config.NewConfig<SolicitudCredito, SolicitudCreditoDto>()
                 .Map(dst => dst.IdentificacionCliente, org => org.IdentificacionCliente)
                 .Map(dst => dst.IdentificacionEjecutivo, org => org.IdentificacionEjecutivo)
                 .Map(dst => dst.NumeroPuntoVenta, org => org.NumeroPuntoVenta)
                 .Map(dst => dst.Placa, org => org.Placa)
                 .Map(dst => dst.MesesPlazo, org => org.MesesPlazo)
                 .Map(dst => dst.Cuotas, org => org.Cuotas)
                 .Map(dst => dst.Cuotas, org => org.Cuotas)
                 .Map(dst => dst.Entrada, org => org.Entrada)
                 .Map(dst => dst.Observacion, org => org.Observacion)
                 .IgnoreNonMapped(true);


            config.NewConfig<SolicitudCredito, AsignacionClienteDto>()
                 .Map(dst => dst.Identificacion, org => org.IdentificacionCliente)
                 .Map(dst => dst.NumeroPuntoVenta, org => org.NumeroPuntoVenta)
                 .IgnoreNonMapped(true);

            return config;
        }
    }
}
