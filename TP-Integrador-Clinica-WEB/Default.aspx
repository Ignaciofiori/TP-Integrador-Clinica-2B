<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Integrador_Clinica_WEB._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="tituloApp">
            <h1 id="tituloApp">Tu salud, más cerca: agendá tus turnos online.</h1>
<%--            <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
            <p><a href="http://www.asp.net" class="btn btn-primary btn-md">Learn more &raquo;</a></p>--%>
        </section>

        <div class="row">
              <div class="col-md-4">
                <label for="buscarNombre" class="form-label">Por nombre</label>
                <select id="buscarNombre" name="nombre" class="form-select">
                  <option selected disabled>Seleccione un nombre</option>
                  <option value="1">Dr. Perez</option>
                  <option value="2">Dra. Gomez</option>
                  <option value="3">Dr. Ramirez</option>
                </select>
              </div>


              <div class="col-md-4">
                <label for="buscarEspecialidad" class="form-label">Especialidad</label>
                <select id="buscarEspecialidad" name="especialidad" class="form-select">
                  <option selected disabled>Seleccione una especialidad</option>
                  <option value="1">Cardiología</option>
                  <option value="2">Pediatría</option>
                  <option value="3">Dermatología</option>
                </select>
              </div>
           

              <div class="col-md-4">
                <label for="buscarCobertura" class="form-label">Cobertura médica</label>
                <select id="buscarCobertura" name="cobertura" class="form-select">
                  <option selected disabled>Seleccione una cobertura</option>
                  <option value="1">OSDE</option>
                  <option value="2">Swiss Medical</option>
                  <option value="3">Galeno</option>
                </select>
              </div>


        </div>
    </main>

</asp:Content>
