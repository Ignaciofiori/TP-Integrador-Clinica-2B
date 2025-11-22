<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoObrasSociales.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.ListadoObrasSociales" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row mb-3">
        <div class="col-md-12">
            <h2>Listado de Obras Sociales</h2>
        </div>
    </div>
    
    <div class="row mb-3">
        <div class="col-md-12">
            <a href="FormularioObraSocial.aspx" class="btn btn-primary">
                <i class="fa fa-plus"></i> Nueva Obra Social
            </a>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="dgvObrasSociales" runat="server" 
                CssClass="table table-striped table-hover"
                AutoGenerateColumns="false"
                DataKeyNames="Id"
                OnRowCommand="dgvObrasSociales_RowCommand">

                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="Id" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="% Cobertura" DataField="PorcentajeCobertura" />
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                    <asp:TemplateField HeaderText="Descuentos">
    <ItemTemplate>
        <asp:LinkButton ID="btnDescuentos" runat="server" CommandName="Descuentos" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-warning">
            Ver Descuentos
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

                    <%-- Columna de Acciones --%>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-info me-2">
                                <i class="fa fa-pencil-alt"></i> Editar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Está seguro de eliminar esta Obra Social?');">
                                <i class="fa fa-trash-alt"></i> Eliminar
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>