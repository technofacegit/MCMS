using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
    public partial class TblKullanicilar
    {
        public static TblKullanicilar ValidLogin(string email, string passkey)
        {
            return Global.Context.TblKullanicilars.SingleOrDefault(U => U.KullaniciAdi == email && U.Sifre == passkey);
            
        }
    }
}
