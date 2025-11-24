<%@ Page Title="Agregar Obra Social" Language="C#" 
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="AgregarObraProfesional.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.AgregarObraProfesional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-6">

                <div class="card shadow">
                    <div class="card-body">

                        <h3 class="text-center mb-4">Agregar Obra Social</h3>

                        <asp:Label 
                            ID="lblError"
                            runat="server"
                            CssClass="text-danger d-block mb-3"
                            Visible="false" />

                        <div class="mb-3">
                            <label>Obra Social</label>
                            <asp:DropDownList 
                                ID="ddlObras"
                                runat="server"
                                CssClass="form-select">
                            </asp:DropDownList>
                        </div>

                        <div class="text-center mt-3">
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
                                OnClick="btnCancelar_Click" />
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
