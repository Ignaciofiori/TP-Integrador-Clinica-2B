<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Integrador_Clinica_WEB._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row mt-4" aria-labelledby="tituloApp">
            <h1 id="tituloApp">Tu salud, más cerca: agendá tus turnos online.</h1>
            <h3> Más fácil, más rápido. </h3>
        </section>

        <div class="row mt-5">

              <%--desarrollo--%>


              <div class="col-md-4">
                    <label for="ddlProfesional" class="form-label">Por profesional</label>
                    <asp:DropDownList ID="ddlProfesional" runat="server" CssClass="form-select">
                       <%-- <asp:ListItem Text="Seleccione un/a profesional" Value="" />--%>
                    </asp:DropDownList>
              </div>
              
              <div class="col-md-4">
                <label for="ddlEspecialidad" class="form-label">Especialidad</label>
                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                    <%--<asp:ListItem Text="Seleccione una especialidad" Value="" />--%>
                </asp:DropDownList>
              </div>
           

              <div class="col-md-4">
                <label for="ddlCobertura" class="form-label">Cobertura médica</label>
                <asp:DropDownList ID="ddlCobertura" runat="server" CssClass="form-select">
                   <%-- <asp:ListItem Text="Seleccione una cobertura" Value="" />--%>
                </asp:DropDownList>
              </div>

           <%-- aca se ve el resultado de la busqueda--%>
             <div class="col-md-12 text-center mt-5">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
            </div>

            <div class="row mt-4">
                <div class="col-md-12">
                    <asp:GridView ID="gvResultados" runat="server" CssClass="table table-striped table-bordered"
                        AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>

            <%-- fin del desarrollo--%>  
        </div>
    </main>

</asp:Content>
