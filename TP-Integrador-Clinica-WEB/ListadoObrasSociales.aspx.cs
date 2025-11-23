using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TP_Integrador_Clinica_WEB
{
    public partial class ListadoObrasSociales : System.Web.UI.Page
    {
        protected GridView dgvObrasSociales; // DECLARACIÓN para vinculacion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            ObraSocialNegocio negocio = new ObraSocialNegocio();
            dgvObrasSociales.DataSource = negocio.Listar();
            dgvObrasSociales.DataBind();
        }

        protected void dgvObrasSociales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idObraSocial = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect("FormularioObraSocial.aspx?id=" + idObraSocial);
            }
            else if (e.CommandName == "Eliminar")
            {
                ObraSocialNegocio negocio = new ObraSocialNegocio();
                try
                {
                    negocio.Eliminar(idObraSocial);
                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    // Manejo de error - muestra un mensaje simple en la pagina o lanza la excepción
                    throw ex;
                }
            }
            else if (e.CommandName == "Descuentos")
            {
                // Redirige al nuevo listado, pasando el ID de la Obra Social
                Response.Redirect("ListadoDescuentos.aspx?idObraSocial=" + idObraSocial);
            }
        }
    }
}