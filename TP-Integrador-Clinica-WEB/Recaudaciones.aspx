<%@ Page Title="Recaudaciones" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Recaudaciones.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.Recaudaciones" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <h2 class="mb-4">Panel de Recaudación</h2>

        <asp:Button 
            ID="btnVolver" 
            runat="server" 
            Text="← Volver a Facturación" 
            CssClass="btn btn-secondary mb-4"
            OnClick="btnVolver_Click" />

        <div class="row text-center">

            <div class="col-md-6 mb-4">
                <asp:Button 
                    ID="btnPorObra" 
                    runat="server" 
                    Text="Recaudación por Obra Social"
                    CssClass="btn btn-primary w-100 p-4 shadow"
                    OnClick="btnPorObra_Click" />
            </div>

            <div class="col-md-6 mb-4">
                <asp:Button 
                    ID="btnPorProfesional" 
                    runat="server" 
                    Text="Recaudación por Profesional"
                    CssClass="btn btn-success w-100 p-4 shadow"
                    OnClick="btnPorProfesional_Click" />
            </div>

            <div class="col-md-6 mb-4">
                <asp:Button 
                    ID="btnPorEspecialidad" 
                    runat="server" 
                    Text="Recaudación por Especialidad"
                    CssClass="btn btn-warning w-100 p-4 shadow"
                    OnClick="btnPorEspecialidad_Click" />
            </div>

            <div class="col-md-6 mb-4">
                <asp:Button 
                    ID="btnPorMes" 
                    runat="server" 
                    Text="Recaudación Mensual"
                    CssClass="btn btn-info w-100 p-4 shadow"
                    OnClick="btnPorMes_Click" />
            </div>

        </div>

        <hr />

        <asp:GridView ID="gvRecaudacion" runat="server"
            AutoGenerateColumns="true"
            CssClass="table table-striped table-hover table-bordered mt-3">
        </asp:GridView>

    </div>

</asp:Content>
