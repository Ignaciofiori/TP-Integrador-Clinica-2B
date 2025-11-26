using modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace negocio
{
    public class FacturaNegocio
    {
        private  TurnoNegocio turnoNeg = new TurnoNegocio();

        public List<FacturaListado> Listar()
        {
            List<FacturaListado> lista = new List<FacturaListado>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 
                f.id_factura,
                f.monto_base,
                f.cobertura_aplicada,
                f.descuento_aplicado,
                f.monto_total,
                f.fecha_emision,

                t.id_turno,
                t.fecha_turno,
                t.hora_turno,
                t.estado,

                os.nombre AS obra_social_nombre,

                (p.apellido + ', ' + p.nombre) AS paciente_nombre_completo,

                (prof.apellido + ', ' + prof.nombre) AS profesional_nombre_completo,

                esp.nombre AS especialidad_nombre,

                (cons.nombre + ' - Sala ' + cons.numero_sala) AS consultorio_display

            FROM Factura f
                INNER JOIN Turno t ON t.id_turno = f.id_turno
                INNER JOIN Paciente p ON p.id_paciente = t.id_paciente
                LEFT JOIN ObraSocial os ON os.id_obra_social = t.id_obra_social
                INNER JOIN HorarioAtencion h ON h.id_horario = t.id_horario
                INNER JOIN Profesional prof ON prof.id_profesional = h.id_profesional
                INNER JOIN Especialidad esp ON esp.id_especialidad = h.id_especialidad
                INNER JOIN Consultorio cons ON cons.id_consultorio = h.id_consultorio
            WHERE f.activo = 1
            ORDER BY f.fecha_emision DESC;
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FacturaListado f = new FacturaListado
                    {
                        IdFactura = (int)datos.Lector["id_factura"],

                        MontoBase = (decimal)datos.Lector["monto_base"],
                        CoberturaAplicada = (decimal)datos.Lector["cobertura_aplicada"],
                        DescuentoAplicado = (decimal)datos.Lector["descuento_aplicado"],
                        MontoTotal = (decimal)datos.Lector["monto_total"],

                        FechaEmision = (DateTime)datos.Lector["fecha_emision"],

                        IdTurno = (int)datos.Lector["id_turno"],
                        FechaTurno = (DateTime)datos.Lector["fecha_turno"],
                        HoraTurno = (TimeSpan)datos.Lector["hora_turno"],
                        EstadoTurno = datos.Lector["estado"].ToString(),

                        PacienteNombreCompleto = datos.Lector["paciente_nombre_completo"].ToString(),
                        ProfesionalNombreCompleto = datos.Lector["profesional_nombre_completo"].ToString(),
                        EspecialidadNombre = datos.Lector["especialidad_nombre"].ToString(),
                        ObraSocialNombre = datos.Lector["obra_social_nombre"].ToString(),
                        ConsultorioDisplay = datos.Lector["consultorio_display"].ToString()
                    };

                    lista.Add(f);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<FacturaListado> Buscar(string criterio, string valor)
        {
            List<FacturaListado> lista = new List<FacturaListado>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"
            SELECT 
                f.id_factura,
                f.monto_base,
                f.cobertura_aplicada,
                f.descuento_aplicado,
                f.monto_total,
                f.fecha_emision,

                t.id_turno,
                t.fecha_turno,
                t.hora_turno,
                t.estado,

                os.nombre AS obra_social_nombre,
                (p.apellido + ', ' + p.nombre) AS paciente_nombre_completo,
                (prof.apellido + ', ' + prof.nombre) AS profesional_nombre_completo,
                esp.nombre AS especialidad_nombre,
                (cons.nombre + ' - Sala ' + cons.numero_sala) AS consultorio_display

            FROM Factura f
                INNER JOIN Turno t ON t.id_turno = f.id_turno
                INNER JOIN Paciente p ON p.id_paciente = t.id_paciente
                LEFT JOIN ObraSocial os ON os.id_obra_social = t.id_obra_social
                INNER JOIN HorarioAtencion h ON h.id_horario = t.id_horario
                INNER JOIN Profesional prof ON prof.id_profesional = h.id_profesional
                INNER JOIN Especialidad esp ON esp.id_especialidad = h.id_especialidad
                INNER JOIN Consultorio cons ON cons.id_consultorio = h.id_consultorio
            WHERE f.activo = 1
        ";

                // -----------------------------------------
                // FILTRO SEGÚN CRITERIO
                // -----------------------------------------

                switch (criterio)
                {
                    case "Paciente":
                        query += " AND (p.apellido + ', ' + p.nombre) LIKE @valor";
                        break;

                    case "Profesional":
                        query += " AND (prof.apellido + ', ' + prof.nombre) LIKE @valor";
                        break;

                    case "Especialidad":
                        query += " AND esp.nombre LIKE @valor";
                        break;

                    case "ObraSocial":
                        query += " AND os.nombre LIKE @valor";
                        break;

                    case "MontoMayor":
                        query += " AND f.monto_total > @monto";
                        break;

                    case "MontoMenor":
                        query += " AND f.monto_total < @monto";
                        break;
                }

                query += " ORDER BY f.fecha_emision DESC";

                datos.setearConsulta(query);

                // Parámetros según criterio
                if (criterio == "MontoMayor" || criterio == "MontoMenor")
                {
                    decimal monto;
                    if (!decimal.TryParse(valor, out monto))
                        throw new Exception("Debe ingresar un valor numérico.");

                    datos.setearParametros("@monto", monto);
                }
                else
                {
                    datos.setearParametros("@valor", "%" + valor + "%");
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(LeerFacturaListado(datos.Lector));
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
        private FacturaListado LeerFacturaListado(SqlDataReader lector)
        {
            return new FacturaListado
            {
                IdFactura = (int)lector["id_factura"],
                MontoBase = (decimal)lector["monto_base"],
                CoberturaAplicada = (decimal)lector["cobertura_aplicada"],
                DescuentoAplicado = (decimal)lector["descuento_aplicado"],
                MontoTotal = (decimal)lector["monto_total"],
                FechaEmision = (DateTime)lector["fecha_emision"],

                IdTurno = (int)lector["id_turno"],
                FechaTurno = (DateTime)lector["fecha_turno"],
                HoraTurno = (TimeSpan)lector["hora_turno"],
                EstadoTurno = lector["estado"].ToString(),

                PacienteNombreCompleto = lector["paciente_nombre_completo"].ToString(),
                ProfesionalNombreCompleto = lector["profesional_nombre_completo"].ToString(),
                EspecialidadNombre = lector["especialidad_nombre"].ToString(),
                ObraSocialNombre = lector["obra_social_nombre"].ToString(),
                ConsultorioDisplay = lector["consultorio_display"].ToString()
            };
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
