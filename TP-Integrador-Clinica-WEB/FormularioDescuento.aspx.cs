using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using modelo;
using negocio;

namespace TP_Integrador_Clinica_WEB
{
    public partial class FormularioDescuento : Page
    {
        // 🛑 Declaración de controles para vinculación manual
        protected HiddenField hfIdDescuento;
        protected HiddenField hfIdObraSocial;
        protected Label lblTitulo;
        protected TextBox txtDescripcion;
        protected TextBox txtPorcentaje;
        protected TextBox txtEdadMin;
        protected TextBox txtEdadMax;
        protected Button btnAceptar;
        protected HyperLink btnCancelar;

        // Necesario para la función de BuscarPorId en edición
        public Descuento BuscarPorId(int id)
        {
            ObraSocialNegocio obraNegocio = new ObraSocialNegocio();
            AccesoDatos datos = new AccesoDatos();
            Descuento d = null;
            try
            {
                // Solo necesitamos los datos del descuento, no toda la Obra Social
                datos.setearConsulta(@"SELECT id_descuento, id_obra_social, edad_min, edad_max, 
                                            porcentaje_descuento, descripcion 
                                     FROM Descuento WHERE id_descuento = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    d = new Descuento();
                    d.Id = (int)datos.Lector["id_descuento"];

                    
                    d.ObraSocial = obraNegocio.BuscarPorId((int)datos.Lector["id_obra_social"] );

                    if (!(datos.Lector["edad_min"] is DBNull)) d.EdadMin = (int)datos.Lector["edad_min"];
                    if (!(datos.Lector["edad_max"] is DBNull)) d.EdadMax = (int)datos.Lector["edad_max"];
                    if (!(datos.Lector["porcentaje_descuento"] is DBNull)) d.PorcentajeDescuento = (decimal)datos.Lector["porcentaje_descuento"];
                    if (!(datos.Lector["descripcion"] is DBNull)) d.Descripcion = (string)datos.Lector["descripcion"];
                }
                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            // Es crucial que en alta recibamos el idObraSocial
            if (Request.QueryString["idObraSocial"] == null && hfIdObraSocial.Value == "")
            {
                Response.Redirect("ListadoObrasSociales.aspx", false); // Manejo de error
            }

            if (!IsPostBack)
            {
                // 1. Manejo del ID de la Obra Social para Alta
                if (Request.QueryString["idObraSocial"] != null)
                {
                    hfIdObraSocial.Value = Request.QueryString["idObraSocial"];
                }

                // 2. Configuración para Edición
                if (Request.QueryString["id"] != null)
                {
                    int idDescuento = Convert.ToInt32(Request.QueryString["id"]);
                    // Nota: Aquí se usa una función de búsqueda directa para evitar circularidad en DescuentoNegocio
                    Descuento descuento = BuscarPorId(idDescuento);

                    if (descuento != null)
                    {
                        lblTitulo.Text = "Modificar Descuento";
                        hfIdDescuento.Value = idDescuento.ToString();
                        hfIdObraSocial.Value = descuento.ObraSocial.IdObraSocial.ToString(); // Sobreescribimos con el ID real

                        txtDescripcion.Text = descuento.Descripcion;
                        txtPorcentaje.Text = descuento.PorcentajeDescuento.ToString();
                        txtEdadMin.Text = descuento.EdadMin.ToString();
                        txtEdadMax.Text = descuento.EdadMax.ToString();
                    }
                }
            }

            // 3. Configurar URL de Cancelar (siempre debe volver al listado de descuentos de esa OS)
            string idOS = string.IsNullOrEmpty(hfIdObraSocial.Value) ? Request.QueryString["idObraSocial"] : hfIdObraSocial.Value;
            if (!string.IsNullOrEmpty(idOS))
            {
                btnCancelar.NavigateUrl = $"ListadoDescuentos.aspx?idObraSocial={idOS}";
            }
            else
            {
                btnCancelar.NavigateUrl = "ListadoObrasSociales.aspx";
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            DescuentoNegocio negocio = new DescuentoNegocio();
            Descuento d = new Descuento();

            try
            {
                // 1. Cargar datos del formulario al objeto
                d.Descripcion = txtDescripcion.Text;
                d.PorcentajeDescuento = decimal.Parse(txtPorcentaje.Text);
                d.EdadMin = int.Parse(txtEdadMin.Text);
                d.EdadMax = int.Parse(txtEdadMax.Text);

                // 2. Cargar el objeto ObraSocial solo con el ID (necesario para el método Agregar/Modificar)
                d.ObraSocial = new ObraSocial
                {
                    IdObraSocial = int.Parse(hfIdObraSocial.Value)
                };


                // 3. Determinar si es Alta o Modificación
                if (!string.IsNullOrEmpty(hfIdDescuento.Value))
                {
                    // Modificación
                    d.Id = int.Parse(hfIdDescuento.Value);
                    negocio.Modificar(d);
                }
                else
                {
                    // Alta
                    negocio.Agregar(d);
                }

                // 4. Redirigir al listado de descuentos de la Obra Social
                Response.Redirect($"ListadoDescuentos.aspx?idObraSocial={hfIdObraSocial.Value}", false);

            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw ex;
            }
        }
    }
}