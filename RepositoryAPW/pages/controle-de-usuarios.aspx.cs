﻿using System;
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
        private readonly string url = "http://localhost:21315/pages/controle-de-usuarios.aspx";

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
            gvDataBind();
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            var id = Session["Id"];

            try
            {
                if (id == null)
                {
                    if (!ws.check_user(tbEmail.Text))
                    {
                        ws.create_user(tbNome.Text, tbEmail.Text, tbSenha.Text);
                        gvDataBind();

                        Auxiliar.ClearControls(this);
                    }
                    else
                    {
                        throw new Exception("Já existe cadastro com este email.");
                    }
                }
                else
                {
                    ws.update_user(Convert.ToInt32(id), tbNome.Text, tbEmail.Text, tbSenha.Text);
                    gvDataBind();

                    tbEmail.Enabled = true;

                    Auxiliar.ClearControls(this);

                    throw new Exception("Dados atualizados com sucesso.");
                }

                Session.Clear();

            }
            catch (Exception ex)
            {
                lblMensagem.Visible = true;
                lblMensagem.Text = ex.Message;
                Session.Clear();
            }
        }

        protected void gvControleUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Select")
                {
                    tbEmail.Enabled = false;

                    var dado = ws.return_user_by_id(id.ToString());

                    var usuario = jss.Deserialize<Usuario[]>(dado);

                    Session["Id"] = usuario[0].Id;
                    tbNome.Text = usuario[0].Nome;
                    tbEmail.Text = usuario[0].Email;
                    tbSenha.Text = usuario[0].Senha;
                }
                else if (e.CommandName == "delete")
                {
                    ws.delete_user(id);
                    gvDataBind();
                    Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Visible = true;
                lblMensagem.Text = ex.Message;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Auxiliar.ClearControls(this);
            lblMensagem.Text = "";
            tbEmail.Enabled = true;
            Session.Clear();
        }
    }
}