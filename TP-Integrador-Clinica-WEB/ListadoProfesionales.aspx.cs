using negocio;
using System;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class ListadoProfesionales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProfesionales();
        }

        private void CargarProfesionales()
        {
            ProfesionalNegocio negocio = new ProfesionalNegocio();
            gvProfesionales.DataSource = negocio.Listar();
            gvProfesionales.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioProfesional.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = ddlCriterio.SelectedValue;
            string filtro = txtFiltro.Text.Trim();

            ProfesionalNegocio negocio = new ProfesionalNegocio();

            gvProfesionales.DataSource = negocio.Buscar(criterio, filtro);
            gvProfesionales.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            ddlCriterio.SelectedIndex = 0;
            CargarProfesionales();
        }

        protected void gvProfesionales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idProfesional = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Convenios")
            {
                Response.Redirect("ProfesionalObrasSociales.aspx?id=" + idProfesional);
            }
            else if (e.CommandName == "Especialidades")
            {
                Response.Redirect("ProfesionalEspecialidades.aspx?id=" + idProfesional);
            }
        }
    }
}
