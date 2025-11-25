using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using modelo;

namespace TP_Integrador_Clinica_WEB
{
    public partial class RegistrarTurno : System.Web.UI.Page
    {
        // Declaración de controles para vinculación manual
        protected HiddenField hfIdTurno, hfPrecioBase, hfIdObraSocial;
        protected Label lblTitulo, lblCosto;
        protected DropDownList ddlPaciente, ddlEspecialidad, ddlProfesional, ddlHorario;
        protected TextBox txtObraSocialNombre, txtFechaTurno;
        protected Button btnAceptar;
        protected UpdatePanel upMain; // Necesario para el PostBack parcial

        // Negocios necesarios
        private readonly PacienteNegocio pacienteNegocio = new PacienteNegocio();
        private readonly EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
        private readonly HorarioAtencionNegocio horarioNegocio = new HorarioAtencionNegocio();
        private readonly TurnoNegocio turnoNegocio = new TurnoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropdownsIniciales();

                // Modo Edición
                if (Request.QueryString["id"] != null)
                {
                    CargarTurnoParaEdicion(int.Parse(Request.QueryString["id"]));
                }
            }
        }

        private void CargarDropdownsIniciales()
        {
            // Cargar Pacientes
            ddlPaciente.DataSource = pacienteNegocio.Listar();
            ddlPaciente.DataBind();
            ddlPaciente.Items.Insert(0, new ListItem("-- Seleccione Paciente --", "0"));

            // Cargar Especialidades
            ddlEspecialidad.DataSource = especialidadNegocio.Listar();
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccione Especialidad --", "0"));

            // Inicializar profesionales y horarios vacíos
            ddlProfesional.Items.Insert(0, new ListItem("-- Seleccione Profesional --", "0"));
            ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Horario --", "0"));

            // Deshabilitar controles que dependen de la selección previa
            ddlProfesional.Enabled = false;
            txtFechaTurno.Enabled = false;
            ddlHorario.Enabled = false;
        }

        private void CargarTurnoParaEdicion(int idTurno)
        {
            // 🛑 NOTA: Debes implementar BuscarPorId(idTurno) en TurnoNegocio o aquí.
            Turno turno = turnoNegocio.BuscarPorId(idTurno);

            if (turno != null)
            {
                lblTitulo.Text = "Modificar Turno N°" + idTurno;
                hfIdTurno.Value = idTurno.ToString();

                // 1. Cargar Paciente
                ddlPaciente.SelectedValue = turno.Paciente.IdPaciente.ToString();
                ddlPaciente.Enabled = false; // No se cambia el paciente en Edición

                // 2. Cargar Especialidad (asumiendo que Horario tiene Especialidad)
                if (turno.Horario != null && turno.Horario.Especialidad != null)
                {
                    ddlEspecialidad.SelectedValue = turno.Horario.Especialidad.IdEspecialidad.ToString();
                    CargarProfesionales(turno.Horario.Especialidad.IdEspecialidad);
                }

                // 3. Cargar Profesional
                if (turno.Horario != null && turno.Horario.Profesional != null)
                {
                    ddlProfesional.SelectedValue = turno.Horario.Profesional.IdProfesional.ToString();
                    ddlProfesional.Enabled = true;
                }

                // 4. Cargar Fecha y Horario
                txtFechaTurno.Text = turno.FechaTurno.ToString("yyyy-MM-dd");
                txtFechaTurno.Enabled = true;
                CargarHorarios(turno.Horario.Profesional.IdProfesional, turno.FechaTurno);

                if (turno.Horario != null)
                {
                    // El HorarioDisplay debe coincidir con el formato de ddlHorario para seleccionar
                    string valorHora = $"{turno.Horario.IdHorario}";
                    ddlHorario.SelectedValue = valorHora;
                    ddlHorario.Enabled = true;
                }

                // Recalcular y mostrar costo final
                ActualizarResumenCosto(turno.Paciente);
            }
        }

        protected void ddlPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaciente.SelectedValue != "0")
            {
                // Muestra la Obra Social del paciente seleccionado
                int idPaciente = int.Parse(ddlPaciente.SelectedValue);
                Paciente paciente = pacienteNegocio.BuscarPorId(idPaciente);

                if (paciente != null && paciente.ObraSocial != null)
                {
                    txtObraSocialNombre.Text = paciente.ObraSocial.Nombre;
                    hfIdObraSocial.Value = paciente.ObraSocial.IdObraSocial.ToString();
                }
                else
                {
                    txtObraSocialNombre.Text = "Particular (Sin Obra Social)";
                    hfIdObraSocial.Value = "0";
                }
            }
            else
            {
                txtObraSocialNombre.Text = "";
                hfIdObraSocial.Value = "0";
                ddlProfesional.SelectedIndex = 0;
                ddlProfesional.Enabled = false;
                ddlHorario.SelectedIndex = 0;
                ddlHorario.Enabled = false;
                lblCosto.Text = "Seleccione un paciente, especialidad, profesional y horario.";
            }

            // Reiniciar pasos dependientes
            CargarProfesionales(int.Parse(ddlEspecialidad.SelectedValue));
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEsp = int.Parse(ddlEspecialidad.SelectedValue);
            CargarProfesionales(idEsp);
        }

        private void CargarProfesionales(int idEsp)
        {
            ddlProfesional.Items.Clear();
            ddlHorario.Items.Clear();
            ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Horario --", "0"));
            txtFechaTurno.Text = "";
            txtFechaTurno.Enabled = false;

            if (idEsp > 0)
            {
                // 🛑 NOTA: Asumo que tienes ListarPorEspecialidad en ProfesionalNegocio
                ddlProfesional.DataSource = new ProfesionalNegocio().ListarPorEspecialidad(idEsp);
                ddlProfesional.DataBind();
                ddlProfesional.Items.Insert(0, new ListItem("-- Seleccione Profesional --", "0"));
                ddlProfesional.Enabled = true;
            }
            else
            {
                ddlProfesional.Items.Insert(0, new ListItem("-- Seleccione Profesional --", "0"));
                ddlProfesional.Enabled = false;
            }
        }

        protected void ddlProfesional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProfesional.SelectedValue != "0")
            {
                txtFechaTurno.Enabled = true;
                ddlHorario.Enabled = false;
                ddlHorario.Items.Clear();
                ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Horario --", "0"));
                lblCosto.Text = "Seleccione una fecha para ver horarios disponibles.";

                // Reiniciar fecha y forzar selección
                txtFechaTurno.Text = "";
            }
            else
            {
                txtFechaTurno.Enabled = false;
                ddlHorario.Enabled = false;
            }
        }

        protected void txtFechaTurno_TextChanged(object sender, EventArgs e)
        {
            DateTime fecha;
            if (DateTime.TryParse(txtFechaTurno.Text, out fecha) && ddlProfesional.SelectedValue != "0")
            {
                int idProf = int.Parse(ddlProfesional.SelectedValue);
                CargarHorarios(idProf, fecha);
            }
            else
            {
                ddlHorario.Items.Clear();
                ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Horario --", "0"));
                ddlHorario.Enabled = false;
                if (!string.IsNullOrEmpty(txtFechaTurno.Text))
                {
                    lblCosto.Text = "Seleccione una fecha válida (Año > 1900)";
                }
                else
                {
                    lblCosto.Text = "Seleccione una fecha.";
                }
            }
        }

        private void CargarHorarios(int idProf, DateTime fecha)
        {
            ddlHorario.Items.Clear();
            ddlHorario.Enabled = false;
            lblCosto.Text = "Calculando disponibilidad...";

            // 🛑 NOTA: Asumo que tienes ListarDisponibles en HorarioAtencionNegocio
            var horariosDisponibles = horarioNegocio.ListarDisponibles(idProf, fecha);

            if (horariosDisponibles.Any())
            {
                ddlHorario.DataSource = horariosDisponibles;
                ddlHorario.DataBind();
                ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Horario --", "0"));
                ddlHorario.Enabled = true;
                lblCosto.Text = "Horarios cargados. Seleccione uno para ver el costo.";
            }
            else
            {
                ddlHorario.Items.Insert(0, new ListItem("No hay horarios disponibles.", "0"));
                lblCosto.Text = "No hay horarios disponibles para la fecha seleccionada.";
            }
        }

        protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHorario.SelectedValue != "0")
            {
                int idPaciente = int.Parse(ddlPaciente.SelectedValue);
                Paciente paciente = pacienteNegocio.BuscarPorId(idPaciente);

                if (paciente != null)
                {
                    // Recalcular el costo para mostrarlo al usuario
                    ActualizarResumenCosto(paciente);
                }
            }
            else
            {
                lblCosto.Text = "Seleccione un horario para ver el costo.";
            }
        }

        private void ActualizarResumenCosto(Paciente paciente)
        {
            Turno turnoTemp = CrearTurnoTemporal(paciente);
            decimal montoFinal = turnoNegocio.DeterminarMonto(turnoTemp);

            // 🛑 NOTA: Necesitas el precio base de la especialidad para mostrar el descuento.
            // Asumo que el objeto Horario.Especialidad.PrecioBase está disponible o lo calculas.

            // Por simplicidad, solo mostramos el monto final calculado por DeterminarMonto
            lblCosto.Text = $"Monto Final a pagar por el paciente: **{montoFinal:C}**.";
        }

        private Turno CrearTurnoTemporal(Paciente paciente)
        {
            // Crea un objeto Turno solo para calcular el monto (DeterminarMonto lo necesita)
            Turno t = new Turno
            {
                Paciente = paciente,
                Horario = horarioNegocio.BuscarPorId(int.Parse(ddlHorario.SelectedValue)),
                // La Obra Social se puede tomar del paciente o rellenar si es editable
                ObraSocial = paciente.ObraSocial,
                FechaTurno = DateTime.Parse(txtFechaTurno.Text)
            };
            return t;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            try
            {
                Paciente paciente = pacienteNegocio.BuscarPorId(int.Parse(ddlPaciente.SelectedValue));

                Turno t = CrearTurnoTemporal(paciente);

                t.Estado = "pendiente"; // Estado inicial

                // Calcular el monto total y guardarlo
                t.MontoTotal = turnoNegocio.DeterminarMonto(t);

                if (!string.IsNullOrEmpty(hfIdTurno.Value))
                {
                    // Lógica de Modificación (no implementada completamente aquí)
                    t.IdTurno = int.Parse(hfIdTurno.Value);
                    // 🛑 NOTA: Necesitas un método ModificarTurno en TurnoNegocio
                    // turnoNegocio.ModificarTurno(t); 
                }
                else
                {
                    // Lógica de Registro
                    turnoNegocio.RegistrarTurno(t);
                }

                Response.Redirect("ListadoTurnos.aspx", false);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw ex;
            }
        }
    }
}