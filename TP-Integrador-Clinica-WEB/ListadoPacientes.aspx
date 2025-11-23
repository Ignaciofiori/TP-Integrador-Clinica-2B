<%@ Page Title="Listado de Pacientes" Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="ListadoPacientes.aspx.cs" 
    Inherits="TP_Integrador_Clinica_WEB.ListadoPacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mt-4 mb-3">Listado de Pacientes</h2>

    <%-- BUSCADOR --%>
    <div class="row mb-4">

        <div class="col-md-3">
            <asp:DropDownList 
                ID="ddlCriterio" 
                runat="server"
                CssClass="form-select">
                <asp:ListItem Value="Nombre" Text="Nombre" />
                <asp:ListItem Value="Apellido" Text="Apellido" />
                <asp:ListItem Value="Dni" Text="DNI" />
                <asp:ListItem Value="ObraSocial" Text="Obra Social" />
                <asp:ListItem Value="Menor a" Text="Menor a.." />
                <asp:ListItem Value="Mayor a" Text="Mayor a.." />
            </asp:DropDownList>
        </div>

        <div class="col-md-4">
            <asp:TextBox 
                ID="txtFiltro" 
                runat="server"
                CssClass="form-control"
                placeholder="Ingrese valor..." />
        </div>

        <div class="col-md-2">
            <asp:Button 
                ID="btnBuscar" 
                runat="server" 
                Text="Buscar"
                CssClass="btn btn-primary w-100"
                OnClick="btnBuscar_Click" />
        </div>

        <div class="col-md-2">
            <asp:Button 
                ID="btnLimpiar" 
                runat="server" 
                Text="Limpiar"
                CssClass="btn btn-secondary w-100"
                OnClick="btnLimpiar_Click" />
        </div>
    </div>

    <%-- BOTÓN AGREGAR PACIENTE --%>
    <asp:Button
        ID="btnAgregar"
        runat="server"
        Text="Agregar Paciente"
        CssClass="btn btn-success mb-3"
        OnClick="btnAgregar_Click" />

    <%-- LISTADO --%>
    <asp:GridView 
        ID="gvPacientes" 
        runat="server" 
        AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered"
        OnRowCommand="gvPacientes_RowCommand">

        <Columns>

            <asp:BoundField DataField="IdPaciente" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="Dni" HeaderText="DNI" />
            <asp:BoundField DataField="Edad" HeaderText="Edad" />
            <asp:BoundField DataField="ObraSocialNombre" HeaderText="Obra Social" />
            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
            <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
            <asp:BoundField DataField="Email" HeaderText="Email" />

            <%-- EDITAR --%>
            <asp:HyperLinkField
                Text="Editar"
                ControlStyle-CssClass="btn btn-info btn-sm"
                DataNavigateUrlFields="IdPaciente"
                DataNavigateUrlFormatString="PacienteFormulario.aspx?id={0}" />

            <%-- ELIMINAR --%>
            <asp:ButtonField
                Text="Eliminar"
                CommandName="Eliminar"
                ControlStyle-CssClass="btn btn-danger btn-sm" />

        </Columns>

    </asp:GridView>

</asp:Content>
