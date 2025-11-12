<%@ Page Title="Inicia Sesion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<%--    Aca es para iniciar sesion--%>

    <main aria-labelledby="title">

        <%--<h2 id="title"><%: Title %>.</h2>--%>
        <h4 class="text-center text-muted mb-5">¡Bienvenido de vuelta a <strong>SaludYa!</strong></h4>

     <%--   bloque para iniciar sesion--%>
         <div class="row justify-content-center">

            <div class="col-md-4">

                <div class="card shadow-sm p-4">

                    <div class="mb-4">
                        <label for="txtEmail" class="form-label">Correo electrónico:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ejemplo: nombre@correo.com"></asp:TextBox>
                    </div>

                    <div class="mb-4">
                        <label for="txtPassword" class="form-label">Contraseña:</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña"></asp:TextBox>
                    </div>

                    <div class="d-grid mb-3">
                        <asp:Button ID="btnIniciarSesion" runat="server" CssClass="btn btn-primary" Text="Iniciar Sesión" />
                    </div>

                    <div class="text-center">
                        <p class="mb-0">¿No tenés cuenta?
                            <a href="About.aspx" class="text-decoration-none">Registrate aquí</a>
                        </p>
                    </div>

                </div>
            </div>
        </div>
    
    </main>

</asp:Content>
