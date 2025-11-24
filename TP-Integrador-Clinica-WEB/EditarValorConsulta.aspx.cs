using System;
using negocio;

namespace TP_Integrador_Clinica_WEB
{
    public partial class EditarValorConsulta : System.Web.UI.Page
    {
        private int idProfesional;
        private int idEspecialidad;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null || Request.QueryString["idEsp"] == null)
                Response.Redirect("ListadoProfesionales.aspx");

            idProfesional = int.Parse(Request.QueryString["id"]);
            idEspecialidad = int.Parse(Request.QueryString["idEsp"]);

            if (!IsPostBack)
                CargarValorActual();
        }

        private void CargarValorActual()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            var relaciones = negocio.ListarPorProfesional(idProfesional);

            foreach (var esp in relaciones)
            {
                if (esp.IdEspecialidad == idEspecialidad)
                {
                    txtValor.Text = esp.ValorConsulta.ToString("0.##");
                    break;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            // Validación 1: valor numérico válido
            decimal nuevoValor;

            if (!decimal.TryParse(txtValor.Text.Trim(), out nuevoValor))
            {
                lblError.Visible = true;
                lblError.Text = "Ingrese un valor numérico válido.";
                return;
            }

            // Validación 2: que no sea negativo
            if (nuevoValor < 0)
            {
                lblError.Visible = true;
                lblError.Text = "El valor no puede ser negativo.";
                return;
            }

            // SI TODO OK → modificar la relación
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            negocio.ModificarValorConsulta(idProfesional, idEspecialidad, nuevoValor);

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
