using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Code.Language
{
    public class AccessResouceFile
    {
        public string gtresource(string rulename)
        {
            string value = null;
            System.Resources.ResourceManager RM = new System.Resources.ResourceManager("MS.Web.Code.Language.Resource_tr_TR", this.GetType().Assembly);
            value = RM.GetString(rulename).ToString();

            if (value != null && value != "")
            {
                return value;

            }
            else
            {
                return "";
            }

        }
    }
}