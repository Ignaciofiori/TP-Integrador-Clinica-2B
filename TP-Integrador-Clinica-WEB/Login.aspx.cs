using modelo;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si ya hay sesión, lo manda al home
            if (Session["usuario"] != null)
                Response.Redirect("Default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario u = negocio.Autenticar(user, pass);

            if (u == null)
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
                return;
            }

            Session["usuario"] = u;

            if (u.IdRol == 1) // Admin
            {
                Response.Redirect("PanelAdmin.aspx");
            }
            else if (u.IdRol == 2) // Profesional
            {
                Response.Redirect("PanelProfesional.aspx");
            }

        }
    }
}