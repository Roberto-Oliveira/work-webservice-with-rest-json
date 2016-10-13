<%@ Page Title="" Language="C#" MasterPageFile="~/masters/Mestre.Master" AutoEventWireup="true" CodeBehind="controle-de-usuarios.aspx.cs" Inherits="RepositoryAPW.pages.controle_de_usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="input-field col s4">
        <i class="material-icons prefix">account_circle</i>
        <asp:TextBox ID="tbNome" runat="server" CssClass="validate" MaxLength="50" length="50" placeholder="Nome:" required="true"></asp:TextBox>
    </div>
    <div class="input-field col s4">
        <i class="material-icons prefix">email</i>
        <asp:TextBox ID="tbEmail" runat="server" CssClass="validate" MaxLength="50" length="50" placeholder="Email:" required="true"></asp:TextBox>
    </div>
    <div class="input-field col s4">
        <i class="material-icons prefix">vpn_key</i>
        <asp:TextBox ID="tbSenha" runat="server" CssClass="validate" MaxLength="50" length="50" placeholder="Senha:" required="true"></asp:TextBox>
    </div>
    <p>
        <i class="btn-floating btn-medium waves-effect waves-light green material-icons">
            <asp:Button ID="btnGravar" runat="server" Text="add" OnClick="btnGravar_Click" ToolTip="Gravar Informações" />
        </i>
        <i class="btn-floating btn-medium waves-effect waves-light red material-icons">
            <asp:Button ID="btnLimpar" runat="server" Text="clear" OnClick="btnLimpar_Click" ToolTip="Limpar Campos" />
        </i>
        <p><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text="Sair"></asp:LinkButton></p>
         
    </p>
    <hr />
    <br />
    <p>
        <asp:GridView CssClass="responsive-table bordered highlight" ID="gvControleUsuarios" runat="server" AllowPaging="True" PageSize="5" OnPageIndexChanging="OnPaging" AutoGenerateColumns="False" OnRowCommand="gvControleUsuarios_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Selecionar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="ibSelecionar" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Id") %>' CommandName="Select" Text="Selecionar" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deletar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="ibDeletar" runat="server" CausesValidation="False" CommandName="delete" OnClientClick="return confirm('Deseja concluir a exclusão?')" Text="Deletar" CommandArgument='<%# Bind("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id" SortExpression="Id">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbId" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Nome" SortExpression="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Senha" HeaderText="Senha" SortExpression="Senha" />
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:Label ID="lblMensagem" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    </p>
</asp:Content>
