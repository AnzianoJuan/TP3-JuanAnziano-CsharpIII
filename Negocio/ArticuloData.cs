using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public class ArticuloData
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetConsulta("SELECT A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, M.Id AS IdMarca, M.Descripcion AS Marca, C.Id AS IdCategoria, C.Descripcion AS Categoria FROM ARTICULOS A JOIN MARCAS M ON A.IdMarca = M.Id JOIN CATEGORIAS C ON A.IdCategoria = C.Id");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo();
                    art.Id = (int)datos.Lector["Id"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Descripcion = (string)datos.Lector["Descripcion"];
                    art.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : null;
                    art.Precio = (decimal)datos.Lector["Precio"];

                    art.Marca = new Marca
                    {
                        Id = (int)datos.Lector["IdMarca"],
                        Descripcion = (string)datos.Lector["Marca"]
                    };

                    art.Categoria = new Categoria
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        Descripcion = (string)datos.Lector["Categoria"]
                    };

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void EliminarFisico(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetConsulta("DELETE FROM ARTICULOS WHERE Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void modificar(Articulo art)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetConsulta("  update ARTICULOS set Codigo = @Codigo,Nombre = @Nombre,Descripcion = @Descripcion,IdMarca = @IdMarca,IdCategoria = @IdCategoria,ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.SetearParametro("@Codigo", art.Codigo);
                datos.SetearParametro("@Nombre", art.Nombre);
                datos.SetearParametro("@Descripcion", art.Descripcion);
                datos.SetearParametro("@IdMarca", art.Marca.Id);
                datos.SetearParametro("@IdCategoria", art.Categoria.Id);
                datos.SetearParametro("@ImagenUrl", art.ImagenUrl);
                datos.SetearParametro("@Precio", art.Precio);
                datos.SetearParametro("@Id", art.Id);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void agregarArticulo(Articulo nuevo)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetConsulta("insert into ARTICULOS(Codigo,Nombre,Descripcion,ImagenUrl,Precio,IdMarca,IdCategoria) values(@Codigo,@Nombre,@Descripcion,@ImagenUrl,@Precio,@IdMarca,@IdCategoria)");
                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.SetearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.SetearParametro("@Precio", nuevo.Precio);
                datos.SetearParametro("@IdMarca", nuevo.Marca.Id);
                datos.SetearParametro("@IdCategoria", nuevo.Categoria.Id);

                datos.EjecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {

            List<Articulo> listaArticulo = new List<Articulo>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {

                string consulta = "SELECT A.Id Id,Codigo,Nombre,A.Descripcion Descripcion,ImagenUrl,Precio,C.Descripcion Categoria,M.Descripcion Marca,A.IdCategoria,A.IdMarca FROM dbo.ARTICULOS A JOIN  dbo.CATEGORIAS C ON C.Id = A.IdCategoria JOIN  dbo.MARCAS M ON M.Id = A.IdMarca where  ";

                if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += " C.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += " C.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += " C.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += " Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += " Precio  < " + filtro;
                            break;
                        default:
                            consulta += " Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += " M.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += " M.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += " M.Descripcion like '%" + filtro + "%'";
                            break;
                    }

                }

                accesoDatos.SetConsulta(consulta);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)accesoDatos.Lector["Id"];
                    aux.Codigo = (string)accesoDatos.Lector["Codigo"];
                    aux.Nombre = (string)accesoDatos.Lector["Nombre"];
                    aux.Descripcion = (string)accesoDatos.Lector["Descripcion"];

                    if (!(accesoDatos.Lector.IsDBNull(accesoDatos.Lector.GetOrdinal("ImagenUrl"))))
                        aux.ImagenUrl = (string)accesoDatos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)accesoDatos.Lector["Precio"];

                    aux.Categoria = new Categoria
                    {
                        Id = (int)accesoDatos.Lector["IdCategoria"],
                        Descripcion = (string)accesoDatos.Lector["Categoria"]
                    };

                    aux.Marca = new Marca
                    {
                        Id = (int)accesoDatos.Lector["IdMarca"],
                        Descripcion = (string)accesoDatos.Lector["Marca"]
                    };

                    listaArticulo.Add(aux);

                }

                return listaArticulo;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
