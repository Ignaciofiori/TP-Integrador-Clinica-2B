<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoEspecialidades.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.ListadoEspecialidades" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Módulo: Especialidades</h2>
    <hr />

    <asp:Button ID="btnNuevaEspecialidad" runat="server" Text="➕ Nueva Especialidad" OnClick="btnNuevaEspecialidad_Click" CssClass="btn btn-primary mb-3" />
    
    <asp:GridView ID="gvEspecialidades" runat="server" 
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        DataKeyNames="Id" 
        OnRowEditing="gvEspecialidades_RowEditing" 
        OnRowDeleting="gvEspecialidades_RowDeleting">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Especialidad" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:CommandField ShowEditButton="True" EditText="✏️ Editar" HeaderText="Acción" />
            <asp:CommandField ShowDeleteButton="True" DeleteText="🗑️ Eliminar" HeaderText="Eliminar" />
        </Columns>
    </asp:GridView>

</asp:Content>