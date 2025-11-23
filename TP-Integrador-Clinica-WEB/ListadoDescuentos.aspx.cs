using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TP_Integrador_Clinica_WEB
{
    public partial class ListadoDescuentos : System.Web.UI.Page
    {
        //  Declaraciones para vinculación manual
        protected Label lblNombreObraSocial;
        protected HyperLink btnNuevoDescuento;
        protected GridView dgvDescuentos;

        private int idObraSocial;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idObraSocial"] == null)
            {
                // Si no hay ID, redirigimos al listado principal (manejo de error básico)
                Response.Redirect("ListadoObrasSociales.aspx", false);
                return;
            }

            // Almacenamos el ID de la Obra Social
            idObraSocial = Convert.ToInt32(Request.QueryString["idObraSocial"]);

            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            ObraSocialNegocio osNegocio = new ObraSocialNegocio();
            DescuentoNegocio dNegocio = new DescuentoNegocio();

            // 1. Mostrar el nombre de la OS
            ObraSocial os = osNegocio.BuscarPorId(idObraSocial);
            lblNombreObraSocial.Text = os != null ? os.Nombre : "No Encontrada";

            // 2. Cargar la grilla de descuentos
            // NOTA: Debes implementar ListarPorObraSocial en DescuentoNegocio
            dgvDescuentos.DataSource = dNegocio.ListarPorObraSocial(idObraSocial);
            dgvDescuentos.DataBind();

            // 3. Configurar el enlace de "Nuevo Descuento" para incluir el ID de la OS
            btnNuevoDescuento.NavigateUrl = $"FormularioDescuento.aspx?idObraSocial={idObraSocial}";
        }

        protected void dgvDescuentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idDescuento = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Redirige al formulario de edición, manteniendo el ID de la OS
                Response.Redirect($"FormularioDescuento.aspx?id={idDescuento}&idObraSocial={idObraSocial}", false);
            }
            else if (e.CommandName == "Eliminar")
            {
                DescuentoNegocio negocio = new DescuentoNegocio();
                try
                {
                    negocio.Eliminar(idDescuento);
                    // Opcional: Mostrar mensaje de éxito
                    CargarDatos(); // Recarga la grilla
                }
                catch (Exception ex)
                {
                    // Manejo de error
                    throw ex;
                }
            }
        }
    }
}