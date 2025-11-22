<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoDescuentos.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.ListadoDescuentos" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row mb-3">
        <div class="col-md-12">
            <h2>Descuentos por Obra Social: <asp:Label ID="lblNombreObraSocial" runat="server" Text="" /></h2>
            <hr />
        </div>
    </div>
    
    <div class="row mb-3">
        <div class="col-md-12">
            <%-- Usamos QueryString para pasar el IdObraSocial al formulario de Alta --%>
            <asp:HyperLink ID="btnNuevoDescuento" runat="server" NavigateUrl="#" CssClass="btn btn-primary">
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
                    <asp:BoundField HeaderText="ID" DataField="Id" />
                    <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                    <asp:BoundField HeaderText="% Descuento" DataField="PorcentajeDescuento" />
                    <asp:BoundField HeaderText="Edad Mínima" DataField="EdadMin" />
                    <asp:BoundField HeaderText="Edad Máxima" DataField="EdadMax" />
                    <%-- El nombre de la Obra Social ya viene cargado en el objeto Descuento --%>
                    <asp:BoundField HeaderText="Obra Social" DataField="IdObraSocial.Nombre" /> 

                    <%-- Columna de Acciones --%>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-info me-2">
                                <i class="fa fa-pencil-alt"></i> Editar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Está seguro de eliminar este Descuento?');">
                                <i class="fa fa-trash-alt"></i> Eliminar
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>