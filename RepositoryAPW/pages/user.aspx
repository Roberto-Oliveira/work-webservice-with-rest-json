<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="RepositoryAPW.pages.user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ListView ID="lvFacebookUser" runat="server">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>Facebook User Id:</td>
                            <td><%# Eval("id") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>Facebook User Name:</td>
                            <td><%# Eval("name") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>Facebook User Email:</td>
                            <td><%# Eval("email") %><br />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
