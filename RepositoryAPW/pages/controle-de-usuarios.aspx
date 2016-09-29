<%@ Page Title="" Language="C#" MasterPageFile="~/masters/Mestre.Master" AutoEventWireup="true" CodeBehind="controle-de-usuarios.aspx.cs" Inherits="RepositoryAPW.pages.controle_de_usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p class="input-field">
        <i class="material-icons prefix">account_circle</i>
        <asp:TextBox ID="tbNome" runat="server" CssClass="validate" MaxLength="50" length="50" placeholder="Nome:" required="true"></asp:TextBox>
    </p>
    <p class="input-field">
        <i class="material-icons prefix">email</i>
        <asp:TextBox ID="tbEmail" runat="server" CssClass="validate" MaxLength="50" length="50" placeholder="Email:" required="true"></asp:TextBox>
    </p>
    <p class="input-field">
        <i class="material-icons prefix">vpn_key</i>
        <asp:TextBox ID="tbSenha" runat="server" CssClass="validate" MaxLength="50" length="50" placeholder="Senha:" required="true"></asp:TextBox>
    </p>
    <p>
        <i class="btn-floating btn-medium waves-effect waves-light green material-icons">
            <asp:Button ID="btnGravar" runat="server" Text="add" OnClick="btnGravar_Click" ToolTip="Gravar Informações" />
        </i>
        <i class="btn-floating btn-medium waves-effect waves-light red material-icons">
            <asp:Button ID="btnLimpar" runat="server" Text="clear" OnClick="btnLimpar_Click" ToolTip="Limpar Campos" />
        </i>
    </p>
    <hr />
    <br />
    <p>
        <asp:GridView CssClass="responsive-table bordered highlight" ID="gvControleUsuarios" runat="server" AllowPaging="True" PageSize="5" OnPageIndexChanging="OnPaging" AutoGenerateColumns="False" OnRowCommand="gvControleUsuarios_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEditar" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Id") %>' CommandName="add" Text="Editar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deletar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDeletar" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Id") %>' CommandName="delete" OnClientClick="return confirm('Deseja concluir a exclusão?')">Deletar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbId" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Senha" HeaderText="Senha" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
