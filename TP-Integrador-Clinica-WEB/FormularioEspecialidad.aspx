<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormularioEspecialidad.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.FormularioEspecialidad" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="lblTitulo" runat="server" Text="Nueva Especialidad" CssClass="h2" />
    <hr />

    <div class="row">
        <div class="col-md-6">
            <div class="mb-3">
                <label for="<%= txtNombre.ClientID %>" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
                    CssClass="text-danger" ErrorMessage="El Nombre es obligatorio." />
            </div>

            <div class="mb-3">
                <label for="<%= txtDescripcion.ClientID %>" class="form-label">Descripción:</label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>

            <asp:Button runat="server" ID="btnAceptar" Text="Guardar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
            <a href="ListadoEspecialidades.aspx" class="btn btn-secondary">Cancelar</a>
            
            <asp:HiddenField runat="server" ID="hfIdEspecialidad" />
        </div>
    </div>

</asp:Content>