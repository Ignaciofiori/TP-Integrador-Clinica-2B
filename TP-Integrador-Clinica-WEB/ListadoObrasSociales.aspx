<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoObrasSociales.aspx.cs" 
    Inherits="TP_Integrador_Clinica_WEB.ListadoObrasSociales" MasterPageFile="~/Site.Master" %>

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
                DataKeyNames="IdObraSocial"
                OnRowCommand="dgvObrasSociales_RowCommand">

                <Columns>

                    <%-- ID --%>
                    <asp:BoundField HeaderText="ID" DataField="IdObraSocial" />

                    <%-- Nombre --%>
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />

                    <%-- Porcentaje --%>
                    <asp:BoundField HeaderText="% Cobertura" DataField="PorcentajeCobertura" />

                    <%-- Teléfono --%>
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />

                    <%-- Botón de descuentos --%>
                    <asp:TemplateField HeaderText="Descuentos">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDescuentos" runat="server" 
                                CommandName="Descuentos"
                                CommandArgument='<%# Eval("IdObraSocial") %>'
                                CssClass="btn btn-sm btn-warning">
                                Ver Descuentos
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Acciones --%>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>

                            <asp:LinkButton ID="btnEditar" runat="server" 
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdObraSocial") %>'
                                CssClass="btn btn-sm btn-info me-2">
                                Editar
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnEliminar" runat="server"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("IdObraSocial") %>'
                                CssClass="btn btn-sm btn-danger"
                                OnClientClick="return confirm('¿Está seguro de eliminar esta Obra Social?');">
                                Eliminar
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
    </div>

</asp:Content>
