using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Principal;

namespace MS.Web.Models.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public Guid UserID { get; set; }
        public string UserName { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Roles { get; set; }

        [JsonIgnore]
        public IIdentity Identity { get; private set; }

        public CustomPrincipal() { }

        public CustomPrincipal(string userName, params byte[] roleTypes)
        {
            this.Identity = new GenericIdentity(userName);
            this.UserName = userName;
            this.Roles = roleTypes;
        }

        public CustomPrincipal(dynamic user, params byte[] roleTypes)
        {
            this.Identity = new GenericIdentity(user.UserName);
            this.UserName = user.UserName;
            this.FirstName = user.UserDetail != null ? user.UserDetail.FirstName : user.FirmDetail.Name;
            this.LastName = user.UserDetail != null ? (!string.IsNullOrWhiteSpace(user.UserDetail.LastName) ? user.UserDetail.LastName : string.Empty) : string.Empty;
            this.Roles = roleTypes;
        }

        public bool IsInRole(Object roleType)
        {
            return Roles.Contains((byte)roleType);
        }

        public bool IsInRole(params Object[] roleTypes)
        {
            return roleTypes.Any(r => Roles.Contains((byte)r));
        }

        public bool IsInRole(string role)
        {
            //Check with enum
            //Object roleType;
            //if (Enum.TryParse(role, out roleType)) { return IsInRole(roleType); }
            return false;
        }
    }
}
