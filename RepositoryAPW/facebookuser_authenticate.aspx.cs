using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Configuration;
using TrabalhoRestBLL;

namespace RepositoryAPW
{
    public partial class facebookuser_authenticate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the Facebook code from the querystring
            if (Request.QueryString["code"] != "")
            {
                var obj = GetFacebookUser(Request.QueryString["code"]);
            }
        }

        protected List<FacebookUser> GetFacebookUser(string code)
        {
            // Exchange the code for an access token
            var targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/user_authenticate.aspx&code=" + code);
            var at = (HttpWebRequest)WebRequest.Create(targetUri);

            var str = new StreamReader(at.GetResponse().GetResponseStream());
            var token = str.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            var combined = token.Split('&');
            var accessToken = combined[0];

            // Exchange the code for an extended access token
            var eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
            var eat = (HttpWebRequest)WebRequest.Create(eatTargetUri);

            var eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
            var eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            var eatWords = eatToken.Split('&');
            var extendedAccessToken = eatWords[0];

            // Request the Facebook user information
            var targetUserUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,gender,locale,link&access_token=" + accessToken);
            var user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

            // Read the returned JSON object response
            var userInfo = new StreamReader(user.GetResponse().GetResponseStream());
            var jsonResponse = string.Empty;
            jsonResponse = userInfo.ReadToEnd();

            // Deserialize and convert the JSON object to the Facebook.User object type
            var jss = new JavaScriptSerializer();
            var json = jsonResponse;
            var faceUser = jss.Deserialize<FacebookUser>(json);

            // Write the user data to a List
            var currentUser = new List<FacebookUser>();
            currentUser.Add(faceUser);

            // Return the current Facebook user
            return currentUser;
        }
    }
}