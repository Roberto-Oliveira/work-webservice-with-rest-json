<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="RepositoryAPW.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Área de Login</title>
    <link rel="icon" href="images/favicon.ico" type="image/gif" sizes="16x16" />
    <link href="css/style.css" rel="stylesheet" />

</head>
<body>

    <div class="wrapper">
        <div class="container">
            <form class="form" runat="server">
                <div id="login">
                    <p><asp:Label ID="lblUsuario" runat="server" Text="Usuário"></asp:Label>
                    <asp:TextBox ID="tbUsuario" runat="server" required="true"></asp:TextBox>
                    <asp:Label ID="lblSenha" runat="server" Text="Senha"></asp:Label>
                    <asp:TextBox ID="tbSenha" runat="server" TextMode="Password" required="true"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnLogar" runat="server" Text="Logar" OnClick="btnLogar_Click" />
                    </p>
                    <p><asp:HyperLink ID="hlLogarWithFacebook" runat="server"></asp:HyperLink></p>
                </div>
            </form>
        </div>
        <ul class="bg-bubbles">
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
        </ul>
    </div>

    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="/js/index.js"></script>

</body>
</html>
