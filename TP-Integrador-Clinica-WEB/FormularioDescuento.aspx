<%@ Page Title="Descuento" Language="C#" AutoEventWireup="true" CodeBehind="FormularioDescuento.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.FormularioDescuento" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="lblTitulo" runat="server" Text="Alta de Nuevo Descuento" CssClass="h2" />
    <hr />

    <div class="row">
        <div class="col-md-6">
            
            <%-- Campos ocultos para ID de Descuento y ID de Obra Social --%>
            <asp:HiddenField runat="server" ID="hfIdDescuento" />
            <asp:HiddenField runat="server" ID="hfIdObraSocial" />
            
            <%-- Campo para la descripción --%>
            <div class="mb-3">
                <label for="<%= txtDescripcion.ClientID %>" class="form-label">Descripción del Descuento:</label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescripcion"
                    CssClass="text-danger" ErrorMessage="La Descripción es obligatoria." />
            </div>

            <%-- Campo para el porcentaje --%>
            <div class="mb-3">
                <label for="<%= txtPorcentaje.ClientID %>" class="form-label">Porcentaje de Descuento:</label>
                <asp:TextBox runat="server" ID="txtPorcentaje" TextMode="Number" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPorcentaje"
                    CssClass="text-danger" ErrorMessage="El Porcentaje es obligatorio." />
                <asp:RangeValidator runat="server" ControlToValidate="txtPorcentaje" Type="Double" MinimumValue="0" MaximumValue="100" 
                    CssClass="text-danger" ErrorMessage="Debe ser entre 0 y 100." />
            </div>
            
            <%-- Campo para edad mínima --%>
            <div class="mb-3">
                <label for="<%= txtEdadMin.ClientID %>" class="form-label">Edad Mínima (0 si no aplica):</label>
                <asp:TextBox runat="server" ID="txtEdadMin" TextMode="Number" CssClass="form-control" Text="0" />
            </div>
            
            <%-- Campo para edad máxima --%>
            <div class="mb-3">
                <label for="<%= txtEdadMax.ClientID %>" class="form-label">Edad Máxima (99 si no aplica):</label>
                <asp:TextBox runat="server" ID="txtEdadMax" TextMode="Number" CssClass="form-control" Text="99" />
            </div>

            <asp:Button runat="server" ID="btnAceptar" Text="Guardar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
            
            <%-- El botón Cancelar es dinámico, usaremos un HyperLink en el Code-Behind --%>
            <asp:HyperLink ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" NavigateUrl="#" />
            
        </div>
    </div>
</asp:Content>