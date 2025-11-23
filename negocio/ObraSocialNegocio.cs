using System;
using System.Collections.Generic;
using modelo;

namespace negocio
{
    public class ObraSocialNegocio
    {

        public List<ObraSocial> Listar()
        {
            List<ObraSocial> lista = new List<ObraSocial>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_obra_social, nombre, porcentaje_cobertura,
                           telefono, direccion, activo
                    FROM ObraSocial
                    WHERE activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObraSocial os = new ObraSocial();

                    os.IdObraSocial = (int)datos.Lector["id_obra_social"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        os.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["porcentaje_cobertura"] is DBNull))
                        os.PorcentajeCobertura = (decimal)datos.Lector["porcentaje_cobertura"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        os.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        os.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        os.Activo = (bool)datos.Lector["activo"];

                    lista.Add(os);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public ObraSocial BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_obra_social, nombre, porcentaje_cobertura,
                           telefono, direccion, activo
                    FROM ObraSocial
                    WHERE id_obra_social = @id AND activo = 1");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    ObraSocial obra = new ObraSocial();

                    obra.IdObraSocial = (int)datos.Lector["id_obra_social"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        obra.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["porcentaje_cobertura"] is DBNull))
                        obra.PorcentajeCobertura = (decimal)datos.Lector["porcentaje_cobertura"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        obra.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        obra.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        obra.Activo = (bool)datos.Lector["activo"];

                    return obra;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

 
        public void Agregar(ObraSocial obra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO ObraSocial
                    (nombre, porcentaje_cobertura, telefono, direccion, activo)
                    VALUES (@nom, @porc, @tel, @dir, 1)");

                datos.setearParametros("@nom", obra.Nombre);
                datos.setearParametros("@porc", obra.PorcentajeCobertura);
                datos.setearParametros("@tel", obra.Telefono);
                datos.setearParametros("@dir", obra.Direccion);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Modificar(ObraSocial obra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE ObraSocial
                    SET nombre = @nom,
                        porcentaje_cobertura = @porc,
                        telefono = @tel,
                        direccion = @dir
                    WHERE id_obra_social = @id");

                datos.setearParametros("@nom", obra.Nombre);
                datos.setearParametros("@porc", obra.PorcentajeCobertura);
                datos.setearParametros("@tel", obra.Telefono);
                datos.setearParametros("@dir", obra.Direccion);
                datos.setearParametros("@id", obra.IdObraSocial);

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
                datos.setearConsulta("UPDATE ObraSocial SET activo = 0 WHERE id_obra_social = @id");
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
                datos.setearConsulta("UPDATE ObraSocial SET activo = 1 WHERE id_obra_social = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<ObraSocial> ListarPorProfesional(int idProfesional)
        {
            List<ObraSocial> lista = new List<ObraSocial>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT o.id_obra_social, o.nombre, o.porcentaje_cobertura,
                           o.telefono, o.direccion, o.activo,
                           pos.convenio_activo, pos.fecha_inicio
                    FROM Profesional_ObraSocial pos
                    INNER JOIN ObraSocial o ON pos.id_obra_social = o.id_obra_social
                    WHERE pos.id_profesional = @id AND pos.activo = 1");

                datos.setearParametros("@id", idProfesional);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObraSocial os = new ObraSocial();

                    os.IdObraSocial = (int)datos.Lector["id_obra_social"];
                    if (!(datos.Lector["nombre"] is DBNull)) os.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["porcentaje_cobertura"] is DBNull)) os.PorcentajeCobertura = (decimal)datos.Lector["porcentaje_cobertura"];
                    if (!(datos.Lector["telefono"] is DBNull)) os.Telefono = (string)datos.Lector["telefono"];
                    if (!(datos.Lector["direccion"] is DBNull)) os.Direccion = (string)datos.Lector["direccion"];
                    if (!(datos.Lector["activo"] is DBNull)) os.Activo = (bool)datos.Lector["activo"];

                    lista.Add(os);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void AgregarRelacion(int idProfesional, int idObraSocial, DateTime fechaInicio)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Profesional_ObraSocial
                    (id_profesional, id_obra_social, convenio_activo, fecha_inicio, activo)
                    VALUES (@prof, @obra, 1, @fec, 1)");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@obra", idObraSocial);
                datos.setearParametros("@fec", fechaInicio);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void BajaLogicaRelacion(int idProfesional, int idObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Profesional_ObraSocial
                    SET activo = 0
                    WHERE id_profesional = @prof AND id_obra_social = @obra");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@obra", idObraSocial);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public void ReactivarRelacion(int idProfesional, int idObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Profesional_ObraSocial
                    SET activo = 1
                    WHERE id_profesional = @prof AND id_obra_social = @obra");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@obra", idObraSocial);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }


}
