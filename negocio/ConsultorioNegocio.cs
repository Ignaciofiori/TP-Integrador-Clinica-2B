using System;
using System.Collections.Generic;
using modelo;

namespace negocio
{
    public class ConsultorioNegocio
    {
        public List<Consultorio> Listar()
        {
            List<Consultorio> lista = new List<Consultorio>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_consultorio, nombre, direccion, piso, numero_sala, activo
                    FROM Consultorio
                    WHERE activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Consultorio cons = new Consultorio();

                    cons.IdConsultorio = (int)datos.Lector["id_consultorio"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        cons.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        cons.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["piso"] is DBNull))
                        cons.Piso = (string)datos.Lector["piso"];

                    if (!(datos.Lector["numero_sala"] is DBNull))
                        cons.NumeroSala = (string)datos.Lector["numero_sala"];

                    if (!(datos.Lector["activo"] is DBNull))
                        cons.Activo = (bool)datos.Lector["activo"];

                    lista.Add(cons);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Consultorio BuscarPorId(int id)
        {
            Consultorio aux = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_consultorio, nombre, direccion, piso, numero_sala, activo
                    FROM Consultorio
                    WHERE id_consultorio = @id AND activo = 1");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = new Consultorio();

                    aux.IdConsultorio = (int)datos.Lector["id_consultorio"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        aux.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["piso"] is DBNull))
                        aux.Piso = (string)datos.Lector["piso"];

                    if (!(datos.Lector["numero_sala"] is DBNull))
                        aux.NumeroSala = (string)datos.Lector["numero_sala"];

                    if (!(datos.Lector["activo"] is DBNull))
                        aux.Activo = (bool)datos.Lector["activo"];
                }

                return aux;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Agregar(Consultorio cons)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Consultorio
                    (nombre, direccion, piso, numero_sala, activo)
                    VALUES
                    (@nombre, @direccion, @piso, @nroSala, 1)");

                datos.setearParametros("@nombre", cons.Nombre);
                datos.setearParametros("@direccion", cons.Direccion);
                datos.setearParametros("@piso", cons.Piso);
                datos.setearParametros("@nroSala", cons.NumeroSala);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Modificar(Consultorio cons)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Consultorio
                    SET nombre = @nombre,
                        direccion = @direccion,
                        piso = @piso,
                        numero_sala = @nroSala
                    WHERE id_consultorio = @id");

                datos.setearParametros("@nombre", cons.Nombre);
                datos.setearParametros("@direccion", cons.Direccion);
                datos.setearParametros("@piso", cons.Piso);
                datos.setearParametros("@nroSala", cons.NumeroSala);
                datos.setearParametros("@id", cons.IdConsultorio);

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
                datos.setearConsulta("UPDATE Consultorio SET activo = 0 WHERE id_consultorio = @id");
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
                datos.setearConsulta("UPDATE Consultorio SET activo = 1 WHERE id_consultorio = @id");
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
