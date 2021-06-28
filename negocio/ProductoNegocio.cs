using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setearConsulta("select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Id as IdMarca, M.Descripcion as MARCAS," +
                " C.Id as IdCategoria, C.Descripcion as CATEGORIAS, A.UrlImagen, A.Precio from ARTICULOS " +
                "A inner join CATEGORIAS C on A.IdCategoria = C.Id inner join MARCAS M on A.IdMarca = M.Id");
                   
                    
                datos.ejecutarLecura();
                

                

               
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.UrlImagen = (string)datos.Lector["UrlImagen"];
                    aux.Precio = (decimal)datos.Lector["precio"];
                    aux.marca = new Marca((string)datos.Lector["MARCAS"]);
                    aux.categoria = new Categoria((string)datos.Lector["Categoria"]);
                    lista.Add(aux);
                }

                return lista;

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

        public void Agregar(Producto Nuevo)
        { AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, UrlImagen, Precio)" +
                    "Values (@codigo, @nombre, @desc, @idMar, @idCat, @urlImag, @precio)");
                datos.agregarParametro("@codigo", Nuevo.Codigo);
                datos.agregarParametro("@nombre", Nuevo.Nombre);
                datos.agregarParametro("@desc", Nuevo.Descripcion);
                datos.agregarParametro("@idmar", Nuevo.marca.Id);
                datos.agregarParametro("@idcat", Nuevo.categoria.Id);
                datos.agregarParametro("@urlimag", Nuevo.UrlImagen);
                datos.agregarParametro("@precio", Nuevo.Precio);
                datos.ejecutarAccion();
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
        

        public void modificar(Producto modificar)
         {
            AccesoDatos datos = new AccesoDatos();
            try 
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @desc, " +
                    "IdMarca=@idmarca, IdCategoria=@idcat, UrlImagen=@urlImag, Precio=@precio where Id=@Id");
                datos.agregarParametro("@codigo", modificar.Codigo);
                datos.agregarParametro("@nombre", modificar.Nombre);
                datos.agregarParametro("@descripcion", modificar.Descripcion);
                datos.agregarParametro("@idmarca", modificar.marca.Id);
                datos.agregarParametro("@urlimagen", modificar.UrlImagen);
                datos.agregarParametro("@precio", modificar.Precio);
                datos.agregarParametro("@id", modificar.Id);

                datos.ejecutarAccion();

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
        
             public void Eliminar(int id)
            {

                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("delete from ARTICULOS where Id=" + id);
                    datos.ejecutarAccion();
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
