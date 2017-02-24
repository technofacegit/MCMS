using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
   public partial class tblImage
    {
       public static List<tblImage> Getimages()
       {
           return Global.Context.tblImages.ToList();
       }
       public static tblImage Getimage(int id)
       {
           return Global.Context.tblImages.FirstOrDefault(x=>x.id == id);
       }
    }
}
