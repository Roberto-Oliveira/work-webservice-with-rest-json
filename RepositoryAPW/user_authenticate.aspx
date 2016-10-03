<%@ Page Title="" Language="C#" MasterPageFile="~/masters/Mestre.Master" AutoEventWireup="true" CodeBehind="user_authenticate.aspx.cs" Inherits="RepositoryAPW.user_authenticate" %>

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
            <asp:Button ID="btnAutenticar" runat="server" Text="Button" OnClick="btnAutenticar_Click" />
        </p>
    </fieldset>
</asp:Content>
