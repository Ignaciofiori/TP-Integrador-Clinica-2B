using System;
using System.Web.UI;
using negocio;

namespace TP_Integrador_Clinica_WEB
{
    public partial class TurnosEstado : Page
    {
        private TurnoNegocio turnoNegocio = new TurnoNegocio();
        private string estado;

        protected void Page_Load(object sender, EventArgs e)
        {
            estado = Request.QueryString["estado"]?.ToLower();

            if (string.IsNullOrEmpty(estado))
                estado = "asistido"; // fallback

            litTitulo.Text = estado == "asistido" ?
                "Turnos Asistidos" :
                "Turnos Cancelados";

            if (!IsPostBack)
                CargarTurnos();
        }

        private void CargarTurnos()
        {
            gvTurnosEstado.DataSource = turnoNegocio.Listar(estado);
            gvTurnosEstado.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string campo = ddlCampo.SelectedValue;
            string filtro = txtFiltro.Text.Trim();

            // Validación de monto
            if ((campo == "MontoMayor" || campo == "MontoMenor"))
            {
                decimal monto;
                if (!decimal.TryParse(filtro, out monto) || monto <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(),
                        "alertMonto", "alert('Debe ingresar un monto válido y mayor a 0.');", true);
                    return;
                }
            }

            gvTurnosEstado.DataSource = turnoNegocio.Buscar(campo, filtro, estado);
            gvTurnosEstado.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCampo.SelectedIndex = 0;
            txtFiltro.Text = "";
            CargarTurnos();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoTurnos.aspx");
        }
    }
}
