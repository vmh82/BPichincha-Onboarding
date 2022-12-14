namespace CreditoAuto.Entities.Dto
{
    public class ClientePatioDto
    {
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string NombrePatio { get; set; }
        public string DireccionPatio { get; set; }
        public int NumeroPuntoVenta { get; set; }
        public string FechaAsignacion { get; set; }
        public int AsignacionId { get; set; }
    }
}
