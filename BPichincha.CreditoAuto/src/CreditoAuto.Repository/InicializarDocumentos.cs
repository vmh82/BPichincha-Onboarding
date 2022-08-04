using CreditoAuto.Entities.Models;
using CreditoAuto.Repository.Context;
using Microsoft.Extensions.Configuration;
namespace CreditoAuto.Repository
{
    public class InicializarDocumentos
    {
        private CreditoAutoDbContext _context;
        private IConfiguration _configuration;
        private int EsCargaInicial;
        public InicializarDocumentos(CreditoAutoDbContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            _configuration = configuration;
        }
        public void Inicializar()
        {
            try
            {
                CargarMarcas();
                CargarClientes();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error de tipo {ex.Message}");
            }
        }
        
        public StreamReader LeerArchivo(string documento)
        {
            StreamReader reader = new StreamReader(File.OpenRead(_configuration[$"Documentos:{documento}"]));
            return reader;
        }

        public void CargarClientes()
        {
            StreamReader documentoClientes = LeerArchivo("clientes");
            List<Cliente> clientes = new List<Cliente>();
            List<string> cedulas = new List<string>();
            EsCargaInicial = _context.Cliente.Count();
            if (EsCargaInicial == 0)
            {
                while (!documentoClientes.EndOfStream)
                {

                    string? line = documentoClientes.ReadLine();
                    string[]? values = line.Split(',');
                    if (!cedulas.Contains(values[0]))
                    {
                        Cliente cliente = new Cliente()
                        {
                            Identificacion = values[0],
                            Nombres = values[1],
                            Edad = Convert.ToInt32(values[2]),
                            FechaNacimiento = Convert.ToDateTime(values[3]),
                            Apellidos = values[4],
                            Direccion = values[5],
                            Telefono = values[6],
                            EstadoCivil = values[7],
                            IdentificacionConyugue = values[8],
                            NombreConyugue = values[9],
                            SujetoCredito = values[10],
                        };

                        clientes.Add(cliente);
                    }
                    else
                    {
                        Console.WriteLine($"cliente{values[0]} ya registrado.");
                    }
                }
                _context.Cliente.AddRange(clientes);
                _context.SaveChanges();
            }
        }

        public void CargarMarcas()
        {
            StreamReader documentoMarcas = LeerArchivo("marcas");
            List<Marca> marcas = new List<Marca>();
            List<string> nombres = new List<string>();
            EsCargaInicial = _context.Marca.Count();
            if (EsCargaInicial == 0)
            {
                while (!documentoMarcas.EndOfStream)
                {
                    string? line = documentoMarcas.ReadLine();
                    string[]? values = line.Split(',');
                    if (!nombres.Contains(values[0]))
                    {
                        nombres.Add(values[0]);
                        marcas.Add(new Marca()
                        {
                            Descripcion = values[0]
                        });
                    }
                    else
                    {
                        Console.WriteLine($"Marca {values[0]} ya registrada.");
                    }
                }
                documentoMarcas.Close();
                _context.Marca.AddRange(marcas);
                _context.SaveChanges();
            }
        }
    }
}
