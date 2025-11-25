<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormularioObraSocial.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.FormularioObraSocial" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="lblTitulo" runat="server" Text="Alta de Obra Social" CssClass="h2" />
    <hr />

    <div class="row">
        <div class="col-md-6">
            
            <asp:HiddenField runat="server" ID="hfIdObraSocial" />

            <%--nombre--%>
            <div class="mb-3">
                <label for="<%= txtNombre.ClientID %>" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
                    CssClass="text-danger" ErrorMessage="El Nombre es obligatorio." />
            </div>

            <%--porcentaje--%>
            <div class="mb-3">
                <label for="<%= txtPorcentaje.ClientID %>" class="form-label">Porcentaje Cobertura:</label>
                <asp:TextBox runat="server" ID="txtPorcentaje" TextMode="Number" CssClass="form-control" />

                <!-- Required -->
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPorcentaje"
                    ErrorMessage="El porcentaje es obligatorio." CssClass="text-danger" Display="Dynamic" />

                <!-- Rango 0–100 -->
                <asp:RangeValidator runat="server" ControlToValidate="txtPorcentaje"
                    MinimumValue="0" MaximumValue="100" Type="Integer"
                    ErrorMessage="Debe ingresar un porcentaje entre 0 y 100."
                    CssClass="text-danger" Display="Dynamic" />
            </div>
            
            <%--telefono--%>
            <div class="mb-3">
                <label for="<%= txtTelefono.ClientID %>" class="form-label">Teléfono:</label>
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" />
                <asp:RequiredFieldValidator 
                    runat="server" 
                    ControlToValidate="txtTelefono"
                    ErrorMessage="El telefono es obligatorio." 
                    CssClass="text-danger"
                    Display="Dynamic" />
                 <!-- Solo numeros y 11 digitos -->
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTelefono"
                    ValidationExpression="^\d{11}$"
                    CssClass="text-danger"
                    ErrorMessage="Ingrese un teléfono válido (11 dígitos)."
                    Display="Dynamic" />
            </div>
            
            <%--direccion--%>
            <div class="mb-3">
                <label for="<%= txtDireccion.ClientID %>" class="form-label">Dirección:</label>
                <asp:TextBox runat="server" ID="txtDireccion" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                 <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDireccion"
                    CssClass="text-danger" ErrorMessage="La Direccion es obligatoria." Display="Dynamic" />
            </div>

            <%--boton--%>
            <asp:Button runat="server" ID="btnAceptar" Text="Guardar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
            <a href="ListadoObrasSociales.aspx" class="btn btn-secondary">Cancelar</a>
            
        </div>
    </div>
</asp:Content>