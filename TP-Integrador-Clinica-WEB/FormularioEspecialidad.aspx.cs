using System;
using System.Web.UI;
using negocio;
using modelo;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class FormularioEspecialidad : Page
    {
        // Declaramos los controles manualmente (solución al error de clase parcial)
        protected TextBox txtNombre;
        protected TextBox txtDescripcion;
        protected HiddenField hfIdEspecialidad;
        protected Label lblTitulo;
        protected Button btnAceptar;

        protected void Page_Load(object sender, EventArgs e)
        {
            // La lógica de carga de datos para edición se ejecuta solo la primera vez que se carga la página.
            if (!IsPostBack)
            {
                // 1. Verificar si viene un ID por QueryString (Modificación)
                if (Request.QueryString["id"] != null)
                {
                    // Modo Edición
                    EspecialidadNegocio negocio = new EspecialidadNegocio();
                    try
                    {
                        int id = int.Parse(Request.QueryString["id"]);

                        // Lógica de carga de datos: Buscar por ID
                        Especialidad especialidad = negocio.BuscarPorId(id);

                        if (especialidad != null)
                        {
                            // 2. Rellenar los campos y cambiar el título
                            lblTitulo.Text = "Modificar Especialidad";
                            txtNombre.Text = especialidad.Nombre;
                            txtDescripcion.Text = especialidad.Descripcion;
                            btnAceptar.Text = "Modificar";

                            // 3. Guardar el ID en el campo oculto
                            hfIdEspecialidad.Value = id.ToString();
                        }
                    }
                    catch (Exception)
                    {
                        Response.Redirect("Error.aspx");
                    }
                }
                // Si no tiene ID y no es PostBack, la página se queda en modo Alta por defecto.
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            // Este método maneja tanto Alta como Modificación

            EspecialidadNegocio negocio = new EspecialidadNegocio();
            Especialidad nuevaEspecialidad = new Especialidad();

            try
            {
                // 1. Cargar los datos del formulario al objeto modelo
                nuevaEspecialidad.Nombre = txtNombre.Text;
                nuevaEspecialidad.Descripcion = txtDescripcion.Text;

                // 2. Verificar si es Alta o Modificación
                if (hfIdEspecialidad.Value == "")
                {
                    // ALTA (Patrón ABM: Agregar)
                    negocio.Agregar(nuevaEspecialidad);
                }
                else
                {
                    // MODIFICACIÓN (Patrón ABM: Modificar)
                    nuevaEspecialidad.IdEspecialidad = int.Parse(hfIdEspecialidad.Value);
                    negocio.Modificar(nuevaEspecialidad);
                }

                // 3. Redirigir al listado después de guardar
                Response.Redirect("ListadoEspecialidades.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }
    }
}