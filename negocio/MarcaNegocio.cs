using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using System.Data.SqlClient;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
         {
             List<Marca> marca = new List<Marca>();
             AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select Id, Descripcion from MARCAS");
                datos.ejecutarLecura();
                while (datos.Lector.Read())
                {
                    marca.Add(new Marca((int)datos.Lector["Id"], (string)datos.Lector["Descripcion"]));
                }
                return marca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

         }
        
        

        
    }
}
