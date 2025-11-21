using System;
using System.Collections.Generic;
using System.Web.UI;
using negocio;
using modelo;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class ListadoEspecialidades : Page
    {
        protected GridView gvEspecialidades;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            try
            {
                gvEspecialidades.DataSource = negocio.Listar();
                gvEspecialidades.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        // ------------------------------------------------------------------
        // Métodos para Eventos (ABM)
        // ------------------------------------------------------------------

        // 1. Redirección para AGREGAR
        protected void btnNuevaEspecialidad_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioEspecialidad.aspx");
        }

        // 2. Redirección para MODIFICAR
        protected void gvEspecialidades_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int idSeleccionado = Convert.ToInt32(gvEspecialidades.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("FormularioEspecialidad.aspx?id=" + idSeleccionado);
        }

        // 3. Lógica para ELIMINAR
        protected void gvEspecialidades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            try
            {
                int idSeleccionado = Convert.ToInt32(gvEspecialidades.DataKeys[e.RowIndex].Value);
                negocio.Eliminar(idSeleccionado);
                CargarGrilla();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }
    }
}