using System.Configuration;
using RepositoryBLL;
using System;
using System.DirectoryServices;
using System.Web.UI;

namespace RepositoryAPW
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            #region// Autenticando o Usuário

            const string dominio = "dpge.MS";
            const string caminho = "LDAP://s176:3268";

            #endregion
            try
            {
                if (!AutenticarUsuario(dominio, tbUsuario.Text, tbSenha.Text, caminho)) return;
                Session["logged"] = true;
                Response.Redirect(Session["PageBeforeLogged"].ToString(), false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AutenticarUsuario(string dominio, string usuario, string senha, string caminho)
        {
            try
            {
                #region //logando usuário

                var dominioUsuario = dominio + @"\" + usuario;
                var entry = new DirectoryEntry(caminho, dominioUsuario, senha);
                var search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + usuario + ")" };
                search.PropertiesToLoad.Add("cn");
                var result = search.FindOne();

                #endregion

                #region //usuário logado

                Session["loggeduser"] = usuario;
                Session["loggedusername"] = result.Properties["cn"][0];
                Session["machinename"] = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName;
                Session["machineip"] = Request.UserHostAddress;

                #endregion

                return true;
            }
            catch
            {
                throw new Exception(Mensagems.FalhaLogon());
            }
        }
    }
}