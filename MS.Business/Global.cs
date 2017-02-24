using System.Web;

namespace MS.Business
{
    public sealed class Global
    {
        public static ProjectDbContext Context
        {
            get
            {
                string ocKey = "dots_" + HttpContext.Current.GetHashCode().ToString("x");
                if (!HttpContext.Current.Items.Contains(ocKey)) { HttpContext.Current.Items.Add(ocKey, new ProjectDbContext()); }
                return HttpContext.Current.Items[ocKey] as ProjectDbContext;
            }
        }
    }
}
