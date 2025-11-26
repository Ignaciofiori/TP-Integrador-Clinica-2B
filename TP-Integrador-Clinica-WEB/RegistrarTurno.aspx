<%@ Page Title="Turno" Language="C#" AutoEventWireup="true" CodeBehind="RegistrarTurno.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.RegistrarTurno" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><asp:Label ID="lblTitulo" runat="server" Text="Registrar Nuevo Turno" /></h2>
    <hr />

    <div class="row">
        <div class="col-md-8">

            <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <%-- *** SECCIÓN 1: SELECCIÓN DE PACIENTE Y ESPECIALIDAD *** --%>
                    <fieldset class="mb-4 p-3 border rounded">
                        <legend class="float-none w-auto px-1 h5">1. Datos Iniciales</legend>
                        
                        <asp:HiddenField ID="hfIdTurno" runat="server" />
                        <asp:HiddenField ID="hfPrecioBase" runat="server" />
                        
                        <div class="mb-3">
                            <label for="<%= ddlPaciente.ClientID %>" class="form-label">Seleccionar Paciente:</label>
                            <asp:DropDownList ID="ddlPaciente" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged" DataValueField="IdPaciente" DataTextField="NombreCompleto" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPaciente" InitialValue="0" CssClass="text-danger" ErrorMessage="Debe seleccionar un paciente." />
                        </div>
                        
                        <div class="mb-3">
                            <label for="<%= ddlEspecialidad.ClientID %>" class="form-label">Seleccionar Especialidad:</label>
                            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged" DataValueField="IdEspecialidad" DataTextField="Nombre" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEspecialidad" InitialValue="0" CssClass="text-danger" ErrorMessage="Debe seleccionar una especialidad." />
                        </div>

                        <%-- Muestra la Obra Social del paciente seleccionado --%>
                        <div class="mb-3">
                            <label class="form-label">Obra Social del Paciente (Automática):</label>
                            <asp:TextBox ID="txtObraSocialNombre" runat="server" CssClass="form-control" ReadOnly="true" />
                            <asp:HiddenField ID="hfIdObraSocial" runat="server" />
                        </div>

                    </fieldset>

                    <%-- *** SECCIÓN 2: SELECCIÓN DE PROFESIONAL Y FECHA *** --%>
                    <fieldset class="mb-4 p-3 border rounded">
                        <legend class="float-none w-auto px-1 h5">2. Profesional y Fecha</legend>

                        <div class="mb-3">
                            <label for="<%= ddlProfesional.ClientID %>" class="form-label">Profesional disponible:</label>
                            <asp:DropDownList ID="ddlProfesional" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProfesional_SelectedIndexChanged" DataValueField="IdProfesional" DataTextField="NombreCompleto" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProfesional" InitialValue="0" CssClass="text-danger" ErrorMessage="Debe seleccionar un profesional." />
                        </div>

                        <!-- selecciona dia de semana -->
                        <div class="mb-3">
                            <label class="form-label">Día disponible:</label>
                            <asp:DropDownList 
                                ID="ddlDiaSemana" 
                                runat="server" 
                                CssClass="form-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlDiaSemana_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <!--selecciona fecha disponible, configurado hasta 4 semanas -->
                        <div class="mb-3">
                            <label class="form-label">Fecha disponible:</label>
                            <asp:DropDownList 
                                ID="ddlFechaDisponible" 
                                runat="server" 
                                CssClass="form-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFechaDisponible_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>



                    </fieldset>

                    <%-- *** SECCIÓN 3: SELECCIÓN DE HORA Y RESUMEN *** --%>
                    <fieldset class="mb-4 p-3 border rounded">
                        <legend class="float-none w-auto px-1 h5">3. Horario y Confirmación</legend>
                        
                        <div class="mb-3">
                            <label for="<%= ddlHorario.ClientID %>" class="form-label">Horarios Disponibles:</label>
                            <%-- ddlHorario se llenará y es crucial que dispare el cálculo del precio --%>
                            <asp:DropDownList ID="ddlHorario" runat="server" CssClass="form-control" DataValueField="IdHorario" DataTextField="HorarioDisplay" AutoPostBack="true" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlHorario" InitialValue="0" CssClass="text-danger" ErrorMessage="Debe seleccionar un horario." />
                        </div>
                        <div class="alert alert-secondary mt-3">
                        <h5  class="alert-heading">Consultorio</h5>
                        <asp:Label ID="lblConsultorio" runat="server" Text="Seleccione un horario." />
                            </div>

                        <%-- Resumen de Monto --%>
                        <div class="alert alert-info mt-4">
                            <h5 class="alert-heading">Resumen de Costo</h5>
                            <asp:Label ID="lblCosto" runat="server" Text="Seleccione un horario para calcular el costo." />
                        </div>
                        
                        <div class="mt-4">
                            <asp:Button ID="btnAceptar" runat="server" Text="Confirmar Turno" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
                            <asp:HyperLink ID="btnCancelar" runat="server" Text="Cancelar" NavigateUrl="~/ListadoTurnos.aspx" CssClass="btn btn-secondary" />
                        </div>

                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>