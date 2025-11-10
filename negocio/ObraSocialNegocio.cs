using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                datos.setearConsulta("SELECT id_obra_social, nombre, porcentaje_cobertura, telefono, direccion FROM ObraSocial");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObraSocial obra = new ObraSocial();

                    obra.Id = (int)datos.Lector["id_obra_social"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        obra.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["porcentaje_cobertura"] is DBNull))
                        obra.PorcentajeCobertura = (decimal)datos.Lector["porcentaje_cobertura"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        obra.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        obra.Direccion = (string)datos.Lector["direccion"];

                    lista.Add(obra);
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

        public List<ObraSocial> ListarPorProfesional(int idProfesional)
        {
            List<ObraSocial> lista = new List<ObraSocial>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT o.id_obra_social, o.nombre, o.porcentaje_cobertura, o.telefono, o.direccion
                    FROM Profesional_ObraSocial pos
                    INNER JOIN ObraSocial o ON pos.id_obra_social = o.id_obra_social
                    WHERE pos.id_profesional = @id");

                datos.setearParametros("@id", idProfesional);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObraSocial aux = new ObraSocial();

                    aux.Id = (int)datos.Lector["id_obra_social"];
                    if (!(datos.Lector["nombre"] is DBNull)) aux.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["porcentaje_cobertura"] is DBNull)) aux.PorcentajeCobertura = (decimal)datos.Lector["porcentaje_cobertura"];
                    if (!(datos.Lector["telefono"] is DBNull)) aux.Telefono = (string)datos.Lector["telefono"];
                    if (!(datos.Lector["direccion"] is DBNull)) aux.Direccion = (string)datos.Lector["direccion"];

                    lista.Add(aux);
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
                datos.setearConsulta("SELECT id_obra_social, nombre, porcentaje_cobertura, telefono, direccion FROM ObraSocial WHERE id_obra_social = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    ObraSocial obra = new ObraSocial();

                    obra.Id = (int)datos.Lector["id_obra_social"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        obra.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["porcentaje_cobertura"] is DBNull))
                        obra.PorcentajeCobertura = (decimal)datos.Lector["porcentaje_cobertura"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        obra.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        obra.Direccion = (string)datos.Lector["direccion"];

                    return obra;
                }

                return null;
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


        public void Agregar(ObraSocial obra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"INSERT INTO ObraSocial (nombre, porcentaje_cobertura, telefono, direccion) 
                                       VALUES (@nombre, @porcentaje, @telefono, @direccion)");

                datos.setearParametros("@nombre", obra.Nombre);
                datos.setearParametros("@porcentaje", obra.PorcentajeCobertura);
                datos.setearParametros("@telefono", obra.Telefono);
                datos.setearParametros("@direccion", obra.Direccion);

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


        public void AgregarRelacion(int idProfesional, int idObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Profesional_ObraSocial (id_profesional, id_obra_social) VALUES (@prof, @obra)");
                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@obra", idObraSocial);

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
                datos.setearConsulta(@"UPDATE ObraSocial 
                                       SET nombre = @nombre, porcentaje_cobertura = @porcentaje, telefono = @telefono, direccion = @direccion
                                       WHERE id_obra_social = @id");

                datos.setearParametros("@nombre", obra.Nombre);
                datos.setearParametros("@porcentaje", obra.PorcentajeCobertura);
                datos.setearParametros("@telefono", obra.Telefono);
                datos.setearParametros("@direccion", obra.Direccion);
                datos.setearParametros("@id", obra.Id);

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
                datos.setearConsulta("DELETE FROM ObraSocial WHERE id_obra_social = @id");
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
    

        public void EliminarRelacion(int idProfesional, int idObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Profesional_ObraSocial WHERE id_profesional = @prof AND id_obra_social = @obra");
                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@obra", idObraSocial);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

     
        public void EliminarRelacionLogica(int idProfesional, int idObraSocial, bool activo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Profesional_ObraSocial SET convenio_activo = @activo WHERE id_profesional = @prof AND id_obra_social = @obra");
                datos.setearParametros("@activo", activo);
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
