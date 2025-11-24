<%@ Page Title="Profesional" Language="C#" 
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="FormularioProfesional.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.FormularioProfesional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-6">

                <div class="card shadow">
                    <div class="card-body">

                        <h2 class="text-center mb-4">Profesional</h2>

                        <asp:ValidationSummary runat="server" CssClass="text-danger text-center mb-3" />

                        <div class="mb-3">
                            <label>Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label>Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label>DNI</label>
                            <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label>Matrícula</label>
                            <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label>Teléfono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label>Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        </div>

                        <div class="text-center mt-3">
                            <asp:Button ID="btnAceptar" runat="server" Text="Guardar"
                                CssClass="btn btn-success px-4" OnClick="btnAceptar_Click" />

                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                CssClass="btn btn-secondary ms-3 px-4" OnClick="btnCancelar_Click" />
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
