using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MichealOgundero
{
    public static class AppSettings
    {
        public static string ContactPassword
        {
            get
            {
                return Setting<string>("contactpassword");
            }
        }

        private static T Setting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}