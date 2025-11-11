using modelo;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProfesionales();
                CargarEspecialidades();
                CargarCoberturas();
            }
        }

        private void CargarProfesionales()
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT P.id_profesional, (Pe.nombre + ' ' + Pe.apellido) AS NombreCompleto FROM Profesional P INNER JOIN Persona Pe ON P.id_profesional = Pe.id_persona;");
                datos.ejecutarLectura();

                ddlProfesional.DataSource = datos.Lector;
                ddlProfesional.DataTextField = "NombreCompleto";
                ddlProfesional.DataValueField = "id_profesional";
                ddlProfesional.DataBind();
                datos.cerrarConexion();

                // agrega uun texto en defecto (como guía, sin valor)
                ddlProfesional.Items.Insert(0, new ListItem("Seleccione un/a profesional...", ""));

                // se asegura que se seleccione el texto en defecto
                ddlProfesional.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT id_especialidad, nombre FROM Especialidad");
                datos.ejecutarLectura();

                ddlEspecialidad.DataSource = datos.Lector;
                ddlEspecialidad.DataTextField = "nombre";
                ddlEspecialidad.DataValueField = "id_especialidad";
                ddlEspecialidad.DataBind();
                datos.cerrarConexion();

                ddlEspecialidad.Items.Insert(0, new ListItem("Seleccione una especialidad...", ""));
                ddlEspecialidad.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCoberturas()
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT id_obra_social, nombre FROM ObraSocial");
                datos.ejecutarLectura();

                ddlCobertura.DataSource = datos.Lector;
                ddlCobertura.DataTextField = "nombre";
                ddlCobertura.DataValueField = "id_obra_social";
                ddlCobertura.DataBind();
                datos.cerrarConexion();

                ddlCobertura.Items.Insert(0, new ListItem("Seleccione una cobertura...", ""));
                ddlEspecialidad.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string query = @"
            SELECT P.id_profesional, Pe.nombre + ' ' + Pe.apellido AS Profesional
            FROM Profesional P
            INNER JOIN Persona Pe ON P.id_profesional = Pe.id_persona
            WHERE 1=1";

            if (!string.IsNullOrEmpty(ddlProfesional.SelectedValue))
                query += " AND P.id_profesional = " + ddlProfesional.SelectedValue;

            if (!string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
                query += @" AND P.id_profesional IN (
                            SELECT id_profesional FROM Profesional_Especialidad 
                            WHERE id_especialidad = " + ddlEspecialidad.SelectedValue + ")";

            if (!string.IsNullOrEmpty(ddlCobertura.SelectedValue))
                query += @" AND P.id_profesional IN (
                            SELECT id_profesional FROM Profesional_ObraSocial 
                            WHERE id_obra_social = " + ddlCobertura.SelectedValue + ")";

            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta(query);
            datos.ejecutarLectura();

            gvResultados.DataSource = datos.Lector;
            gvResultados.DataBind();

            datos.cerrarConexion();
        }

    }
   }
