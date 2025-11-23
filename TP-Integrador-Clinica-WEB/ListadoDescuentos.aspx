<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoDescuentos.aspx.cs" 
    Inherits="TP_Integrador_Clinica_WEB.ListadoDescuentos" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row mb-3">
        <div class="col-md-12">
            <h2>Descuentos por Obra Social: <asp:Label ID="lblNombreObraSocial" runat="server" Text="" /></h2>
            <hr />
        </div>
    </div>
    
    <div class="row mb-3">
        <div class="col-md-12">

            <%-- Alta de descuento (se completa en Page_Load) --%>
            <asp:HyperLink ID="btnNuevoDescuento" runat="server" CssClass="btn btn-primary">
                <i class="fa fa-plus"></i> Nuevo Descuento
            </asp:HyperLink>

            <a href="ListadoObrasSociales.aspx" class="btn btn-secondary">Volver al Listado de Obras Sociales</a>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">

            <asp:GridView ID="dgvDescuentos" runat="server"
                CssClass="table table-striped table-hover"
                AutoGenerateColumns="false"
                DataKeyNames="Id"
                OnRowCommand="dgvDescuentos_RowCommand">

                <Columns>

                    <%-- ID --%>
                    <asp:BoundField HeaderText="ID" DataField="Id" />

                    <%-- Descripción --%>
                    <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />

                    <%-- % Descuento --%>
                    <asp:BoundField HeaderText="% Descuento" DataField="PorcentajeDescuento" />

                    <%-- Edad mínima/máxima --%>
                    <asp:BoundField HeaderText="Edad Mínima" DataField="EdadMin" />
                    <asp:BoundField HeaderText="Edad Máxima" DataField="EdadMax" />

                    <%-- Obra Social (NO SE PUEDE USAR BoundField) --%>
                    <asp:TemplateField HeaderText="Obra Social">
                        <ItemTemplate>
                            <%# Eval("ObraSocial.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Acciones --%>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>

                            <asp:LinkButton ID="btnEditar" runat="server"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-sm btn-info me-2">
                                Editar
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnEliminar" runat="server"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-sm btn-danger"
                                OnClientClick="return confirm('¿Está seguro de eliminar este Descuento?');">
                                Eliminar
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
    </div>

</asp:Content>
