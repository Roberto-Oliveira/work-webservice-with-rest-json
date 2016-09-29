using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using RepositoryBLL;
using TrabalhoRestAPW.webservices;
using TrabalhoRestBLL;

namespace RepositoryAPW.pages
{
    public partial class users_control : Page
    {
        private readonly webservicewithrest ws = new webservicewithrest();
        private readonly JavaScriptSerializer jss = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            gvDataBind();
        }

        private void gvDataBind()
        {
            var lista_de_usuarios = ws.return_list_users();
            var dados = jss.Deserialize<List<Usuario>>(lista_de_usuarios);

            gvControleUsuarios.DataSource = dados;
            gvControleUsuarios.DataBind();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gvControleUsuarios.PageIndex = e.NewPageIndex;
            gvControleUsuarios.DataBind();
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            ws.create_user(tbNome.Text, tbEmail.Text, tbSenha.Text);

            gvControleUsuarios.DataBind();

            Auxiliar.ClearControls(this);
        }

        protected void gvControleUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                var id = Convert.ToInt32(e.CommandArgument);
                var linha = gvControleUsuarios.Rows[id];
            }
        }
    }
}