using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using RepositoryBLL;
using TrabalhoRestAPW.webservices;
using TrabalhoRestBLL;
using System.Web.UI.WebControls;

namespace RepositoryAPW.pages
{
    public partial class controle_de_usuarios : Page
    {
        private readonly webservicewithrest ws = new webservicewithrest();
        private readonly JavaScriptSerializer jss = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvDataBind();
            }
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
            gvDataBind();
        }


        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (Session["Id"] == null)
            {
                ws.create_user(tbNome.Text, tbEmail.Text, tbSenha.Text);
                gvDataBind();

                Auxiliar.ClearControls(this);
            }
            else
            {
                ws.update_user(Convert.ToInt32(Session["Id"]), tbNome.Text, tbEmail.Text, tbSenha.Text);
                gvDataBind();

                Auxiliar.ClearControls(this);
            }
        }

        protected void gvControleUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(e.CommandArgument);

                switch (e.CommandName)
                {
                    case "add":

                        var dado = ws.return_user_by_id(id.ToString());

                        var usuario = jss.Deserialize<Usuario[]>(dado);

                        Session["Id"] = usuario[0].Id;
                        tbNome.Text = usuario[0].Nome;
                        tbEmail.Text = usuario[0].Email;
                        tbSenha.Text = usuario[0].Senha;

                        break;

                    case "delete":

                        ws.delete_user(id);

                        gvDataBind();

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Auxiliar.ClearControls(this);
            gvDataBind();
        }
    }
}