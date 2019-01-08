using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TrireksaWebsite
{
    public class RolesAttribute:AuthorizeAttribute
    {
        public RolesAttribute( params string [] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
