using modelo;
using negocio;
using System;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class AgregarEspecialidadProfesional : System.Web.UI.Page
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

            ddlEspecialidad.DataSource = negocio.Listar(); // solo activas
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione una especialidad...", ""));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            // Validación 1: elegir especialidad
            if (ddlEspecialidad.SelectedValue == "")
            {
                lblError.Visible = true;
                lblError.Text = "Seleccione una especialidad.";
                return;
            }

            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            EspecialidadNegocio negocio = new EspecialidadNegocio();

            // Validación 2: evitar duplicados
            if (negocio.ExisteRelacionActiva(idProfesional, idEspecialidad))
            {
                lblError.Visible = true;
                lblError.Text = "Esta especialidad ya está asignada al profesional.";
                return;
            }

            // Validación 3: valor coherente
            decimal valor;

            // Primero parseamos el valor del textbox
            if (!decimal.TryParse(txtValor.Text.Trim(), out valor))
            {
                lblError.Visible = true;
                lblError.Text = "Ingrese un valor numérico válido.";
                return;
            }

            if (valor < 0)
            {
                lblError.Visible = true;
                lblError.Text = "El valor no puede ser negativo.";
                return;
            }

            // OK → agregar relación
            negocio.AgregarRelacion(idProfesional, idEspecialidad, valor);

            Response.Redirect("ProfesionalEspecialidades.aspx?id=" + idProfesional, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesionalEspecialidades.aspx?id=" + idProfesional, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
