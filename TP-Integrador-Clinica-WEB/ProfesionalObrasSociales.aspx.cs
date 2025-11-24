using System;
using negocio;
using modelo;

namespace TP_Integrador_Clinica_WEB
{
    public partial class ProfesionalObrasSociales : System.Web.UI.Page
    {
        private int idProfesional;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Validación básica
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("ListadoProfesionales.aspx");
                return;
            }

            idProfesional = int.Parse(Request.QueryString["id"]);

            if (!IsPostBack)
                CargarGrilla();
        }

        private void CargarGrilla()
        {
            ObraSocialNegocio negocio = new ObraSocialNegocio();
            gvObrasSociales.DataSource = negocio.ListarPorProfesional(idProfesional);
            gvObrasSociales.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarObraProfesional.aspx?id=" + idProfesional, false);
        }

        protected void gvObrasSociales_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int idObra = int.Parse(e.CommandArgument.ToString());
            ObraSocialNegocio negocio = new ObraSocialNegocio();

            if (e.CommandName == "Eliminar")
            {
                negocio.BajaFisicaRelacion(idProfesional, idObra);
                CargarGrilla();
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarObraProfesional.aspx?idProf={idProfesional}&idObra={idObra}", false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoProfesionales.aspx", false);
        }
    }
}
