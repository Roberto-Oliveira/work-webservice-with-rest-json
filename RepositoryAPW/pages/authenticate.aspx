<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="authenticate.aspx.cs" Inherits="RepositoryAPW.pages.authenticate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                <asp:TextBox ID="tbEmail" runat="server" TextMode="Email" Width="250px" required="true"></asp:TextBox><br />
                <asp:TextBox ID="tbSenha" runat="server" TextMode="Password" Width="200px" required="true"></asp:TextBox><br />
                <br />
                <asp:Button ID="btnAuthenticate" runat="server" Text="Autenticar" OnClick="btnAuthenticate_Click" />
            </p>
            <p>
                <asp:HyperLink ID="hlLogarWithFacebook" runat="server"></asp:HyperLink>
            </p>
            <p>
                <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
            </p>
        </div>
    </form>
</body>
</html>
