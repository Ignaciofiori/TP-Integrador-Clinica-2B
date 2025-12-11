<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelProfesional.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.PanelProfesional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Panel del Profesional</h2>

    <asp:Label ID="lblBienvenida" runat="server"></asp:Label>

    <br /> <%--//quitarlo dps--%>

    <a href="MisTurnos.aspx" class="btn btn-primary">Ver mis turnos</a>

</asp:Content>
