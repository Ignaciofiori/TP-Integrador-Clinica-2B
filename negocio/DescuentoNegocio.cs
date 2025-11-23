using modelo;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class DescuentoNegocio
    {
        public List<Descuento> Listar()
        {
            List<Descuento> lista = new List<Descuento>();
            AccesoDatos datos = new AccesoDatos();
            ObraSocialNegocio obraNegocio = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_descuento, id_obra_social, edad_min, edad_max,
                           porcentaje_descuento, descripcion, activo
                    FROM Descuento
                    WHERE activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Descuento d = new Descuento();

                    d.Id = (int)datos.Lector["id_descuento"];

                    if (!(datos.Lector["id_obra_social"] is DBNull))
                    {
                        int idObra = (int)datos.Lector["id_obra_social"];
                        d.ObraSocial = obraNegocio.BuscarPorId(idObra);
                    }

                    if (!(datos.Lector["edad_min"] is DBNull))
                        d.EdadMin = (int)datos.Lector["edad_min"];

                    if (!(datos.Lector["edad_max"] is DBNull))
                        d.EdadMax = (int)datos.Lector["edad_max"];

                    if (!(datos.Lector["porcentaje_descuento"] is DBNull))
                        d.PorcentajeDescuento = (decimal)datos.Lector["porcentaje_descuento"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        d.Descripcion = (string)datos.Lector["descripcion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        d.Activo = (bool)datos.Lector["activo"];

                    lista.Add(d);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public List<Descuento> ListarPorObraSocial(int idObra)
        {
            List<Descuento> lista = new List<Descuento>();
            AccesoDatos datos = new AccesoDatos();
            ObraSocialNegocio obraNegocio = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_descuento, id_obra_social, edad_min, edad_max,
                           porcentaje_descuento, descripcion, activo
                    FROM Descuento
                    WHERE id_obra_social = @id AND activo = 1");

                datos.setearParametros("@id", idObra);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Descuento d = new Descuento();

                    d.Id = (int)datos.Lector["id_descuento"];

                    if (!(datos.Lector["id_obra_social"] is DBNull))
                    {
                        int idObraSocial = (int)datos.Lector["id_obra_social"];
                        d.ObraSocial = obraNegocio.BuscarPorId(idObraSocial);
                    }

                    if (!(datos.Lector["edad_min"] is DBNull))
                        d.EdadMin = (int)datos.Lector["edad_min"];

                    if (!(datos.Lector["edad_max"] is DBNull))
                        d.EdadMax = (int)datos.Lector["edad_max"];

                    if (!(datos.Lector["porcentaje_descuento"] is DBNull))
                        d.PorcentajeDescuento = (decimal)datos.Lector["porcentaje_descuento"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        d.Descripcion = (string)datos.Lector["descripcion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        d.Activo = (bool)datos.Lector["activo"];

                    lista.Add(d);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public Descuento BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            ObraSocialNegocio obraNegocio = new ObraSocialNegocio();
            Descuento d = null;

            try
            {
                datos.setearConsulta(@"
                    SELECT id_descuento, id_obra_social, edad_min, edad_max,
                           porcentaje_descuento, descripcion, activo
                    FROM Descuento
                    WHERE id_descuento = @id");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    d = new Descuento();

                    d.Id = (int)datos.Lector["id_descuento"];

                    if (!(datos.Lector["id_obra_social"] is DBNull))
                    {
                        int idObra = (int)datos.Lector["id_obra_social"];
                        d.ObraSocial = obraNegocio.BuscarPorId(idObra);
                    }

                    if (!(datos.Lector["edad_min"] is DBNull))
                        d.EdadMin = (int)datos.Lector["edad_min"];

                    if (!(datos.Lector["edad_max"] is DBNull))
                        d.EdadMax = (int)datos.Lector["edad_max"];

                    if (!(datos.Lector["porcentaje_descuento"] is DBNull))
                        d.PorcentajeDescuento = (decimal)datos.Lector["porcentaje_descuento"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        d.Descripcion = (string)datos.Lector["descripcion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        d.Activo = (bool)datos.Lector["activo"];
                }

                return d;
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
                    INSERT INTO Descuento
                    (id_obra_social, edad_min, edad_max, porcentaje_descuento, descripcion, activo)
                    VALUES (@obra, @min, @max, @porc, @desc, 1)");

                datos.setearParametros("@obra", d.ObraSocial.IdObraSocial);
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
                    SET id_obra_social = @obra,
                        edad_min = @min,
                        edad_max = @max,
                        porcentaje_descuento = @porc,
                        descripcion = @desc
                    WHERE id_descuento = @id");

                datos.setearParametros("@obra", d.ObraSocial.IdObraSocial);
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
                datos.setearConsulta("UPDATE Descuento SET activo = 0 WHERE id_descuento = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public void Reactivar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Descuento SET activo = 1 WHERE id_descuento = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }        }


    }
}
