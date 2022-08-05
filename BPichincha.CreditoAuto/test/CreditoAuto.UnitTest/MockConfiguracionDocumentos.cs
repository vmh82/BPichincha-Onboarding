﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.UnitTest
{
    public  class MockConfiguracionDocumentos
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
                {"Documentos:ejecutivos",string.Format(@"{0}\{1}",pathDocumentos, "ejecutivos.csv")}
            };

            return  new ConfigurationBuilder().AddInMemoryCollection(llavesConfiguracion).Build();
        }
    }
}
