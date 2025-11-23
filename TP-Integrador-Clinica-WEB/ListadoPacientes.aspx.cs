
using negocio;

using System;

namespace TP_Integrador_Clinica_WEB
{
    public partial class ListadoPacientes : System.Web.UI.Page
    {
        PacienteNegocio negocio = new PacienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarPacientes();
        }

        private void CargarPacientes()
        {
            gvPacientes.DataSource = negocio.Listar();
            gvPacientes.DataBind();
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = ddlCriterio.SelectedValue;
            string valor = txtFiltro.Text.Trim();

            // Validación para criterios de edad
            if (criterio == "Menor a" || criterio == "Mayor a")
            {
                // 1) Validar que sea número
                int edad;
                if (!int.TryParse(valor, out edad))
                {
                    txtFiltro.Text = "";
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "alert",
                        "alert('Debes ingresar un número válido para la edad.');",
                        true
                    );
                    return;
                }

                // 2) Validar que NO sea negativo
                if (edad < 0)
                {
                    txtFiltro.Text = "";
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "alert",
                        "alert('La edad no puede ser un número negativo.');",
                        true
                    );
                    return;
                }
                // Validación: la edad no puede ser mayor a 120
                if (edad > 120)
                {
                    txtFiltro.Text = "";
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "alert",
                        "alert('La edad ingresada es demasiado alta. Máximo permitido: 120.');",
                        true
                    );
                    return;
                }

            }


            // EJECUTAR BÚSQUEDA NORMAL
            gvPacientes.DataSource = negocio.Buscar(criterio, valor);
            gvPacientes.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            // limpiar el texto del input
            txtFiltro.Text = "";

            // resetear el criterio al primero (opcional)
            ddlCriterio.SelectedIndex = 0;

            // cargar el listado completo
            CargarPacientes();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            // más adelante va: Response.Redirect("PacienteFormulario.aspx");
        }

        protected void gvPacientes_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            // Lo vas a usar más adelante para manejar eliminar, si querés
        }
    }
}
