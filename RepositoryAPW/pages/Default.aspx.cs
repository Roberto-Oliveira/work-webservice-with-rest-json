using System;
using System.Configuration;

namespace RepositoryAPW.pages
{
    public partial class Default : System.Web.UI.Page
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
    }
}