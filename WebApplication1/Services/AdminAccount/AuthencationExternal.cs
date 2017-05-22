using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services.AdminAccount
{
    public class AuthencationExternal
    {
        private const string googleClientID = "700097064640-mv4l7qpltjkaruc6srvo121jkqd2uc7r.apps.googleusercontent.com";
        private const string facebookClientID = "319131278501042";
        private const string googleID = "8A1iFQ1LB1pzhhNTdHkT5T32";
        private const string facebookSecrect = "77ee7f3722bb6aa99bbb1383706620f5";
        
        public static string getGoogleID()
        {
            return googleClientID;
        }
        public static string getGoogleSecrect()
        {
            return googleID;
        }
        public static string getFacebookClientID()
        {
            return facebookClientID;
        }
        public static string getFacebookSecrect()
        {
            return facebookSecrect;
        }
    }
}
