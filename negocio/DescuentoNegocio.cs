using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelo;

namespace negocio
{
    /// 
    /// Funciones basicas, agregar mas si es necesario.
    ///

    public class DescuentoNegocio
    {
        public List<Descuento> Listar()
        {
            List<Descuento> lista = new List<Descuento>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT D.id_descuento, D.id_obra_social, D.edad_min, D.edad_max, 
                           D.porcentaje_descuento, D.descripcion,
                           O.nombre AS nombre_obra_social
                    FROM Descuento D
                    INNER JOIN ObraSocial O ON D.id_obra_social = O.id_obra_social
                ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Descuento d = new Descuento();
                    d.Id = (int)datos.Lector["id_descuento"];

                    // Cargar ObraSocial asociada
                    d.IdObraSocial = new ObraSocial
                    {
                        Id = (int)datos.Lector["id_obra_social"],
                        Nombre = datos.Lector["nombre_obra_social"] as string
                    };

                    if (!(datos.Lector["edad_min"] is DBNull))
                        d.EdadMin = (int)datos.Lector["edad_min"];

                    if (!(datos.Lector["edad_max"] is DBNull))
                        d.EdadMax = (int)datos.Lector["edad_max"];

                    if (!(datos.Lector["porcentaje_descuento"] is DBNull))
                        d.PorcentajeDescuento = (decimal)datos.Lector["porcentaje_descuento"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        d.Descripcion = (string)datos.Lector["descripcion"];

                    lista.Add(d);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Descuento> ListarPorObraSocial(int idObraSocial)
        {
            List<Descuento> lista = new List<Descuento>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT D.id_descuento, D.id_obra_social, D.edad_min, D.edad_max, 
                   D.porcentaje_descuento, D.descripcion,
                   O.nombre AS nombre_obra_social
            FROM Descuento D
            INNER JOIN ObraSocial O ON D.id_obra_social = O.id_obra_social
            WHERE D.id_obra_social = @idObraSocial 
        ");

                // Seteamos el parámetro que filtra la consulta
                datos.setearParametros("@idObraSocial", idObraSocial);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Descuento d = new Descuento();
                    d.Id = (int)datos.Lector["id_descuento"];

                    // Cargar la Obra Social (usando el modelo complejo de tu compañero)
                    d.IdObraSocial = new ObraSocial
                    {
                        Id = (int)datos.Lector["id_obra_social"],
                        Nombre = datos.Lector["nombre_obra_social"] as string
                    };

                    // Cargar el resto de las propiedades, manejando DBNull
                    if (!(datos.Lector["edad_min"] is DBNull))
                        d.EdadMin = (int)datos.Lector["edad_min"];

                    if (!(datos.Lector["edad_max"] is DBNull))
                        d.EdadMax = (int)datos.Lector["edad_max"];

                    if (!(datos.Lector["porcentaje_descuento"] is DBNull))
                        d.PorcentajeDescuento = (decimal)datos.Lector["porcentaje_descuento"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        d.Descripcion = (string)datos.Lector["descripcion"];

                    lista.Add(d);
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

        public void Agregar(Descuento d)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Descuento (id_obra_social, edad_min, edad_max, porcentaje_descuento, descripcion)
                    VALUES (@obra, @min, @max, @porc, @desc)
                ");

                datos.setearParametros("@obra", d.IdObraSocial.Id);
                datos.setearParametros("@min", d.EdadMin);
                datos.setearParametros("@max", d.EdadMax);
                datos.setearParametros("@porc", d.PorcentajeDescuento);
                datos.setearParametros("@desc", d.Descripcion);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Descuento d)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    UPDATE Descuento
                    SET id_obra_social = @obra, edad_min = @min, edad_max = @max,
                        porcentaje_descuento = @porc, descripcion = @desc
                    WHERE id_descuento = @id
                ");

                datos.setearParametros("@obra", d.IdObraSocial.Id);
                datos.setearParametros("@min", d.EdadMin);
                datos.setearParametros("@max", d.EdadMax);
                datos.setearParametros("@porc", d.PorcentajeDescuento);
                datos.setearParametros("@desc", d.Descripcion);
                datos.setearParametros("@id", d.Id);

                datos.ejecutarAccion();
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
                datos.setearConsulta("DELETE FROM Descuento WHERE id_descuento = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

