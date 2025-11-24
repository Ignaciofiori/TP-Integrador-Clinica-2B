using System;
using negocio;

namespace TP_Integrador_Clinica_WEB
{
    public partial class AgregarObraProfesional : System.Web.UI.Page
    {
        private int idProfesional;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("ListadoProfesionales.aspx");
                return;
            }

            idProfesional = int.Parse(Request.QueryString["id"]);

            if (!IsPostBack)
                CargarDropDown();
        }

        private void CargarDropDown()
        {
            ObraSocialNegocio negocio = new ObraSocialNegocio();
            ddlObras.DataSource = negocio.Listar();
            ddlObras.DataTextField = "Nombre";
            ddlObras.DataValueField = "IdObraSocial";
            ddlObras.DataBind();

            ddlObras.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione una obra social...", ""));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            // Validación: elegir obra social
            if (ddlObras.SelectedValue == "")
            {
                lblError.Visible = true;
                lblError.Text = "Debe seleccionar una obra social.";
                return;
            }

            int idObra = int.Parse(ddlObras.SelectedValue);

            ObraSocialNegocio negocio = new ObraSocialNegocio();

            // Validación: evitar relación duplicada
            if (negocio.ExisteRelacionActiva(idProfesional, idObra))
            {
                lblError.Visible = true;
                lblError.Text = "Este profesional ya tiene convenio con esta obra social.";
                return;
            }

            // Crear relación
            negocio.AgregarRelacion(idProfesional, idObra, DateTime.Now);

            Response.Redirect("ProfesionalObrasSociales.aspx?id=" + idProfesional, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesionalObrasSociales.aspx?id=" + idProfesional, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
