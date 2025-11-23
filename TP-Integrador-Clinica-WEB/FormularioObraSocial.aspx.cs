using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using modelo;
using negocio;

namespace TP_Integrador_Clinica_WEB
{
    public partial class FormularioObraSocial : Page
    {
        // 🛑 Declaración de controles para vinculación manual
        protected HiddenField hfIdObraSocial;
        protected Label lblTitulo;
        protected TextBox txtNombre;
        protected TextBox txtPorcentaje;
        protected TextBox txtTelefono;
        protected TextBox txtDireccion;
        protected Button btnAceptar;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Configuración para Edición
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    ObraSocialNegocio negocio = new ObraSocialNegocio();
                    ObraSocial obra = negocio.BuscarPorId(id);

                    // Cargar datos
                    if (obra != null)
                    {
                        lblTitulo.Text = "Modificar Obra Social";
                        hfIdObraSocial.Value = id.ToString();
                        txtNombre.Text = obra.Nombre;
                        txtPorcentaje.Text = obra.PorcentajeCobertura.ToString();
                        txtTelefono.Text = obra.Telefono;
                        txtDireccion.Text = obra.Direccion;
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ObraSocialNegocio negocio = new ObraSocialNegocio();
            ObraSocial nuevaObra = new ObraSocial();

            try
            {
                // 1. Cargar datos del formulario al objeto
                nuevaObra.Nombre = txtNombre.Text;

                // Conversiones seguras
                if (!string.IsNullOrEmpty(txtPorcentaje.Text))
                    nuevaObra.PorcentajeCobertura = decimal.Parse(txtPorcentaje.Text);
                else
                    nuevaObra.PorcentajeCobertura = 0;

                nuevaObra.Telefono = txtTelefono.Text;
                nuevaObra.Direccion = txtDireccion.Text;

                // 2. Determinar si es Alta o Modificación
                if (!string.IsNullOrEmpty(hfIdObraSocial.Value))
                {
                    // Modificación
                    nuevaObra.IdObraSocial = int.Parse(hfIdObraSocial.Value);
                    negocio.Modificar(nuevaObra);
                }
                else
                {
                    // Alta
                    negocio.Agregar(nuevaObra);
                }

                // 3. Redirigir al listado
                Response.Redirect("ListadoObrasSociales.aspx", false);

            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw ex;
            }
        }
    }
}