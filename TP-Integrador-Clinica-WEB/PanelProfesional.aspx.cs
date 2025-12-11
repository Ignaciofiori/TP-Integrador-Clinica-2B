using modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class PanelProfesional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var navbar = Master.FindControl("navBarPrincipal");
            if (navbar != null)
                navbar.Visible = false;

            var usuario = (Usuario)Session["usuario"];

            if (usuario == null || usuario.IdRol != 2)
            {
                Response.Redirect("Login.aspx");
            }

            lblBienvenida.Text = "Bienvenido Dr. " + usuario.Nombre + " " + usuario.Apellido;
        }
    }
}