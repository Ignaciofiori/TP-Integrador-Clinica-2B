using negocio;
using System;
using System.Data;

namespace TP_Integrador_Clinica_WEB
{
    public partial class Recaudaciones : System.Web.UI.Page
    {
        private FacturaNegocio facNeg = new FacturaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Facturacion.aspx");
        }

        protected void btnPorObra_Click(object sender, EventArgs e)
        {
            gvRecaudacion.DataSource = facNeg.RecaudacionPorObraSocial();
            gvRecaudacion.DataBind();
        }

        protected void btnPorProfesional_Click(object sender, EventArgs e)
        {
            gvRecaudacion.DataSource = facNeg.RecaudacionPorProfesional();
            gvRecaudacion.DataBind();
        }

        protected void btnPorEspecialidad_Click(object sender, EventArgs e)
        {
            gvRecaudacion.DataSource = facNeg.RecaudacionPorEspecialidad();
            gvRecaudacion.DataBind();
        }

        protected void btnPorMes_Click(object sender, EventArgs e)
        {
            gvRecaudacion.DataSource = facNeg.RecaudacionMensual();
            gvRecaudacion.DataBind();
        }
    }
}
