using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;

namespace Trabalho.Utility
{
    public class FacebookConnection
    {
        /// <summary>
        /// Retorna os dados do usuario
        /// </summary>
        /// <param name="facebook_token"></param>
        /// <returns></returns>
        public string GetFacebookParameters(string facebook_token)
        {
            try
            {
                FacebookClient facebookClient = new FacebookClient(facebook_token);
                facebookClient.AppId = "614756975370105";
                facebookClient.AppSecret = "5aaaaeb5b72b7c09772ce268a0d30a14";
                return facebookClient.Get("me?fields=email,name").ToString();
            }
            catch (FacebookOAuthException)
            {
                throw new Exception("Token de acesso já está expirado.");
            }
         
        }
    }
}