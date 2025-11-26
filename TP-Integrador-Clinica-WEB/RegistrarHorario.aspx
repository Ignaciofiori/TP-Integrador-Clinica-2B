<%@ Page Title="Horario de Atención" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarHorario.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.RegistrarHorario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <asp:Label ID="tituloFormulario" runat="server" 
                   CssClass="h4 mb-4 d-block text-primary">
        </asp:Label>

        <div class="card shadow-sm p-4">

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Profesional</label>
                    <asp:DropDownList ID="ddlProfesional" runat="server"
                        CssClass="form-select"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProfesional_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Especialidad</label>
                    <asp:DropDownList ID="ddlEspecialidad" runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Consultorio</label>
                    <asp:DropDownList ID="ddlConsultorio" runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Día de la Semana</label>
                    <asp:DropDownList ID="ddlDia" runat="server"
                        CssClass="form-select">
                        <asp:ListItem Text="Lunes" />
                        <asp:ListItem Text="Martes" />
                        <asp:ListItem Text="Miércoles" />
                        <asp:ListItem Text="Jueves" />
                        <asp:ListItem Text="Viernes" />
                        <asp:ListItem Text="Sábado" />
                        <asp:ListItem Text="Domingo" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Hora Inicio</label>
                    <asp:TextBox ID="txtInicio" runat="server"
                        CssClass="form-control" 
                        TextMode="Time">
                    </asp:TextBox>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Hora Fin</label>
                    <asp:TextBox ID="txtFin" runat="server"
                        CssClass="form-control"
                        TextMode="Time">
                    </asp:TextBox>
                </div>
            </div>

            <asp:Label ID="lblError" runat="server"
                       CssClass="text-danger fw-bold d-block mt-2 mb-3">
            </asp:Label>

            <div class="d-flex justify-content-end mt-3">
                <asp:Button ID="btnGuardar" runat="server" 
                    Text="Guardar"
                    CssClass="btn btn-primary me-3"
                    OnClick="btnGuardar_Click" />

                <asp:Button ID="btnCancelar" runat="server" 
                    Text="Cancelar"
                    CssClass="btn btn-secondary"
                    OnClick="btnCancelar_Click" />
            </div>

        </div>
    </div>

</asp:Content>
