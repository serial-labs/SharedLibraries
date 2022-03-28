﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SerialLabs.Web
{
    public static class Helpers
    {
        public static string getStringValueOrEmpty(this System.Web.HttpSessionStateBase Session, string key)
        {
            if (Session[key] == null) return "";
            return Session[key].ToString();
        }
        public static string getStringValueOrEmpty(this System.Web.SessionState.HttpSessionState Session, string key)
        {
            return new HttpSessionStateWrapper(Session).getStringValueOrEmpty(key);
        }

        public static string getSessionStringValueOrEmpty(string key)
        {
            HttpContext context = HttpContext.Current;
            if (context == null) return "";
            if (context.Session == null) return "";
            return context.Session.getStringValueOrEmpty(key);
            //return new HttpSessionStateWrapper(context.Session).getStringValueOrEmpty(key)
            //if (context.Session[key] == null) return "";
            //return context.Session[key].ToString();
        }

        public static bool isUserDev()
        {
            return (HttpContext.Current.IsDebuggingEnabled || HttpContext.Current.Session.getStringValueOrEmpty("isUserDev") != "" || hasUserDevIP());
        }

        private static bool hasUserDevIP()
        {
            string userHostAdress = HttpContext.Current.Request.UserHostAddress;

            string[] devAdresses = {
                "localhost", // Localhost
                "127.0.0.1", // Localhost
                "::1", // Localhost
                "77.130.42.235" // PC Corentin at the office 
            };

            for(int i = 0; i < devAdresses.Length; i++)
            {
                if (devAdresses[i] == userHostAdress)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
