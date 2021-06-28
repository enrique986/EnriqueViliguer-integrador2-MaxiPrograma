using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using negocio;
namespace presentacion_consola
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> lista;
            ProductoNegocio negocio = new ProductoNegocio();

            lista = negocio.Listar();

            foreach (Producto producto in lista)
               {

                Console.WriteLine(producto.Nombre);

               }
            Console.ReadKey();

        }
    }
}
