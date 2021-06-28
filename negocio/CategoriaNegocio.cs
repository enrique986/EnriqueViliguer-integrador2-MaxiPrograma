using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
   public class CategoriaNegocio
    {
        public List<Categoria>Listar()
        {
            List<Categoria> categorias = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta("select Id,Descripcion from CATEGORIAS");
                datos.ejecutarLecura();

                while (datos.Lector.Read())
                {
                    categorias.Add(new Categoria((int)datos.Lector["Id"], (string)datos.Lector["Descripcion"]));
                 }
                return categorias;
            }
            catch(Exception ex)
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
