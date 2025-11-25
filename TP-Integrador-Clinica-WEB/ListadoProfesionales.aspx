<%@ Page Title="Listado de Profesionales" Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="ListadoProfesionales.aspx.cs" 
    Inherits="TP_Integrador_Clinica_WEB.ListadoProfesionales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mt-4 mb-3">Listado de Profesionales</h2>

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
                <asp:ListItem Value="Telefono" Text="Teléfono" />
                <asp:ListItem Value="Email" Text="Email" />
                <asp:ListItem Value="Especialidad" Text="Especialidad" />
                <asp:ListItem Value="ObraSocial" Text="Obra Social" />
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

    <%-- BOTÓN AGREGAR PROFESIONAL --%>
    <asp:Button
        ID="btnAgregar"
        runat="server"
        Text="Agregar Profesional"
        CssClass="btn btn-success mb-3"
        OnClick="btnAgregar_Click" />

    <%-- LISTADO --%>
    <asp:GridView 
        ID="gvProfesionales" 
        runat="server" 
        AutoGenerateColumns="False"
        CssClass="table table-striped table-hover"
        OnRowCommand="gvProfesionales_RowCommand">

        <Columns>

            <asp:BoundField DataField="IdProfesional" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="Dni" HeaderText="DNI" />
            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
 
            <%-- EDITAR --%>
            <asp:HyperLinkField
                Text="Editar"
                ControlStyle-CssClass="btn btn-info btn-sm"
                DataNavigateUrlFields="IdProfesional"
                DataNavigateUrlFormatString="FormularioProfesional.aspx?id={0}" />

            <%-- VER CONVENIOS (OBRAS SOCIALES) --%>
          <asp:TemplateField HeaderText="Convenios">
            <ItemTemplate>
                <asp:LinkButton
                    ID="btnConvenios"
                    runat="server"
                    Text="Ver Convenios"
                    CssClass="btn btn-warning btn-sm"
                    CommandName="Convenios"
                    CommandArgument='<%# Eval("IdProfesional") %>' />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Especialidades">
            <ItemTemplate>
                <asp:LinkButton
                    ID="btnEspecialidades"
                    runat="server"
                    Text="Ver Especialidades"
                    CssClass="btn btn-secondary btn-sm"
                    CommandName="Especialidades"
                    CommandArgument='<%# Eval("IdProfesional") %>' />
            </ItemTemplate>
</asp:TemplateField>

   <asp:TemplateField HeaderText="Eliminar">
    <ItemTemplate>
        <asp:LinkButton
            ID="btnEliminar"
            runat="server"
            Text="Eliminar"
            CssClass="btn btn-danger btn-sm"
            CommandName="Eliminar"
            CommandArgument='<%# Eval("IdProfesional") %>'
            OnClientClick="return confirm('¿Seguro que desea eliminar este profesional?');" />
    </ItemTemplate>
</asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
