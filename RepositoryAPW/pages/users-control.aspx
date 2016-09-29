<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="users-control.aspx.cs" Inherits="RepositoryAPW.pages.users_control" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="12000"></asp:ScriptManager>
        <div>
            <p>
                <asp:TextBox ID="tbNome" runat="server" placeholder="Nome do Usuário..."></asp:TextBox>
            </p>
            <p>
                <asp:TextBox ID="tbEmail" runat="server" placeholder="Email do Usuário..."></asp:TextBox>
            </p>
            <p>
                <asp:TextBox ID="tbSenha" runat="server" placeholder="Senha do Usuário..."></asp:TextBox>
            </p>
            <p>
                <asp:Button CssClass="btn btn-primary" ID="btnGravar" runat="server" Text="Gravar Informações" OnClick="btnGravar_Click" />
            </p>
            <hr />
            <br />
            <p>
                <asp:GridView ID="gvControleUsuarios" runat="server" AllowPaging="True" PageSize="5" OnPageIndexChanging="OnPaging" AutoGenerateColumns="False" OnRowCommand="gvControleUsuarios_RowCommand" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Opções" ValidateRequestMode="Enabled">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Editar" Text="edit" />
                                &nbsp;<asp:Button ID="btnExcluir" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Excluir" Text="delete" />
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
                        <asp:TemplateField HeaderText="Nome" SortExpression="Nome">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Nome") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="Email">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Senha" SortExpression="Senha">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Senha") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Senha") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </p>
        </div>
    </form>
</body>
</html>
