using System.Configuration;

namespace MS.Web.Code.LIBS
{
    public static class SiteKeys
    {
        public static string Domain
        {
            get { return ConfigurationManager.AppSettings["domain"]; }
        }

        public static string DBImagePath
        {
            get { return ConfigurationManager.AppSettings["DBImagePath"]; }
        }
    }
}
