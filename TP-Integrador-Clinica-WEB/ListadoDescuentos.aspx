<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoHorarios.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.ListadoHorarios" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Listado de Horarios</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="container mt-4">
            <h2 class="mb-4">Listado de Horarios</h2>

            <asp:GridView 
                ID="gvHorarios"
                runat="server"
                CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False"
                DataKeyNames="Id"
                OnRowCommand="gvHorarios_RowCommand">

                <Columns>

                    <asp:BoundField DataField="Id" HeaderText="ID" />

                    <asp:BoundField DataField="NombreCompletoProfesional" HeaderText="Profesional" />

                    <asp:BoundField DataField="DiaSemana" HeaderText="Día" />

                    <asp:BoundField DataField="HoraInicio" HeaderText="Inicio" />

                    <asp:BoundField DataField="HoraFin" HeaderText="Fin" />

                    <asp:ButtonField 
                        Text="Eliminar"
                        CommandName="Eliminar"
                        ButtonType="Button"
                        ControlStyle-CssClass="btn btn-danger btn-sm" />

                </Columns>

            </asp:GridView>

        </div>

    </form>
</body>
</html>
