using modelo;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> listarClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP FROM CLIENTES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();

                    aux.Id = (int)datos.Lector["Id"];

                    if (datos.Lector["Documento"] != DBNull.Value)
                        aux.Documento = (string)datos.Lector["Documento"];

                    if (datos.Lector["Nombre"] != DBNull.Value)
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (datos.Lector["Apellido"] != DBNull.Value)
                        aux.Apellido = (string)datos.Lector["Apellido"];

                    if (datos.Lector["Email"] != DBNull.Value)
                        aux.Email = (string)datos.Lector["Email"];

                    if (datos.Lector["Direccion"] != DBNull.Value)
                        aux.Direccion = (string)datos.Lector["Direccion"];

                    if (datos.Lector["Ciudad"] != DBNull.Value)
                        aux.Ciudad = (string)datos.Lector["Ciudad"];

                    if (datos.Lector["CP"] != DBNull.Value)
                        aux.CP = (int)datos.Lector["CP"];

                    listaClientes.Add(aux);
                }

                return listaClientes;
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

        public Cliente buscarPorId(int id)
        {
            Cliente cliente = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP FROM CLIENTES WHERE Id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cliente = new Cliente();

                    cliente.Id = (int)datos.Lector["Id"];

                    if (datos.Lector["Documento"] != DBNull.Value)
                        cliente.Documento = (string)datos.Lector["Documento"];

                    if (datos.Lector["Nombre"] != DBNull.Value)
                        cliente.Nombre = (string)datos.Lector["Nombre"];

                    if (datos.Lector["Apellido"] != DBNull.Value)
                        cliente.Apellido = (string)datos.Lector["Apellido"];

                    if (datos.Lector["Email"] != DBNull.Value)
                        cliente.Email = (string)datos.Lector["Email"];

                    if (datos.Lector["Direccion"] != DBNull.Value)
                        cliente.Direccion = (string)datos.Lector["Direccion"];

                    if (datos.Lector["Ciudad"] != DBNull.Value)
                        cliente.Ciudad = (string)datos.Lector["Ciudad"];

                    if (datos.Lector["CP"] != DBNull.Value)
                        cliente.CP = (int)datos.Lector["CP"];
                }

                return cliente;
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

        public Cliente BuscarPorDNI(string DNI)
        {
            Cliente cliente = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP FROM CLIENTES WHERE Documento = @DNI");
                datos.setearParametros("@DNI", DNI);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cliente = new Cliente();

                    cliente.Id = (int)datos.Lector["Id"];

                    if (datos.Lector["Documento"] != DBNull.Value)
                        cliente.Documento = (string)datos.Lector["Documento"];

                    if (datos.Lector["Nombre"] != DBNull.Value)
                        cliente.Nombre = (string)datos.Lector["Nombre"];

                    if (datos.Lector["Apellido"] != DBNull.Value)
                        cliente.Apellido = (string)datos.Lector["Apellido"];

                    if (datos.Lector["Email"] != DBNull.Value)
                        cliente.Email = (string)datos.Lector["Email"];

                    if (datos.Lector["Direccion"] != DBNull.Value)
                        cliente.Direccion = (string)datos.Lector["Direccion"];

                    if (datos.Lector["Ciudad"] != DBNull.Value)
                        cliente.Ciudad = (string)datos.Lector["Ciudad"];

                    if (datos.Lector["CP"] != DBNull.Value)
                        cliente.CP = (int)datos.Lector["CP"];
                }

                return cliente;
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

        public int RegistrarCliente(Cliente nuevoCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"INSERT INTO CLIENTES (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP)
                                    VALUES (@Documento, @Nombre, @Apellido, @Email, @Direccion, @Ciudad, @CP);
                                    SELECT SCOPE_IDENTITY()");

                datos.setearParametros("@Documento", nuevoCliente.Documento);
                datos.setearParametros("@Nombre", nuevoCliente.Nombre);
                datos.setearParametros("@Apellido", nuevoCliente.Apellido);
                datos.setearParametros("@Email", nuevoCliente.Email);
                datos.setearParametros("@Direccion", nuevoCliente.Direccion);
                datos.setearParametros("@Ciudad", nuevoCliente.Ciudad);
                datos.setearParametros("@CP", nuevoCliente.CP);

                datos.ejecutarAccion();

                return datos.ejecutarAccionScalar();
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

