<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormularioObraSocial.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.FormularioObraSocial" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="lblTitulo" runat="server" Text="Alta de Obra Social" CssClass="h2" />
    <hr />

    <div class="row">
        <div class="col-md-6">
            
            <asp:HiddenField runat="server" ID="hfIdObraSocial" />

            <div class="mb-3">
                <label for="<%= txtNombre.ClientID %>" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
                    CssClass="text-danger" ErrorMessage="El Nombre es obligatorio." />
            </div>

            <div class="mb-3">
                <label for="<%= txtPorcentaje.ClientID %>" class="form-label">Porcentaje Cobertura:</label>
                <asp:TextBox runat="server" ID="txtPorcentaje" TextMode="Number" CssClass="form-control" />
            </div>
            
            <div class="mb-3">
                <label for="<%= txtTelefono.ClientID %>" class="form-label">Teléfono:</label>
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" />
            </div>
            
            <div class="mb-3">
                <label for="<%= txtDireccion.ClientID %>" class="form-label">Dirección:</label>
                <asp:TextBox runat="server" ID="txtDireccion" TextMode="MultiLine" Rows="2" CssClass="form-control" />
            </div>

            <asp:Button runat="server" ID="btnAceptar" Text="Guardar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
            <a href="ListadoObrasSociales.aspx" class="btn btn-secondary">Cancelar</a>
            
        </div>
    </div>
</asp:Content>