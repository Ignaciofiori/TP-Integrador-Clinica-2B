<%@ Page Title="Paciente" Language="C#" 
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="FormularioPaciente.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.FormularioPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-6">

                <div class="card shadow">
                    <div class="card-body">

                        <h2 class="text-center mb-4">Paciente</h2>

                        <%--<asp:ValidationSummary runat="server" CssClass="text-danger text-center mb-3" />--%>

                        <div class="mb-3">
                            <label>Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                             <asp:RequiredFieldValidator 
                                runat="server" 
                                ControlToValidate="txtNombre"
                                ErrorMessage="El nombre es obligatorio." 
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                        <div class="mb-3">  
                            <label>Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                             <asp:RequiredFieldValidator 
                                runat="server" 
                                ControlToValidate="txtApellido"
                                ErrorMessage="El apellido es obligatorio." 
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                        <div class="mb-3">
                            <label>DNI</label>
                            <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator 
                                runat="server" 
                                ControlToValidate="txtDNI"
                                ErrorMessage="El DNI es obligatorio." 
                                CssClass="text-danger"
                                Display="Dynamic" />
                            <asp:RegularExpressionValidator
                                runat="server"
                                ControlToValidate="txtDNI"
                                ValidationExpression="^\d{7,9}$"
                                ErrorMessage="Ingrese un DNI válido."
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                        <div class="mb-3">
                            <label>Fecha de Nacimiento</label>
                            <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date" />
                            <asp:RequiredFieldValidator 
                                runat="server"
                                ControlToValidate="txtFechaNacimiento"
                                ErrorMessage="La fecha de nacimiento es obligatoria."
                                CssClass="text-danger"
                                Display="Dynamic" />
                             <asp:RangeValidator 
                                runat="server"
                                ControlToValidate="txtFechaNacimiento"
                                Type="Date"
                                MinimumValue="01/01/1900"
                                MaximumValue="01/01/2026"
                                ErrorMessage="Ingrese una fecha válida."
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                        <div class="mb-3">
                            <label>Teléfono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator 
                                runat="server" 
                                ControlToValidate="txtTelefono"
                                ErrorMessage="El telefono es obligatorio." 
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                        <div class="mb-3">
                            <label>Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator 
                                runat="server" 
                                ControlToValidate="txtEmail"
                                ErrorMessage="El email es obligatorio." 
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                        <div class="mb-3">
                            <label>Dirección</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator 
                                runat="server" 
                                ControlToValidate="txtDireccion"
                                ErrorMessage="La direccion es obligatoria." 
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                                 <div class="mb-3">
                        <label>Obra Social</label>
                        <asp:DropDownList ID="ddlObraSocial" runat="server" CssClass="form-select" />
                    </div>

                        <div class="text-center mt-3">
                            <asp:Button ID="btnAceptar" runat="server" Text="Guardar"
                                CssClass="btn btn-success px-4" OnClick="btnAceptar_Click" />

                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                CssClass="btn btn-secondary ms-3 px-4" OnClick="btnCancelar_Click" CausesValidation="false"/>
                        </div>

                    </div>
                </div>

            </div>
        </div>

    </div>

</asp:Content>
