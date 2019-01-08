using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaAppContext
{
    public class Helpers
    {
        internal static string GetUserId(string name)
        {
            using (var db = new OcphDbContext())
            {
                var u = db.Users.Where(O => O.UserName == name).FirstOrDefault();
                if (u != null)
                {
                    return u.Id; }
                else
                {
                    throw new SystemException("Not Found !");
                }

            }
        }

        internal static int GenerateOutgoingCode()
        {
            using (var db = new OcphDbContext())
            {
                var u = db.Outgoing.GetLastItem();
                if (u != null)
                {
                    return u.Code+1;
                }
                else
                {
                    return 1;
                }

            }
        }
        

    }
}
