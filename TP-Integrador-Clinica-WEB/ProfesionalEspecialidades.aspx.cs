using negocio;
using System;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class ProfesionalEspecialidades : System.Web.UI.Page
    {
        private int idProfesional;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("ListadoProfesionales.aspx");

            idProfesional = int.Parse(Request.QueryString["id"]);

            if (!IsPostBack)
                CargarEspecialidades();
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            gvEspecialidades.DataSource = negocio.ListarPorProfesional(idProfesional);
            gvEspecialidades.DataBind();
        }

        protected void gvEspecialidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idEspecialidad = int.Parse(e.CommandArgument.ToString());

                EspecialidadNegocio negocio = new EspecialidadNegocio();
                negocio.BajaFisicaRelacion(idProfesional, idEspecialidad);

                CargarEspecialidades();
            }

            if (e.CommandName == "EditarValor")
            {
                int idEspecialidad = int.Parse(e.CommandArgument.ToString());

                Response.Redirect("EditarValorConsulta.aspx?id=" + idProfesional + "&idEsp=" + idEspecialidad);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEspecialidadProfesional.aspx?id=" + idProfesional);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoProfesionales.aspx");
        }
    }
}
