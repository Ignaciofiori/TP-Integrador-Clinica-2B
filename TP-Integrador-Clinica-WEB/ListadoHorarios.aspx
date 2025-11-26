<%@ Page Title="Listado Horarios" Language="C#" 
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true" 
    CodeBehind="ListadoHorarios.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.ListadoHorarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mt-4 mb-4">Listado de Horarios</h2>

  
    <asp:Button 
        ID="btnNuevo"
        runat="server"
        Text="+ Nuevo Horario"
        CssClass="btn btn-success mb-3"
        OnClick="btnNuevo_Click" />
    

    <asp:GridView 
        ID="gvHorarios"
        runat="server"
        DataKeyNames="IdHorario"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="False"
        OnRowCommand="gvHorarios_RowCommand">

        <Columns>

            <asp:BoundField DataField="IdHorario" HeaderText="ID" />

            <asp:BoundField DataField="NombreCompletoProfesional" HeaderText="Profesional" />

            <asp:BoundField DataField="DiaSemana" HeaderText="Día" />
            <asp:BoundField DataField="NombreConsultorio" HeaderText="Consultorio" />


         <asp:BoundField DataField="HoraInicio" HeaderText="Inicio" />
        <asp:BoundField DataField="HoraFin" HeaderText="Fin" />

           <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:LinkButton 
                    ID="btnEliminar"
                    runat="server"
                    Text="Eliminar"
                    CssClass="btn btn-danger btn-sm"
                    CommandName="Eliminar"
                    CommandArgument='<%# Eval("IdHorario") %>' />
            </ItemTemplate>
        </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
