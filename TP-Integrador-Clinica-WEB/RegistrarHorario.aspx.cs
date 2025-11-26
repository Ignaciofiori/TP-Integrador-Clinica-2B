using negocio;
using modelo;
using System;

namespace TP_Integrador_Clinica_WEB
{
    public partial class RegistrarHorario : System.Web.UI.Page
    {
        HorarioAtencionNegocio horarioNeg = new HorarioAtencionNegocio();
        ProfesionalNegocio profesionalNeg = new ProfesionalNegocio();
        EspecialidadNegocio especialidadNeg = new EspecialidadNegocio();
        ConsultorioNegocio consultorioNeg = new ConsultorioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropdowns();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarHorario(id);
                    tituloFormulario.Text = "Editar Horario de Atención";
                    btnGuardar.Text = "Actualizar";
                }
            }
        }

        private void CargarDropdowns()
        {
            ddlProfesional.DataSource = profesionalNeg.Listar();
            ddlProfesional.DataTextField = "NombreCompleto";
            ddlProfesional.DataValueField = "IdProfesional";
            ddlProfesional.DataBind();

            ddlConsultorio.DataSource = consultorioNeg.Listar();
            ddlConsultorio.DataTextField = "Nombre";
            ddlConsultorio.DataValueField = "IdConsultorio";
            ddlConsultorio.DataBind();

            ddlEspecialidad.DataSource = especialidadNeg.Listar();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();
        }

        private void CargarHorario(int id)
        {
            var h = horarioNeg.BuscarPorId(id);
            if (h == null) return;

            ddlProfesional.SelectedValue = h.Profesional.IdProfesional.ToString();
            ddlEspecialidad.SelectedValue = h.Especialidad.IdEspecialidad.ToString();
            ddlConsultorio.SelectedValue = h.Consultorio.IdConsultorio.ToString();
            ddlDia.SelectedValue = h.DiaSemana;
            txtInicio.Text = h.HoraInicio.ToString(@"hh\:mm");
            txtFin.Text = h.HoraFin.ToString(@"hh\:mm");
        }

        protected void ddlProfesional_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProf = int.Parse(ddlProfesional.SelectedValue);

            ddlEspecialidad.DataSource = especialidadNeg.ListarPorProfesional(idProf);
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                HorarioAtencion h = new HorarioAtencion();

                if (Request.QueryString["id"] != null)
                    h.IdHorario = int.Parse(Request.QueryString["id"]);

                h.Profesional = new Profesional
                {
                    IdProfesional = int.Parse(ddlProfesional.SelectedValue)
                };

                h.Especialidad = new Especialidad
                {
                    IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue)
                };

                h.Consultorio = new Consultorio
                {
                    IdConsultorio = int.Parse(ddlConsultorio.SelectedValue)
                };

                h.DiaSemana = ddlDia.SelectedValue;
                h.HoraInicio = TimeSpan.Parse(txtInicio.Text);
                h.HoraFin = TimeSpan.Parse(txtFin.Text);
                h.Activo = true;

                if (horarioNeg.ExisteConflictoHorario(h))
                {
                    lblError.Text = "Conflicto detectado: el horario se superpone con otro.";
                    return;
                }

                if (Request.QueryString["id"] != null)
                    horarioNeg.Modificar(h);
                else
                    horarioNeg.Agregar(h);

                Response.Redirect("ListadoHorarios.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoHorarios.aspx");
        }
    }
}
