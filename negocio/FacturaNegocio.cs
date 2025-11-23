using modelo;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class FacturaNegocio
    {
        private  TurnoNegocio turnoNeg = new TurnoNegocio();

        public List<Factura> Listar()
        {
            List<Factura> lista = new List<Factura>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_factura, id_turno, monto_base, cobertura_aplicada, 
                           descuento_aplicado, monto_total, fecha_emision, activo
                    FROM Factura
                    WHERE activo = 1
                ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Factura f = new Factura();

                    // PK
                    f.IdFactura = (int)datos.Lector["id_factura"];

                    // Turno como objeto (lo traemos desde negocio)
                    if (!(datos.Lector["id_turno"] is DBNull))
                        f.Turno = turnoNeg.BuscarPorId((int)datos.Lector["id_turno"]);

                    if (!(datos.Lector["monto_base"] is DBNull))
                        f.MontoBase = (decimal)datos.Lector["monto_base"];

                    if (!(datos.Lector["cobertura_aplicada"] is DBNull))
                        f.CoberturaAplicada = (decimal)datos.Lector["cobertura_aplicada"];

                    if (!(datos.Lector["descuento_aplicado"] is DBNull))
                        f.DescuentoAplicado = (decimal)datos.Lector["descuento_aplicado"];

                    if (!(datos.Lector["monto_total"] is DBNull))
                        f.MontoTotal = (decimal)datos.Lector["monto_total"];

                    if (!(datos.Lector["fecha_emision"] is DBNull))
                        f.FechaEmision = (DateTime)datos.Lector["fecha_emision"];

                    if (!(datos.Lector["activo"] is DBNull))
                        f.Activo = (bool)datos.Lector["activo"];

                    lista.Add(f);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Factura BuscarPorId(int idFactura)
        {
            AccesoDatos datos = new AccesoDatos();
            Factura f = null;

            try
            {
                datos.setearConsulta(@"
                    SELECT id_factura, id_turno, monto_base, cobertura_aplicada, 
                           descuento_aplicado, monto_total, fecha_emision, activo
                    FROM Factura
                    WHERE id_factura = @id AND activo = 1
                ");

                datos.setearParametros("@id", idFactura);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    f = new Factura();

                    f.IdFactura = (int)datos.Lector["id_factura"];

                    if (!(datos.Lector["id_turno"] is DBNull))
                        f.Turno = turnoNeg.BuscarPorId((int)datos.Lector["id_turno"]);

                    if (!(datos.Lector["monto_base"] is DBNull))
                        f.MontoBase = (decimal)datos.Lector["monto_base"];

                    if (!(datos.Lector["cobertura_aplicada"] is DBNull))
                        f.CoberturaAplicada = (decimal)datos.Lector["cobertura_aplicada"];

                    if (!(datos.Lector["descuento_aplicado"] is DBNull))
                        f.DescuentoAplicado = (decimal)datos.Lector["descuento_aplicado"];

                    if (!(datos.Lector["monto_total"] is DBNull))
                        f.MontoTotal = (decimal)datos.Lector["monto_total"];

                    if (!(datos.Lector["fecha_emision"] is DBNull))
                        f.FechaEmision = (DateTime)datos.Lector["fecha_emision"];

                    if (!(datos.Lector["activo"] is DBNull))
                        f.Activo = (bool)datos.Lector["activo"];
                }

                return f;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Agregar(Factura f)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Factura 
                    (id_turno, monto_base, cobertura_aplicada, descuento_aplicado, monto_total, fecha_emision, activo)
                    VALUES 
                    (@turno, @base, @cob, @desc, @total, @fecha, 1)
                ");

                datos.setearParametros("@turno", f.Turno.IdTurno);
                datos.setearParametros("@base", f.MontoBase);
                datos.setearParametros("@cob", f.CoberturaAplicada);
                datos.setearParametros("@desc", f.DescuentoAplicado);
                datos.setearParametros("@total", f.MontoTotal);
                datos.setearParametros("@fecha", f.FechaEmision);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Modificar(Factura f)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Factura 
                    SET id_turno = @turno,
                        monto_base = @base,
                        cobertura_aplicada = @cob, 
                        descuento_aplicado = @desc,
                        monto_total = @total,
                        fecha_emision = @fecha
                    WHERE id_factura = @id
                ");

                datos.setearParametros("@turno", f.Turno.IdTurno);
                datos.setearParametros("@base", f.MontoBase);
                datos.setearParametros("@cob", f.CoberturaAplicada);
                datos.setearParametros("@desc", f.DescuentoAplicado);
                datos.setearParametros("@total", f.MontoTotal);
                datos.setearParametros("@fecha", f.FechaEmision);
                datos.setearParametros("@id", f.IdFactura);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        // Baja lógica
        public void Eliminar(int idFactura)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Factura SET activo = 0 WHERE id_factura = @id");
                datos.setearParametros("@id", idFactura);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Reactivar(int idFactura)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Factura SET activo = 1 WHERE id_factura = @id");
                datos.setearParametros("@id", idFactura);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
