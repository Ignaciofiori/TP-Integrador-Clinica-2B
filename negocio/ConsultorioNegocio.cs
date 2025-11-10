using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                datos.setearConsulta("SELECT id_consultorio, nombre, direccion, piso, numero_sala FROM Consultorio");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Consultorio cons = new Consultorio();

                    cons.Id = (int)datos.Lector["id_consultorio"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        cons.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        cons.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["piso"] is DBNull))
                        cons.Piso = (string)datos.Lector["piso"];

                    if (!(datos.Lector["numero_sala"] is DBNull))
                        cons.NumeroSala = (string)datos.Lector["numero_sala"];

                    lista.Add(cons);
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

        public Consultorio BuscarPorId(int id)
        {
            Consultorio aux = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT id_consultorio, nombre, direccion, piso, numero_sala
            FROM Consultorio
            WHERE id_consultorio = @id");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = new Consultorio();

                    aux.Id = (int)datos.Lector["id_consultorio"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        aux.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["piso"] is DBNull))
                        aux.Piso = (string)datos.Lector["piso"];

                    if (!(datos.Lector["numero_sala"] is DBNull))
                        aux.NumeroSala = (string)datos.Lector["numero_sala"];
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
                datos.setearConsulta(@"INSERT INTO Consultorio (nombre, direccion, piso, numero_sala) 
                                       VALUES (@nombre, @direccion, @piso, @nroSala)");

                datos.setearParametros("@nombre", cons.Nombre);
                datos.setearParametros("@direccion", cons.Direccion);
                datos.setearParametros("@piso", cons.Piso);
                datos.setearParametros("@nroSala", cons.NumeroSala);

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


        public void Modificar(Consultorio cons)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE Consultorio 
                                       SET nombre = @nombre, direccion = @direccion, piso = @piso, numero_sala = @nroSala
                                       WHERE id_consultorio = @id");

                datos.setearParametros("@nombre", cons.Nombre);
                datos.setearParametros("@direccion", cons.Direccion);
                datos.setearParametros("@piso", cons.Piso);
                datos.setearParametros("@nroSala", cons.NumeroSala);
                datos.setearParametros("@id", cons.Id);

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
                datos.setearConsulta("DELETE FROM Consultorio WHERE id_consultorio = @id");
                datos.setearParametros("@id", id);
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
    }

}
