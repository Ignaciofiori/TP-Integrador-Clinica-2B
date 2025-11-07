//using modelo;
//using System;
//using System.Collections.Generic;

//namespace negocio
//{
//    public class ArticuloNegocio
//    {
//        public List<Articulo> Listar()
//        {
//            List<Articulo> lista = new List<Articulo>();
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta(@"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, 
//                                              M.Descripcion as Marca, 
//                                              C.Descripcion as Categoria, A.Precio 
//                                       FROM ARTICULOS A 
//                                       INNER JOIN MARCAS M ON A.IdMarca = M.Id 
//                                       INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id");
//                datos.ejecutarLectura();

//                while (datos.Lector.Read())
//                {
//                    /**/
//                    Articulo aux = new Articulo();

//                    aux.Id = (int)datos.Lector["Id"];

//                    if (!(datos.Lector["Codigo"] is DBNull))
//                        aux.CodigoArticulo = (string)datos.Lector["Codigo"];

//                    if (!(datos.Lector["Nombre"] is DBNull))
//                        aux.Nombre = (string)datos.Lector["Nombre"];

//                    if (!(datos.Lector["Descripcion"] is DBNull))
//                        aux.Descripcion = (string)datos.Lector["Descripcion"];

//                    if (!(datos.Lector["Precio"] is DBNull))
//                        aux.Precio = (decimal)datos.Lector["Precio"];

//                    aux.Marca = new Marca();
//                    if (!(datos.Lector["Marca"] is DBNull))
//                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];

//                    aux.Categoria = new Categoria();
//                    if (!(datos.Lector["Categoria"] is DBNull))
//                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

//                    // Imagenes asociadas
//                    aux.Imagenes = ListarImagenesPorArticulo(aux.Id);

//                    lista.Add(aux);
//                }

//                return lista;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }
//        }

//        public List<Imagen> ListarImagenesPorArticulo(int IdArticulo)
//        {
//            List<Imagen> lista = new List<Imagen>();
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta("SELECT Id, IdArticulo, ImagenURL FROM IMAGENES WHERE IdArticulo = @idArticulo");
//                datos.setearParametros("@idArticulo", IdArticulo);
//                datos.ejecutarLectura();

//                while (datos.Lector.Read())
//                {
//                    Imagen aux = new Imagen();

//                    aux.Id = (int)datos.Lector["Id"];
//                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];

//                    if (!(datos.Lector["ImagenURL"] is DBNull))
//                        aux.ImagenURL = (string)datos.Lector["ImagenURL"];

//                    lista.Add(aux);
//                }

//                return lista;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }
//        }

//        public Articulo BuscarPorId(int id)
//        {
//            Articulo articulo = null;
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta(@"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, 
//                                              M.Descripcion as Marca, 
//                                              C.Descripcion as Categoria, 
//                                              A.Precio 
//                                       FROM ARTICULOS A 
//                                       INNER JOIN MARCAS M ON A.IdMarca = M.Id 
//                                       INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id 
//                                       WHERE A.Id = @id");
//                datos.setearParametros("@id", id);
//                datos.ejecutarLectura();

//                if (datos.Lector.Read())
//                {
//                    articulo = new Articulo();

//                    articulo.Id = (int)datos.Lector["Id"];

//                    if (!(datos.Lector["Codigo"] is DBNull))
//                        articulo.CodigoArticulo = (string)datos.Lector["Codigo"];

//                    if (!(datos.Lector["Nombre"] is DBNull))
//                        articulo.Nombre = (string)datos.Lector["Nombre"];

//                    if (!(datos.Lector["Descripcion"] is DBNull))
//                        articulo.Descripcion = (string)datos.Lector["Descripcion"];

//                    if (!(datos.Lector["Precio"] is DBNull))
//                        articulo.Precio = (decimal)datos.Lector["Precio"];

//                    articulo.Marca = new Marca();
//                    if (!(datos.Lector["Marca"] is DBNull))
//                        articulo.Marca.Descripcion = (string)datos.Lector["Marca"];

//                    articulo.Categoria = new Categoria();
//                    if (!(datos.Lector["Categoria"] is DBNull))
//                        articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];

//                    // Imágenes del artículo
//                    articulo.Imagenes = (List<Imagen>)ListarImagenesPorArticulo(articulo.Id);
//                }

//                return articulo;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }
//        }
//    }
//}
