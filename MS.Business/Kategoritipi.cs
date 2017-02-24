using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
    public partial class Kategoritipi
    {
        public static List<Kategoritipi> GetCategoryType()
        {
            return Global.Context.Kategoritipis.ToList();
        }
    }
}
