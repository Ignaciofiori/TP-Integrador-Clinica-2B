using negocio;
using System;

namespace TP_Integrador_Clinica_WEB
{
    public partial class ListadoHorarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarHorarios();
        }

        private void CargarHorarios()
        {
            HorarioAtencionNegocio neg = new HorarioAtencionNegocio();
            gvHorarios.DataSource = neg.Listar();
            gvHorarios.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarHorario.aspx");
        }

        protected void gvHorarios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Ahora CommandArgument es el ID real
                int id = Convert.ToInt32(e.CommandArgument);

                HorarioAtencionNegocio neg = new HorarioAtencionNegocio();
                neg.Desactivar(id);

                CargarHorarios();
            }
        }

    }
}
