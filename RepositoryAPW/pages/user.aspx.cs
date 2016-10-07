using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using RepositoryAPW.wswithrest;
using TrabalhoRestBLL;

namespace RepositoryAPW.pages
{
    public partial class user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the Facebook code from the querystring
            if (Request.QueryString["code"] != "")
            {
                var ws = new webservicewithrest();

                var obj = GetFacebookUser(Request.QueryString["code"]);

                ws.create_user(obj.Nome, obj.Email, obj.Senha);

                Response.Redirect("controle-de-usuarios.aspx");
            }
        }

        protected Usuario GetFacebookUser(string code)
        {
            var usuario = new Usuario();

            // Substituir o código para um token de acesso
            var targetUri =
                new Uri("https://graph.facebook.com/oauth/access_token?client_id=" +
                        ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" +
                        ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" +
                        Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] +
                        "/pages/user.aspx&code=" + code);
            var at = (HttpWebRequest)WebRequest.Create(targetUri);

            var str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
            var token = str.ReadToEnd().ToString().Replace("access_token=", "");

            // Dividi o token de acesso e de expiração em uma única string
            var combined = token.Split('&');
            var accessToken = combined[0];

            // Substituir o código para um token de acesso extend
            var eatTargetUri =
                new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" +
                        ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" +
                        ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
            var eat = (HttpWebRequest)WebRequest.Create(eatTargetUri);

            var eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
            var eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

            // Dividir o token de acesso e de expiração da única string
            var eatWords = eatToken.Split('&');
            var extendedAccessToken = eatWords[0];

            // Solicita as informações do usuário Facebook
            var targetUserUri = new Uri("https://graph.facebook.com/me?fields=id,name,email&access_token=" + accessToken);
            var user = (HttpWebRequest)WebRequest.Create(targetUserUri);

            // Le a resposta e retorna um objeto JSON
            var userInfo = new StreamReader(user.GetResponse().GetResponseStream());
            var jsonResponse = userInfo.ReadToEnd();

            // Deserialize e converte o objeto JSON para o tipo de objeto Facebook.User
            var jss = new JavaScriptSerializer();
            var facebookUser = jss.Deserialize<Usuario>(jsonResponse);

            // Escreve os dados do usuário em uma lista
            //var currentUser = new List<FacebookUser> { facebookUser };

            // Retorna o usuário atual Facebook
            return facebookUser;
        }
    }
}
