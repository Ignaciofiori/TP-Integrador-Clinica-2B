<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TP_Integrador_Clinica_WEB.Login" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Inicio de Sesión</title>
    <style>
        body {
            background-color: #f4f4f4;
            font-family: Arial;
        }
        .login-container {
            width: 350px;
            margin: 120px auto;
            padding: 25px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 0 10px #ccc;
        }
        .title {
            text-align: center;
            margin-bottom: 20px;
            font-size: 20px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="login-container">

            <div class="title">Iniciar Sesión</div>

            <div class="campo">
                <asp:Label Text="Usuario: " runat="server" />
                <asp:TextBox ID="txtUser" CssClass="form-control" runat="server" />
            </div>

            <div class="campo">
                <asp:Label Text="Contraseña: " runat="server" />
                <asp:TextBox ID="txtPass" TextMode="Password" CssClass="form-control" runat="server" />
            </div>

            <asp:Button 
                ID="btnLogin" 
                runat="server" 
                Text="Ingresar" 
                CssClass="btn btn-primary" 
                style="margin-top:15px; width:100%;" 
                OnClick="btnLogin_Click" />

            <asp:Label ID="lblError" runat="server" ForeColor="Red" />

        </div>

    </form>
</body>
</html>
