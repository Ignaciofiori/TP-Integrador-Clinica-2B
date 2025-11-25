<%@ Page Title="Turnos por Estado" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="TurnosEstado.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.TurnosEstado" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <h2 class="mb-4">
            <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
        </h2>

        <asp:Button 
    ID="btnVolver" 
    runat="server" 
    Text="← Volver a Turnos Pendientes"
    CssClass="btn btn-secondary mb-3"
    OnClick="btnVolver_Click" />

        <div class="row mb-4 p-3 border rounded shadow-sm bg-light">

            <div class="col-md-3">
                <label class="form-label">Filtrar por:</label>
                <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione Campo" Value="0" />
                    <asp:ListItem Text="Paciente" Value="Paciente" />
                    <asp:ListItem Text="Profesional" Value="Profesional" />
                    <asp:ListItem Text="Especialidad" Value="Especialidad" />
                    <asp:ListItem Text="Obra Social" Value="ObraSocial" />
                    <asp:ListItem Text="Monto mayor a..." Value="MontoMayor" />
                    <asp:ListItem Text="Monto menor a..." Value="MontoMenor" />
                </asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label class="form-label">Valor:</label>
                <div class="input-group">
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"
                        placeholder="Ingrese valor..." />

                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                        CssClass="btn btn-primary"
                        OnClick="btnBuscar_Click" />
                </div>
            </div>

            <div class="col-md-3 d-flex align-items-end">
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Filtro"
                    CssClass="btn btn-outline-secondary w-100"
                    OnClick="btnLimpiar_Click" />
            </div>

        </div>

        <hr />

        <asp:GridView ID="gvTurnosEstado" runat="server"
            AutoGenerateColumns="false"
            CssClass="table table-striped table-hover table-bordered">

            <Columns>
                <asp:BoundField DataField="IdTurno" HeaderText="ID" />
                <asp:BoundField DataField="Paciente.NombreCompleto" HeaderText="Paciente" />
                <asp:BoundField DataField="Horario.Profesional.NombreCompleto" HeaderText="Profesional" />
                <asp:BoundField DataField="Horario.Especialidad.Nombre" HeaderText="Especialidad" />
                <asp:BoundField DataField="NombreObraSocialTurno" HeaderText="Obra Social" />
                <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total" DataFormatString="{0:C}" HtmlEncode="false" />
                <asp:BoundField DataField="FechaTurno" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                
                <asp:TemplateField HeaderText="Hora">
                    <ItemTemplate>
                        <%# ((TimeSpan)Eval("HoraTurno")).ToString(@"hh\:mm") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
