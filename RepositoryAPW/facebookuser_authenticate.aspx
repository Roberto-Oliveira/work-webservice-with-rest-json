<%@ Page Title="" Language="C#" MasterPageFile="~/masters/Mestre.Master" AutoEventWireup="true" CodeBehind="facebookuser_authenticate.aspx.cs" Inherits="RepositoryAPW.facebookuser_authenticate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>Área de Login</legend>

        <p>
            <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="tbSenha" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnAutenticar" runat="server" Text="Autenticar" />
        </p>
        <p>
            <asp:HyperLink ID="hlLogarFacebook" runat="server">Logar Facebook</asp:HyperLink>
        </p>
        <p>
            <asp:Label ID="lblUsuarioFacebook" runat="server" Text=""></asp:Label></p>
    </fieldset>
</asp:Content>
