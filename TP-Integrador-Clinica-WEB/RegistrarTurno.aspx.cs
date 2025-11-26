using modelo;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_Integrador_Clinica_WEB
{
    public partial class RegistrarTurno : System.Web.UI.Page
    {
        // Declaración de controles para vinculación manual
        protected HiddenField hfIdTurno, hfPrecioBase, hfIdObraSocial;
        protected Label lblTitulo, lblCosto;
        protected DropDownList ddlPaciente, ddlEspecialidad, ddlProfesional, ddlHorario;
        protected TextBox txtObraSocialNombre;

        protected Button btnAceptar;
        protected UpdatePanel upMain; // Necesario para el PostBack parcial

        //Para turnos
        protected DropDownList ddlDiaSemana;
        protected DropDownList ddlFechaDisponible;
        protected Label lblConsultorio;


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
            ddlHorario.Enabled = false;
        }

        private void CargarTurnoParaEdicion(int idTurno)
        {
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
                ddlFechaDisponible.Items.Clear();
                ddlFechaDisponible.Items.Add(new ListItem(turno.FechaTurno.ToString("yyyy-MM-dd"), turno.FechaTurno.ToString("yyyy-MM-dd")));
                ddlFechaDisponible.SelectedIndex = 0;
                ddlFechaDisponible.Enabled = true;

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
            }

            //  Si NO hay especialidad seleccionada todavía, solo limpio y me voy
            if (ddlEspecialidad.SelectedValue == "0")
            {
                ddlProfesional.Items.Clear();
                ddlProfesional.Items.Insert(0, new ListItem("-- Seleccione Profesional --", "0"));
                ddlProfesional.Enabled = false;

                ddlDiaSemana.Items.Clear();
                ddlDiaSemana.Items.Insert(0, new ListItem("-- Seleccione Día --", "0"));

                ddlFechaDisponible.Items.Clear();
                ddlFechaDisponible.Items.Insert(0, new ListItem("-- Seleccione Fecha --", "0"));

                ddlHorario.Items.Clear();
                ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Horario --", "0"));
                ddlHorario.Enabled = false;
                lblCosto.Text = "Seleccione un paciente, especialidad, profesional y horario.";
                return;
            }

            // Si hay especialidad elegida, cargo profesionales
            CargarProfesionales(int.Parse(ddlEspecialidad.SelectedValue));

            // Y limpio día/fecha/horarios
            ddlDiaSemana.Items.Clear();
            ddlDiaSemana.Items.Insert(0, new ListItem("-- Seleccione Día --", "0"));

            ddlFechaDisponible.Items.Clear();
            ddlFechaDisponible.Items.Insert(0, new ListItem("-- Seleccione Fecha --", "0"));

            ddlHorario.Items.Clear();
            ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Horario --", "0"));
            ddlHorario.Enabled = false;
            lblCosto.Text = "Seleccione día y horario para calcular costo.";
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
           

            if (idEsp > 0)
            {
                
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


        /// para turnos

        private List<string> GenerarProximasFechas(string diaSemana)
        {
            var mapa = new Dictionary<string, DayOfWeek>()
    {
        {"Lunes", DayOfWeek.Monday},
        {"Martes", DayOfWeek.Tuesday},
        {"Miércoles", DayOfWeek.Wednesday},
        {"Jueves", DayOfWeek.Thursday},
        {"Viernes", DayOfWeek.Friday},
        {"Sábado", DayOfWeek.Saturday},
        {"Domingo", DayOfWeek.Sunday}
    };

            var fechas = new List<string>();
            DateTime hoy = DateTime.Today;

            if (!mapa.ContainsKey(diaSemana))
                return fechas;

            DayOfWeek target = mapa[diaSemana];

            // Primer día válido
            int offset = ((int)target - (int)hoy.DayOfWeek + 7) % 7;
            DateTime primerDia = hoy.AddDays(offset);

            //  SIEMPRE 7 fechas
            for (int i = 0; i < 7; i++)
                fechas.Add(primerDia.AddDays(7 * i).ToString("yyyy-MM-dd"));

            return fechas;
        }
        protected void ddlDiaSemana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDiaSemana.SelectedIndex <= 0 || ddlProfesional.SelectedValue == "0")
                return;

            string dia = ddlDiaSemana.SelectedValue;
            var fechas = GenerarProximasFechas(dia);

            ddlFechaDisponible.Items.Clear();
            ddlFechaDisponible.DataSource = fechas;
            ddlFechaDisponible.DataBind();
            ddlFechaDisponible.Items.Insert(0, new ListItem("-- Seleccione Fecha --", "0"));
        }

        protected void ddlFechaDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFechaDisponible.SelectedIndex <= 0 || ddlProfesional.SelectedValue == "0")
                return;

            int idProfesional = int.Parse(ddlProfesional.SelectedValue);
            DateTime fecha = DateTime.Parse(ddlFechaDisponible.SelectedValue);

            // Usamos tu método ya existente para listar disponibles
            HorarioAtencionNegocio horarioNeg = new HorarioAtencionNegocio();
            var horarios = horarioNeg.ListarDisponibles(idProfesional, fecha);

            ddlHorario.Items.Clear();
            if (horarios.Any())
            {
                ddlHorario.DataSource = horarios;
                ddlHorario.DataTextField = "HorarioDisplay";
                ddlHorario.DataValueField = "IdHorario";
                ddlHorario.DataBind();
                ddlHorario.Items.Insert(0, new ListItem("-- Seleccione horario --", "0"));
                ddlHorario.Enabled = true;
                lblCosto.Text = "Horarios cargados. Seleccione uno para ver el costo.";
            }
            else
            {
                ddlHorario.Items.Insert(0, new ListItem("No hay horarios disponibles.", "0"));
                ddlHorario.Enabled = false;
                lblCosto.Text = "No hay horarios disponibles para la fecha seleccionada.";
            }
        }


        protected void ddlProfesional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProfesional.SelectedValue != "0")
            {
                int idProfesional = int.Parse(ddlProfesional.SelectedValue);

                ddlHorario.Enabled = false;
                ddlHorario.Items.Clear();
                ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Horario --", "0"));
                lblCosto.Text = "Seleccione un día para ver fechas disponibles.";

                // esto limpia date dropdowns
                ddlDiaSemana.Items.Clear();
                ddlDiaSemana.Items.Insert(0, new ListItem("-- Seleccione Día --", "0"));

                ddlFechaDisponible.Items.Clear();
                ddlFechaDisponible.Items.Insert(0, new ListItem("-- Seleccione Fecha --", "0"));


                //
                // carga dias del profesional
                // 
                HorarioAtencionNegocio horarioNeg = new HorarioAtencionNegocio();
                var dias = horarioNeg.ListarDiasQueAtiende(idProfesional);

                ddlDiaSemana.Items.Clear();
                ddlDiaSemana.DataSource = dias;
                ddlDiaSemana.DataBind();
                ddlDiaSemana.Items.Insert(0, new ListItem("-- Seleccione Día --", "0"));
            }
            else
            {
               
                ddlHorario.Enabled = false;

                // limpia los nuevos dropdowns
                ddlDiaSemana.Items.Clear();
                ddlDiaSemana.Items.Insert(0, new ListItem("-- Seleccione Día --", "0"));

                ddlFechaDisponible.Items.Clear();
                ddlFechaDisponible.Items.Insert(0, new ListItem("-- Seleccione Fecha --", "0"));
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
                var horario = horarioNegocio.BuscarPorId(int.Parse(ddlHorario.SelectedValue));
                lblConsultorio.Text = $"Consultorio: {horario.Consultorio.NombreCompleto}";

                if (paciente != null)
                {
                    ActualizarResumenCosto(paciente);
                }
            }
            else
            {
                lblCosto.Text = "Seleccione un horario para ver el costo.";
                lblConsultorio.Text = "Seleccione un horario.";
            }
        }

        private void ActualizarResumenCosto(Paciente paciente)
        {
            Turno turnoTemp = CrearTurnoTemporal(paciente);
            decimal montoFinal = turnoNegocio.DeterminarMonto(turnoTemp);

         
            lblCosto.Text = $"Monto Final a pagar por el paciente: **{montoFinal:C}**.";
        }

        private Turno CrearTurnoTemporal(Paciente paciente)
        {

            // aca busca el horario elegido
            var horario = horarioNegocio.BuscarPorId(int.Parse(ddlHorario.SelectedValue));

            // Crea un objeto Turno solo para calcular el monto (DeterminarMonto lo necesita)
            Turno t = new Turno
            {
                Paciente = paciente,
                Horario = horario,
                ObraSocial = paciente.ObraSocial,
                FechaTurno = DateTime.Parse(ddlFechaDisponible.SelectedValue),
                HoraTurno = horario.HoraInicio

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
                    t.IdTurno = int.Parse(hfIdTurno.Value);
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