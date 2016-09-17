using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;

namespace Trabalho.Utility
{
    public class GetFacebookLoginUrl
    {
        public string GetFacebookLogin(string facebook_token)
        {
            //_fb.AppId = "614756975370105";
            //_fb.AppSecret = "5aaaaeb5b72b7c09772ce268a0d30a14";

            FacebookClient myfbclient = new FacebookClient(facebook_token);
            
            dynamic me = myfbclient.Get("me?fields=email");
            return "";
        }

        

    }
}