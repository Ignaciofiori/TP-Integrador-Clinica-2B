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
                ProfesionalNegocio profNeg = new ProfesionalNegocio();
                List<Profesional> profesionales = profNeg.Listar();

                foreach (var p in profesionales)
                {
                    //concateno para mostrar
                    p.Nombre = p.Nombre + " " + p.Apellido;
                }

                ddlProfesional.DataSource = profesionales;
                ddlProfesional.DataTextField = "Nombre"; 
                ddlProfesional.DataValueField = "Id";
                ddlProfesional.DataBind();

                ddlProfesional.Items.Insert(0, new ListItem("Seleccione un/a profesional...", ""));
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
                EspecialidadNegocio espNeg = new EspecialidadNegocio();

                ddlEspecialidad.DataSource = espNeg.Listar();
                ddlEspecialidad.DataTextField = "Nombre";
                ddlEspecialidad.DataValueField = "Id";
                ddlEspecialidad.DataBind();

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
                ObraSocialNegocio obraNeg = new ObraSocialNegocio();

                ddlCobertura.DataSource = obraNeg.Listar();
                ddlCobertura.DataTextField = "Nombre";
                ddlCobertura.DataValueField = "Id";
                ddlCobertura.DataBind();

                ddlCobertura.Items.Insert(0, new ListItem("Seleccione una cobertura...", ""));
                ddlCobertura.SelectedIndex = 0;
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
