using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using modelo; // 

namespace TP_Integrador_Clinica_WEB
{
    public partial class ListadoTurnos : System.Web.UI.Page
    {
        
        
        protected GridView gvTurnos;
        protected DropDownList ddlCampo;
        protected TextBox txtFiltro;

        private TurnoNegocio turnoNegocio = new TurnoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            try
            {
          
                gvTurnos.DataSource = turnoNegocio.Listar("Pendiente");
                gvTurnos.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de error.
                Response.Write($"Error al cargar la grilla de turnos: {ex.Message}");
            }
        }

        // --- MANEJADORES DE COMANDOS DEL GRIDVIEW ---

        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idTurno = Convert.ToInt32(e.CommandArgument);

            try
            {
                if (e.CommandName == "Asistir")
                {
                    turnoNegocio.CambiarEstado(idTurno, "Asistido");
                    CargarGrilla();
                }
                else if (e.CommandName == "Cancelar")
                {
                    turnoNegocio.CambiarEstado(idTurno, "Cancelado");
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error al actualizar el estado del turno: " + ex.Message);
            }
        }


        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Lógica pendiente: Ocultar los botones de acción si el turno NO es "Pendiente".
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Se requiere la propiedad 'Estado' en el objeto Turno para esta lógica.
                // modelo.Turno turno = (modelo.Turno)e.Row.DataItem; 
                // LinkButton btnCancelar = (LinkButton)e.Row.FindControl("btnCancelar");
                // LinkButton btnAsistido = (LinkButton)e.Row.FindControl("btnAsistido");

                // if (turno != null && turno.Estado != "Pendiente")
                // {
                //     if (btnCancelar != null) btnCancelar.Visible = false;
                //     if (btnAsistido != null) btnAsistido.Visible = false;
                // }
            }
        }

        // --- BOTONES DE ACCIÓN Y NAVEGACIÓN ---

        protected void btnEstado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string estado = btn.CommandArgument;

            Response.Redirect($"TurnosEstado.aspx?estado={estado}");
        }
            
        // --- MANEJADORES DE BÚSQUEDA DINÁMICA ---

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lógica pendiente: Ajustar el modo del TextBox de filtro.
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string campo = ddlCampo.SelectedValue;
            string filtro = txtFiltro.Text.Trim();

            if (campo == "0" || string.IsNullOrEmpty(filtro))
            {
                CargarGrilla();
                return;
            }

            // --- VALIDACIÓN DE MONTOS ---
            if (campo == "MontoMayor" || campo == "MontoMenor")
            {
                decimal monto;

                if (!decimal.TryParse(filtro, out monto) || monto <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(),
                        "alertMonto", "alert('Debe ingresar un monto válido y mayor a 0.');", true);

                    return;
                }
            }

            gvTurnos.DataSource = turnoNegocio.Buscar(campo, filtro, "pendiente");
            gvTurnos.DataBind();
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            ddlCampo.SelectedIndex = 0;

            CargarGrilla();
        }
        protected void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarTurno.aspx");
        }

        protected void btnNuevoHorario_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarHorario.aspx");
        }
    }

}