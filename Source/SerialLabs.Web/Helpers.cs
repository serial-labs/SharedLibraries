using seriallabs;
using System;
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


        /*public static float getFirstFloatInsideStringEx(this string input)
        {
            return getFirstFloatInsideStringEx(input);
        }
        public static int getFirstIntInsideStringEx(this string input)
        {
            return getFirstIntInsideStringEx (input);
        }*/
        /// <summary>
        /// Constructs a QueryString (string).
        /// Consider this method to be the opposite of "System.Web.HttpUtility.ParseQueryString"
        /// </summary>
        /// <param name="nvc">NameValueCollection</param>
        /// <returns>String</returns>
        /// <see cref="http://blog.leekelleher.com/2008/06/06/how-to-convert-namevaluecollection-to-a-query-string/"/>
        public static String ConstructQueryString(this System.Collections.Specialized.NameValueCollection parameters)
        {
            List<String> items = new List<String>();

            foreach (String name in parameters)
                items.Add(String.Concat(name, "=", System.Web.HttpUtility.UrlEncode(parameters[name])));

            return String.Join("&", items.ToArray());
        }
        public static String ConstructQueryString(this System.Collections.Specialized.NameValueCollection parameters, Encoding e)
        {
            List<String> items = new List<String>();

            foreach (String name in parameters)
                items.Add(String.Concat(name, "=", System.Web.HttpUtility.UrlEncode(parameters[name], e)));

            return String.Join("&", items.ToArray());
        }
        public static String ConstructQueryStringAddEncoding(this string url, bool special = false)
        {
            //pi : position AFTER separator
            int pi = url.IndexOf("?");
            if (pi > -1) pi += 1;
            else { pi = url.IndexOf(".php"); if (pi > -1) pi += 4; }


            string q = "";
            if (pi > -1) q = url.Substring(pi);

            string f = System.Web.HttpUtility.ParseQueryString(q).ConstructQueryString(Encoding.GetEncoding(1252));
            if (special)
            {
                f = string.Join("/",
                        q.SplitEx("/").Select(w => System.Web.HttpUtility.UrlEncode(w, Encoding.GetEncoding(1252))).ToArray()
                    );
            }

            string ap = (pi < 0 ? "" : url.Substring(0, pi));
            return ap + f;

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

