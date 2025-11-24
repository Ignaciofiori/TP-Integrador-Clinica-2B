using System;
using System.Web.UI;
using negocio;
using modelo;

namespace TP_Integrador_Clinica_WEB
{
    public partial class FormularioProfesional : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarProfesional(id);
                }
            }
        }

        private void CargarProfesional(int id)
        {
            ProfesionalNegocio negocio = new ProfesionalNegocio();
            Profesional p = negocio.BuscarPorId(id);

            if (p == null)
                return;

            txtNombre.Text = p.Nombre;
            txtApellido.Text = p.Apellido;
            txtDni.Text = p.Dni;
            txtMatricula.Text = p.Matricula;
            txtTelefono.Text = p.Telefono;
            txtEmail.Text = p.Email;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ProfesionalNegocio negocio = new ProfesionalNegocio();
            Profesional nuevo = new Profesional();

            // Si es edición
            if (Request.QueryString["id"] != null)
                nuevo.IdProfesional = int.Parse(Request.QueryString["id"]);

            nuevo.Nombre = txtNombre.Text;
            nuevo.Apellido = txtApellido.Text;
            nuevo.Dni = txtDni.Text;
            nuevo.Matricula = txtMatricula.Text;
            nuevo.Telefono = txtTelefono.Text;
            nuevo.Email = txtEmail.Text;

            if (nuevo.IdProfesional > 0)
                negocio.Modificar(nuevo);
            else
                negocio.Agregar(nuevo);

            Response.Redirect("ListadoProfesionales.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoProfesionales.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
