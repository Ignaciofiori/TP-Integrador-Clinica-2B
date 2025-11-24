<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoEspecialidades.aspx.cs" 
    Inherits="TP_Integrador_Clinica_WEB.ListadoEspecialidades" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Módulo: Especialidades</h2>
    <hr />

    <asp:Button ID="btnNuevaEspecialidad" runat="server" Text="➕ Nueva Especialidad"
        OnClick="btnNuevaEspecialidad_Click" CssClass="btn btn-primary mb-3" />
    
    <asp:GridView ID="gvEspecialidades" runat="server"
        CssClass="table table-striped table-hover"
        AutoGenerateColumns="false"
        DataKeyNames="IdEspecialidad"
        OnRowEditing="gvEspecialidades_RowEditing"
        OnRowDeleting="gvEspecialidades_RowDeleting">

        <Columns>

            <asp:BoundField DataField="Nombre" HeaderText="Especialidad" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />

                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditar" runat="server"
                            CommandName="Edit"
                            CommandArgument='<%# Eval("IdEspecialidad") %>'
                            CssClass="btn btn-sm btn-info me-2">
                            ✏️ Editar
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEliminar" runat="server"
                            CommandName="Delete"
                            CommandArgument='<%# Eval("IdEspecialidad") %>'
                            CssClass="btn btn-sm btn-danger"
                            OnClientClick="return confirm('¿Seguro que querés eliminar esta especialidad?');">
                            🗑️ Eliminar
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


        </Columns>

    </asp:GridView>

</asp:Content>
