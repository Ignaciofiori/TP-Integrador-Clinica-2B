<%@ Page Title="Obras Sociales del Profesional" Language="C#" 
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ProfesionalObrasSociales.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.ProfesionalObrasSociales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <h2 class="mb-4">Obras Sociales del Profesional</h2>

        <asp:Button 
            ID="btnAgregar"
            runat="server"
            Text="Agregar Obra Social"
            CssClass="btn btn-success mb-3"
            OnClick="btnAgregar_Click" />

        <asp:GridView 
            ID="gvObrasSociales" 
            runat="server" 
            AutoGenerateColumns="False"
            CssClass="table table-striped table-hover"
            OnRowCommand="gvObrasSociales_RowCommand">

            <Columns>

                <asp:BoundField DataField="IdObraSocial" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Obra Social" />
                <asp:BoundField DataField="PorcentajeCobertura" HeaderText="Cobertura" DataFormatString="{0}%" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />


                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton 
                            ID="btnEliminar"
                            runat="server"
                            Text="Eliminar"
                            CssClass="btn btn-danger btn-sm"
                            CommandName="Eliminar"
                            CommandArgument='<%# Eval("IdObraSocial") %>'
                            OnClientClick="return confirm('¿Seguro que deseas eliminar este convenio?');" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <asp:Button 
            ID="btnVolver"
            runat="server"
            Text="Volver"
            CssClass="btn btn-secondary mt-3"
            OnClick="btnVolver_Click" />

    </div>

</asp:Content>
