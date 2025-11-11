<%@ Page Title="Registrate!" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
      <%--  <h2 id="Registro"><%: Title %>.</h2>--%>
        <h2 class="mb-4">¡Elegí cómo querés registrarte!</h2>

          <div class="row justify-content-center">
            <!-- Paciente -->
            <div class="col-md-5">
                <div class="card shadow-sm mb-4 p-4">
                    <h4 class="mb-3">Paciente</h4>
                    <p>Accedé a turnos online, gestioná tus citas y consultá con profesionales.</p>
                    <a href="RegistroPaciente.aspx" class="btn btn-primary">Registrarme como Paciente</a>
                </div>
            </div>

    </main>
</asp:Content>
