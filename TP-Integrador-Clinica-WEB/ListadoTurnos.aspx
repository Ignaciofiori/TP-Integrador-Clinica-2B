<%@ Page Title="Turnos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoTurnos.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.ListadoTurnos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mb-4">📋 Listado de Turnos</h2>

        <div class="mb-4 d-flex justify-content-start align-items-center flex-wrap">
            
            <%-- Botones de ACCIÓN PRINCIPALES --%>
            <asp:Button ID="btnNuevoTurno" runat="server" Text="Agregar Turno" 
                CssClass="btn btn-primary me-3 mb-2" OnClick="btnNuevoTurno_Click" />

            <asp:Button ID="btnNuevoHorario" runat="server" Text="Agregar Horario de Atención" 
                CssClass="btn btn-success me-4 mb-2" OnClick="btnNuevoHorario_Click" />
                
            <%-- Botones de NAVEGACIÓN POR ESTADO --%>
            <asp:Button ID="btnVerPendientes" runat="server" Text="Ver Turnos Pendientes" 
                CssClass="btn btn-info me-2 mb-2" OnClick="btnEstado_Click" CommandArgument="Pendiente" />
            
            <asp:Button ID="btnVerAsistidos" runat="server" Text="Ver Turnos Asistidos" 
                CssClass="btn btn-warning me-2 mb-2" OnClick="btnEstado_Click" CommandArgument="Asistido" />
            
            <asp:Button ID="btnVerCancelados" runat="server" Text="Ver Turnos Cancelados" 
                CssClass="btn btn-danger mb-2" OnClick="btnEstado_Click" CommandArgument="Cancelado" />
        </div>
        
        <hr />

        <div class="row mb-4 p-3 border rounded shadow-sm bg-light">
            <div class="col-md-3">
                <label for="<%= ddlCampo.ClientID %>" class="form-label">Filtrar por:</label>
                <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                    <asp:ListItem Text="Seleccione Campo" Value="0" />
                    <asp:ListItem Text="Estado" Value="ESTADO" />
                    <asp:ListItem Text="Fecha" Value="FECHA" />
                    <asp:ListItem Text="Paciente" Value="PACIENTE" />
                    <asp:ListItem Text="Profesional" Value="PROFESIONAL" />
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
                <asp:BoundField DataField="FechaTurno" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="HoraTurno" HeaderText="Hora" DataFormatString="{0:hh\:mm}" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />

                <%-- Columna de Acciones (Solo para Pendientes, gestionada por RowDataBound) --%>
                <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <%-- Botón para Marcar como Asistido (Requiere MarcarAsistido en el negocio) --%>
                        <asp:LinkButton ID="btnAsistido" runat="server" 
                            CommandName="MarcarAsistido" 
                            CommandArgument='<%# Eval("IdTurno") %>'
                            CssClass="btn btn-sm btn-success me-2"
                            Text="Asistido" />
                        
                        <%-- Botón para Cancelar (Requiere CancelarTurno en el negocio) --%>
                        <asp:LinkButton ID="btnCancelar" runat="server" 
                            CommandName="CancelarTurno" 
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