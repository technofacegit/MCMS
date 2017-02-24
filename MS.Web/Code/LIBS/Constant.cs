﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Code.LIBS
{
    public class Constant
    {
        public static IDictionary<string, string> DisplayOrder
        {
            get
            {
                return new Dictionary<string, string>
                    {
                         {"Select Display Order","0"},
                         {"1","1"},
                         {"2","2"},
                         {"3","3"},
                         {"4","4"},
                         {"5","5"},
                         {"6","6"},
                         {"7","7"},
                         {"8","8"},
                         {"9","9"},
                         {"10","10"}
                        
                    };
            }
        }
    }
}