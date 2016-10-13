using System;
using System.Configuration;
using TrabalhoRestBLL;
using System.Linq;

namespace RepositoryAPW.pages
{
    public partial class authenticate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hlLogarWithFacebook.NavigateUrl = "https://www.facebook.com/v2.4/dialog/oauth/?client_id=" +
                                             ConfigurationManager.AppSettings["FacebookAppId"] +
                                             "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" +
                                             Request.ServerVariables["SERVER_PORT"] +
                                             "/pages/user.aspx&response_type=code&state=1";

            hlLogarWithFacebook.Text = "Login with Facebook";
        }

        protected void btnAuthenticate_Click(object sender, EventArgs e)
        {
            var ws = new wswithrest.webservicewithrest();
            var dc = new webservicewithrestDataContext();

            if (!ws.check_user_authenticate(tbEmail.Text, tbSenha.Text))
            {
                lblMensagem.Text = "Error: Seu usuário ou senha não conferem.";
            }

            var usuario = dc.Usuarios.
                Where(u => u.Email == tbEmail.Text).
                Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    u.Senha
                });

            Session["user"] = usuario;

            Response.Redirect("controle-de-usuarios.aspx");
        }
    }
}