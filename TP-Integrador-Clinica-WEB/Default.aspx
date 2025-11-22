<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Integrador_Clinica_WEB._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="mt-4">

        <!-- MENSAJE DE BIENVENIDA -->
        <section class="text-center mb-4">
            <h1 class="display-6">Bienvenido al Panel Administrativo de SaludYa!</h1>
            <p class="lead text-muted">
                Gestioná pacientes, profesionales, turnos y facturación desde el menú superior.
            </p>
        </section>

        <!-- BANNER CENTRAL -->
        <section class="text-center">
           <img src="Content/Images/banner-clinica.png" 
             alt="Banner de la clínica"
             class="img-fluid w-100 shadow"
             style="
                max-height: 490px; 
                object-fit: cover; 
                border-radius: 12px;
             " />
        </section>

        <!-- MENSAJE FINAL (opcional) -->
        <section class="text-center mt-4">
            <p class="text-secondary">
                Administrá con confianza. Todo está al alcance de un clic..
            </p>
        </section>

    </main>
</asp:Content>
