<%@ Page Title="Agregar Especialidad" Language="C#" 
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="AgregarEspecialidadProfesional.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.AgregarEspecialidadProfesional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-6">

                <div class="card shadow">
                    <div class="card-body">

                        <h2 class="text-center mb-4">Agregar Especialidad</h2>

                    <%--<asp:ValidationSummary 
                        ID="ValidationSummary1"
                        runat="server" 
                        CssClass="text-danger text-center mb-3" />--%>

                            <asp:Label 
                            ID="lblError"
                            runat="server"
                            CssClass="text-danger text-center mb-3 d-block"
                            Visible="false" />

                        <div class="mb-3">
                            <label>Especialidad</label>
                            <asp:DropDownList 
                                ID="ddlEspecialidad" 
                                runat="server" 
                                CssClass="form-select">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator 
                                runat="server"
                                ControlToValidate="ddlEspecialidad"
                                InitialValue=""
                                ErrorMessage="Debe seleccionar una especialidad."
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                        <div class="mb-3">
                            <label>Valor de la Consulta</label>
                            <asp:TextBox 
                                ID="txtValor" 
                                runat="server" 
                                CssClass="form-control" 
                                TextMode="Number" />

                            <asp:RequiredFieldValidator 
                                runat="server"
                                ControlToValidate="txtValor"
                                ErrorMessage="Debe ingresar un valor."
                                CssClass="text-danger"
                                Display="Dynamic" />

                            <asp:RangeValidator 
                                runat="server"
                                ControlToValidate="txtValor"
                                Type="Double"
                                MinimumValue="1"
                                MaximumValue="999999"
                                ErrorMessage="Ingrese un valor válido."
                                CssClass="text-danger"
                                Display="Dynamic" />
                        </div>

                        <div class="text-center mt-4">
                            <asp:Button 
                                ID="btnGuardar" 
                                runat="server" 
                                Text="Guardar"
                                CssClass="btn btn-success px-4"
                                OnClick="btnGuardar_Click" />

                            <asp:Button 
                                ID="btnCancelar" 
                                runat="server" 
                                Text="Cancelar"
                                CssClass="btn btn-secondary ms-3 px-4"
                                OnClick="btnCancelar_Click" 
                                CausesValidation="false" />
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
