using System;
using System.Web.UI;
using negocio;
using modelo;

namespace TP_Integrador_Clinica_WEB
{
    public partial class FormularioPaciente : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarObrasSociales();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarPaciente(id);
                }
            }
        }

        private void CargarObrasSociales()
        {
            ObraSocialNegocio negocio = new ObraSocialNegocio();
            ddlObraSocial.DataSource = negocio.Listar();
            ddlObraSocial.DataTextField = "Nombre";
            ddlObraSocial.DataValueField = "IdObraSocial";
            ddlObraSocial.DataBind();

            // opción sin obra social
            ddlObraSocial.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Sin obra social", ""));
        }

        private void CargarPaciente(int id)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente p = negocio.BuscarPorId(id);

            if (p == null)
                return;

            txtNombre.Text = p.Nombre;
            txtApellido.Text = p.Apellido;
            txtDni.Text = p.Dni;
            txtTelefono.Text = p.Telefono;
            txtEmail.Text = p.Email;
            txtDireccion.Text = p.Direccion;
           

            if (p.FechaNacimiento.HasValue)
                txtFechaNacimiento.Text = p.FechaNacimiento.Value.ToString("yyyy-MM-dd");

            if (p.ObraSocial != null)
                ddlObraSocial.SelectedValue = p.ObraSocial.IdObraSocial.ToString();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente nuevo = new Paciente();

            // si viene por edición
            if (Request.QueryString["id"] != null)
                nuevo.IdPaciente = int.Parse(Request.QueryString["id"]);

            nuevo.Nombre = txtNombre.Text;
            nuevo.Apellido = txtApellido.Text;
            nuevo.Dni = txtDni.Text;
            nuevo.Telefono = txtTelefono.Text;
            nuevo.Email = txtEmail.Text;
            nuevo.Direccion = txtDireccion.Text;
            

            if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))
                nuevo.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

            if (!string.IsNullOrEmpty(ddlObraSocial.SelectedValue))
            {
                nuevo.ObraSocial = new ObraSocial();
                nuevo.ObraSocial.IdObraSocial = int.Parse(ddlObraSocial.SelectedValue);
            }

            if (nuevo.IdPaciente > 0)
                negocio.Modificar(nuevo);
            else
                negocio.Agregar(nuevo);

            Response.Redirect("ListadoPacientes.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoPacientes.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
