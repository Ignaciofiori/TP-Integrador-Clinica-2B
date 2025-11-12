using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelo;

namespace negocio
{
    public class FacturaNegocio
    {
        public List<Factura> Listar()
        {
            List<Factura> lista = new List<Factura>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT f.id_factura, f.id_turno, f.monto_base, f.cobertura_aplicada, 
                           f.descuento_aplicado, f.monto_total, f.fecha_emision,
                           t.id_turno AS turno_id
                    FROM Factura f
                    INNER JOIN Turno t ON f.id_turno = t.id_turno
                ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Factura f = new Factura();
                    f.Id = (int)datos.Lector["id_factura"];

                    f.Turno = new Turno
                    {
                        Id = (int)datos.Lector["id_turno"]

                        //aca podemos aniadir mas propiedades del turno si es necesario
                    };

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

                    lista.Add(f);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Factura BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Factura f = null;

            try
            {
                datos.setearConsulta(@"
                    SELECT f.id_factura, f.id_turno, f.monto_base, f.cobertura_aplicada, 
                           f.descuento_aplicado, f.monto_total, f.fecha_emision,
                           t.id_turno AS turno_id
                    FROM Factura f
                    INNER JOIN Turno t ON f.id_turno = t.id_turno
                    WHERE f.id_factura = @id
                ");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    f = new Factura();
                    f.Id = (int)datos.Lector["id_factura"];

                    f.Turno = new Turno
                    {
                        Id = (int)datos.Lector["id_turno"]
                    };

                    f.MontoBase = (decimal)datos.Lector["monto_base"];
                    f.CoberturaAplicada = (decimal)datos.Lector["cobertura_aplicada"];
                    f.DescuentoAplicado = (decimal)datos.Lector["descuento_aplicado"];
                    f.MontoTotal = (decimal)datos.Lector["monto_total"];
                    f.FechaEmision = (DateTime)datos.Lector["fecha_emision"];
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
                    INSERT INTO Factura (id_turno, monto_base, cobertura_aplicada, descuento_aplicado, monto_total, fecha_emision)
                    VALUES (@turno, @base, @cob, @desc, @total, @fecha)
                ");

                datos.setearParametros("@turno", f.Turno.Id);
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
                    SET id_turno = @turno, monto_base = @base, cobertura_aplicada = @cob, 
                        descuento_aplicado = @desc, monto_total = @total, fecha_emision = @fecha
                    WHERE id_factura = @id
                ");

                datos.setearParametros("@turno", f.Turno.Id);
                datos.setearParametros("@base", f.MontoBase);
                datos.setearParametros("@cob", f.CoberturaAplicada);
                datos.setearParametros("@desc", f.DescuentoAplicado);
                datos.setearParametros("@total", f.MontoTotal);
                datos.setearParametros("@fecha", f.FechaEmision);
                datos.setearParametros("@id", f.Id);

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
                datos.setearConsulta("DELETE FROM Factura WHERE id_factura = @id");
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
