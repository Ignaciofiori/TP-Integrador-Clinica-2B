<%@ Page Title="Editar Valor de Consulta" Language="C#" 
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="EditarValorConsulta.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.EditarValorConsulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-5">

                <div class="card shadow">
                    <div class="card-body">

                        <h2 class="text-center mb-4">Editar Valor de la Consulta</h2>

                        <asp:ValidationSummary runat="server"
                            CssClass="text-danger text-center mb-3" />
                        <asp:Label 
                            ID="lblError"
                            runat="server"
                            CssClass="text-danger text-center mb-3 d-block"
                            Visible="false" />

                        <div class="mb-3">
                            <label>Valor Actual</label>
                            <asp:TextBox 
                                ID="txtValor" 
                                runat="server" 
                                CssClass="form-control"
                                TextMode="Number"
                                step="0.01" />
                        </div>

                        <div class="text-center mt-4">

                            <asp:Button 
                                ID="btnGuardar"
                                runat="server"
                                Text="Guardar Cambios"
                                CssClass="btn btn-success px-4"
                                OnClick="btnGuardar_Click" />

                            <asp:Button 
                                ID="btnCancelar"
                                runat="server"
                                Text="Cancelar"
                                CssClass="btn btn-secondary ms-3 px-4"
                                OnClick="btnCancelar_Click" />

                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
