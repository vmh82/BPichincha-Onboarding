using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace CreditoAuto.Test
{
    public class ConfigurationPropertiesTest
    {
        public IConfigurationRoot InicializarConfiguration()
        {
            string? directorioSolucion = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName);
            string pathDocumentos = Path.Combine(directorioSolucion, @"src\Documentos");
            Dictionary<string, string>? llavesConfiguracion = new Dictionary<string, string>
            {
                {"Documentos:clientes", string.Format(@"{0}\{1}",pathDocumentos, "clientes.csv")},
                {"Documentos:marcas", string.Format(@"{0}\{1}",pathDocumentos, "marcas.csv")},
                {"Documentos:patios", string.Format(@"{0}\{1}",pathDocumentos, "patios.csv")},
                {"Documentos:ejecutivos",string.Format(@"{0}\{1}",pathDocumentos, "ejecutivos.csv")},
                {"Documentos:clientesduplicados",string.Format(@"{0}\{1}",pathDocumentos, "clientesduplicados.csv")}
            };

            return new ConfigurationBuilder().AddInMemoryCollection(llavesConfiguracion).Build();
        }
    }
}
