using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrireksaAppContext
{
  public  class RolesContext
    {
        public IEnumerable<Iroles> Get()
        {
            using (var db = new OcphDbContext())
            {
                return db.Roles.Select();
            }
        }
    }
}
