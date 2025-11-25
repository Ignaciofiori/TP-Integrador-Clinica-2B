using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using modelo; // 🛑 Asegúrate de que esta referencia sea correcta

namespace TP_Integrador_Clinica_WEB
{
    public partial class ListadoTurnos : System.Web.UI.Page
    {
        // 🛑 DECLARACIÓN DE CONTROLES (Asegúrate de que los ID coincidan con tu ASPX)
        // Si tu GridView se llama gvTurnos en el ASPX, usa gvTurnos aquí.
        protected GridView gvTurnos;
        protected DropDownList ddlCampo;
        protected TextBox txtFiltro;

        private TurnoNegocio turnoNegocio = new TurnoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // La grilla se carga con todos los turnos, ya que Listar() no acepta filtros.
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            try
            {
                // Llama al método Listar sin parámetros (método existente).
                gvTurnos.DataSource = turnoNegocio.Listar();
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

            // 🛑 LÓGICA DE CANCELAR (asumimos que este método SÍ existe)
            if (e.CommandName == "CancelarTurno")
            {
                try
                {
                    // Llama al método de cancelación de la capa de negocio
                    turnoNegocio.CancelarTurno(idTurno);
                    CargarGrilla(); // Recarga la grilla
                }
                catch (Exception ex)
                {
                    Response.Write($"Error al cancelar el turno: {ex.Message}");
                }
            }

            // 🛑 MarcarAsistido (La lógica queda pendiente para tu compañero, solo recargamos)
            else if (e.CommandName == "MarcarAsistido")
            {
                // Aquí tu compañero debe implementar turnoNegocio.MarcarAsistido(idTurno);
                CargarGrilla();
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

        protected void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarTurno.aspx");
        }

        protected void btnNuevoHorario_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarHorario.aspx");
        }

        // Manejador para los botones de Ver Pendientes, Asistidos, Cancelados
        protected void btnEstado_Click(object sender, EventArgs e)
        {
            // Implementación pendiente: Como Listar() no filtra, solo recarga la grilla completa.
            CargarGrilla();
        }

        // --- MANEJADORES DE BÚSQUEDA DINÁMICA ---

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lógica pendiente: Ajustar el modo del TextBox de filtro.
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Implementación pendiente: Aplicar el filtro dinámico.
            CargarGrilla();
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            // Lógica pendiente: Limpiar controles.
            CargarGrilla();
        }
    }
}