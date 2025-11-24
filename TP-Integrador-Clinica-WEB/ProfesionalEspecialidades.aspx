<%@ Page Title="Especialidades del Profesional" Language="C#" 
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ProfesionalEspecialidades.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.ProfesionalEspecialidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <h2 class="mb-4">Especialidades del Profesional</h2>

        <asp:Button 
            ID="btnAgregar"
            runat="server"
            Text="Agregar Especialidad"
            CssClass="btn btn-success mb-3"
            OnClick="btnAgregar_Click" />

      <asp:GridView 
    ID="gvEspecialidades" 
    runat="server" 
    AutoGenerateColumns="False"
    CssClass="table table-striped table-hover"
    OnRowCommand="gvEspecialidades_RowCommand">

    <Columns>

        <asp:BoundField DataField="IdEspecialidad" HeaderText="ID" />
        <asp:BoundField DataField="Nombre" HeaderText="Especialidad" />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
        <asp:BoundField DataField="ValorConsulta" HeaderText="Valor Consulta" DataFormatString="{0:C}" />

        <asp:TemplateField HeaderText="Editar Valor">
            <ItemTemplate>
                <asp:LinkButton 
                    ID="btnEditarValor"
                    runat="server"
                    Text="Editar"
                    CssClass="btn btn-primary btn-sm"
                    CommandName="EditarValor"
                    CommandArgument='<%# Eval("IdEspecialidad") %>' />
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
                    CommandArgument='<%# Eval("IdEspecialidad") %>'
                    OnClientClick="return confirm('¿Seguro que deseas eliminar esta especialidad?');" />
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
