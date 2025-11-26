using negocio;
using System;
using System.Collections.Generic;

namespace TP_Integrador_Clinica_WEB
{
    public partial class Facturacion : System.Web.UI.Page
    {
        private readonly FacturaNegocio facturaNegocio = new FacturaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarFacturas();
        }

        private void CargarFacturas()
        {
            gvFacturas.DataSource = facturaNegocio.Listar();
            gvFacturas.DataBind();
        }
        protected void btnRecaudaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recaudaciones.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = ddlCampo.SelectedValue;
            string valor = txtFiltro.Text.Trim();

            // Si no elige campo → no hacemos nada
            if (criterio == "0" || string.IsNullOrWhiteSpace(valor))
            {
                CargarFacturas();
                return;
            }

            // Llamamos a la búsqueda del negocio
            var lista = facturaNegocio.Buscar(criterio, valor);

            gvFacturas.DataSource = lista;
            gvFacturas.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCampo.SelectedIndex = 0;
            txtFiltro.Text = string.Empty;
            CargarFacturas();
        }
    }
}
