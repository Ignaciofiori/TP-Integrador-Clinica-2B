using System;
using System.Web.UI;
using negocio;
using modelo;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class FormularioEspecialidad : Page
    {
        protected TextBox txtNombre;
        protected TextBox txtDescripcion;
        protected HiddenField hfIdEspecialidad;
        protected Label lblTitulo;
        protected Button btnAceptar;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    EspecialidadNegocio negocio = new EspecialidadNegocio();
                    try
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        Especialidad especialidad = negocio.BuscarPorId(id);

                        if (especialidad != null)
                        {
                            lblTitulo.Text = "Modificar Especialidad";
                            txtNombre.Text = especialidad.Nombre;
                            txtDescripcion.Text = especialidad.Descripcion;
                            btnAceptar.Text = "Modificar";
                            hfIdEspecialidad.Value = id.ToString();
                        }
                    }
                    catch (Exception)
                    {
                        Response.Redirect("Error.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            Especialidad nuevaEspecialidad = new Especialidad();

            try
            {
                nuevaEspecialidad.Nombre = txtNombre.Text;
                nuevaEspecialidad.Descripcion = txtDescripcion.Text;

                if (hfIdEspecialidad.Value == "")
                {
                    negocio.Agregar(nuevaEspecialidad);
                }
                else
                {
                    nuevaEspecialidad.IdEspecialidad = int.Parse(hfIdEspecialidad.Value);
                    negocio.Modificar(nuevaEspecialidad);
                }

                // Redirect SIN ThreadAbort
                Response.Redirect("ListadoEspecialidades.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}
