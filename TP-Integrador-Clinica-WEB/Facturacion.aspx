<%@ Page Title="Facturación" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Facturacion.aspx.cs"
    Inherits="TP_Integrador_Clinica_WEB.Facturacion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <h2 class="mb-4">Listado de Facturación</h2>

        <div class="row mb-4 p-3 border rounded shadow-sm bg-light">

            <div class="col-md-3">
                <label class="form-label">Filtrar por:</label>
                <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione Campo" Value="0" />
                    <asp:ListItem Text="Paciente" Value="Paciente" />
                    <asp:ListItem Text="Profesional" Value="Profesional" />
                    <asp:ListItem Text="Especialidad" Value="Especialidad" />
                    <asp:ListItem Text="Obra Social" Value="ObraSocial" />
                    <asp:ListItem Text="Monto mayor a..." Value="MontoMayor" />
                    <asp:ListItem Text="Monto menor a..." Value="MontoMenor" />
                    <asp:ListItem Text="Fecha" Value="Fecha" />
                </asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label class="form-label">Valor:</label>
                <div class="input-group">
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"
                        placeholder="Ingrese valor..." />

                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                        CssClass="btn btn-primary"
                        OnClick="btnBuscar_Click" />
                </div>
            </div>

            <div class="col-md-3 d-flex align-items-end">
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Filtro"
                    CssClass="btn btn-outline-secondary w-100"
                    OnClick="btnLimpiar_Click" />
            </div>

       <hr />
        </div>
                               <asp:Button 
                       ID="btnRecaudaciones" 
                       runat="server" 
                       Text="Recaudaciones"
                       CssClass="btn btn-info mb-3"
                       OnClick="btnRecaudaciones_Click" />

        <asp:GridView ID="gvFacturas" runat="server"
            AutoGenerateColumns="false"
            CssClass="table table-striped table-hover table-bordered">

            <Columns>

                <asp:BoundField DataField="IdFactura" HeaderText="Factura #" />

                <asp:BoundField DataField="PacienteNombreCompleto" HeaderText="Paciente" />

                <asp:BoundField DataField="ProfesionalNombreCompleto" HeaderText="Profesional" />

                <asp:BoundField DataField="EspecialidadNombre" HeaderText="Especialidad" />

                <asp:BoundField DataField="ObraSocialNombre" HeaderText="Obra Social" />

                <asp:BoundField DataField="ConsultorioDisplay" HeaderText="Consultorio" />

                <asp:BoundField DataField="FechaTurno" HeaderText="Fecha Turno"
                    DataFormatString="{0:dd/MM/yyyy}" />

                <asp:TemplateField HeaderText="Hora Turno">
                    <ItemTemplate>
                        <%# ((TimeSpan)Eval("HoraTurno")).ToString(@"hh\:mm") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total"
                    DataFormatString="{0:C}" HtmlEncode="false" />

                <asp:BoundField DataField="FechaEmision" HeaderText="Fecha Emisión"
                    DataFormatString="{0:dd/MM/yyyy}" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
