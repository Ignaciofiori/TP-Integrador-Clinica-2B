<%@ Page Title="Turnos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoTurnos.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.ListadoTurnos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mb-4">📋 Listado de Turnos</h2>

        <div class="mb-4 d-flex justify-content-start align-items-center flex-wrap">
            
            <%-- Botones de ACCIÓN PRINCIPALES --%>
            <asp:Button ID="btnNuevoTurno" runat="server" Text="Agregar Turno" 
                CssClass="btn btn-primary me-3 mb-2" OnClick="btnNuevoTurno_Click" />

            <asp:Button ID="btnVerHorarios" runat="server" Text="Ver Horarios de Atención"
                     CssClass="btn btn-info me-3 mb-2" OnClick="btnVerHorarios_Click" />
                
            <%-- Botones de NAVEGACIÓN POR ESTADO --%>
        
            <asp:Button ID="btnVerAsistidos" runat="server" Text="Ver Turnos Asistidos" 
                CssClass="btn btn-warning me-2 mb-2" OnClick="btnEstado_Click" CommandArgument="Asistido" />
            
            <asp:Button ID="btnVerCancelados" runat="server" Text="Ver Turnos Cancelados" 
                CssClass="btn btn-danger mb-2" OnClick="btnEstado_Click" CommandArgument="Cancelado" />
        </div>
        
        <hr />

        <div class="row mb-4 p-3 border rounded shadow-sm bg-light">
            <div class="col-md-3">
                <label for="<%= ddlCampo.ClientID %>" class="form-label">Filtrar por:</label>
<asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-select">
    <asp:ListItem Text="Seleccione Campo" Value="0" />
    <asp:ListItem Text="Paciente" Value="Paciente" />
    <asp:ListItem Text="Profesional" Value="Profesional" />
    <asp:ListItem Text="Especialidad" Value="Especialidad" />
    <asp:ListItem Text="Obra Social" Value="ObraSocial" />
    <asp:ListItem Text="Monto mayor a..." Value="MontoMayor" />
    <asp:ListItem Text="Monto menor a..." Value="MontoMenor" />
</asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label for="<%= txtFiltro.ClientID %>" class="form-label">Valor del Filtro:</label>
                <div class="input-group">
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" Visible="true" placeholder="Ingrese el valor a buscar" />
                    
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary" />
                </div>
            </div>

            <div class="col-md-3 d-flex align-items-end">
                <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar Filtro" OnClick="btnLimpiarFiltro_Click" CssClass="btn btn-outline-secondary w-100" />
            </div>
        </div>
        
        <hr />

        <asp:GridView ID="gvTurnos" runat="server" 
            AutoGenerateColumns="false" 
            CssClass="table table-striped table-hover table-bordered" 
            DataKeyNames="IdTurno"
            OnRowCommand="gvTurnos_RowCommand" 
            OnRowDataBound="gvTurnos_RowDataBound"> 
            
            <Columns>
                <%-- Columnas de Datos (Ajusta los DataField a tu modelo) --%>
                <asp:BoundField DataField="IdTurno" HeaderText="ID" SortExpression="IdTurno" ItemStyle-Width="50px" />
                <asp:BoundField DataField="Paciente.NombreCompleto" HeaderText="Paciente" SortExpression="Paciente.Apellido" />
                <asp:BoundField DataField="Horario.Profesional.NombreCompleto" HeaderText="Profesional" SortExpression="Profesional.Apellido" />
                <asp:BoundField DataField="Horario.Especialidad.Nombre" HeaderText="Especialidad" SortExpression="Especialidad.Nombre" />
               <asp:BoundField DataField="NombreObraSocialTurno" HeaderText="Obra Social" ItemStyle-Width="150px" />
                <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total" DataFormatString="{0:C}" HtmlEncode="false" ItemStyle-Width="120px" />
                <asp:BoundField DataField="FechaTurno" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="HoraTurno" HeaderText="Hora" DataFormatString="{0:hh\:mm}" />
                <asp:BoundField DataField="ConsultorioNombre" HeaderText="Consultorio" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />

               <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="200px">
                    <ItemTemplate>

                        <!-- Botón de Asistido -->
                        <asp:LinkButton ID="btnAsistido" runat="server"
                            CommandName="Asistir"
                            CommandArgument='<%# Eval("IdTurno") %>'
                            CssClass="btn btn-sm btn-success me-2"
                            Text="Asistido"
                            OnClientClick="return confirm('¿Confirmar asistencia del turno?');" />

                        <!-- Botón de Cancelar -->
                        <asp:LinkButton ID="btnCancelar" runat="server"
                            CommandName="Cancelar"
                            CommandArgument='<%# Eval("IdTurno") %>'
                            CssClass="btn btn-sm btn-danger"
                            Text="Cancelar"
                            OnClientClick="return confirm('¿Está seguro de que desea cancelar este turno?');" />

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>