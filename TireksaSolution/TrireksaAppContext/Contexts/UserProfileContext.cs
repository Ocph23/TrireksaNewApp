using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ModelsShared;

namespace TrireksaAppContext
{
    public class UserProfileContext
    {
        public IEnumerable<userprofile> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = (from u in db.Users.Select()
                             join p in db.UserProfiles.Select() on u.Id equals p.UserCode
                             select new userprofile
                             {
                                 Id=u.Id,
                                 PhoneNumber = u.PhoneNumber,
                                 Address = p.Address,
                                 Email = u.Email,
                                 UserId = p.UserId,
                                 UserCode = p.UserCode,
                                 Photo = p.Photo, FirstName=p.FirstName, LastName=p.LastName
                             }).ToList();
               
              foreach(var item in result)
                {
                    item.Roles = (from a in db.UserRoles.Where(O => O.UserId == item.Id)
                                  join b in db.Roles.Select() on a.RoleId equals b.Id
                                  select new roles { Id = b.Id, Name = b.Name }).ToList();
                }

                return result.ToList();
            }
        }
        public userprofile GetProfile(string name)
        {
            using (var db = new OcphDbContext())
            {
                var u = db.Users.Where(O => O.UserName == name).FirstOrDefault();
                if (u != null)
                {
                    var profile = db.UserProfiles.Where(O => O.UserCode == u.Id).FirstOrDefault();
                    if (profile != null)
                    {
                        profile.Email = u.Email;
                        profile.PhoneNumber = u.PhoneNumber;
                        var roles = from a in db.UserRoles.Where(O => O.UserId == profile.UserCode)
                                    join b in db.Roles.Select() on a.RoleId equals b.Id
                                    select new roles { Id = b.Id, Name = b.Name };
                        profile.Roles = roles.ToList();
                    }
                    return profile;
                }
                else
                    return null;

            }
        }

        public userprofile Get(int Id,string username)
        {
            using (var db = new OcphDbContext())
            {
                var u = db.Users.Where(O => O.UserName == username).FirstOrDefault();
                if (u != null)
                {
                    var profile = db.UserProfiles.Where(O => O.UserCode == u.Id).FirstOrDefault();
                    if (profile != null)
                    {
                        profile.Email = u.Email;
                        profile.PhoneNumber = u.PhoneNumber;
                        var roles = from a in db.UserRoles.Where(O => O.UserId == profile.UserCode)
                                    join b in db.Roles.Select() on a.RoleId equals b.Id
                                    select new roles { Id = b.Id, Name = b.Name };
                        profile.Roles = roles.ToList();
                    }
                    return profile;
                }
                else
                    return null;
            }
        }

        public roles AddNewRole(string userId, string Id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    userroles userrole = new userroles { RoleId = Id, UserId = userId };
                    var role = db.Roles.Where(O => O.Id == Id).FirstOrDefault();
                    if(role!=null)
                    {
                        if (db.UserRoles.Insert(userrole))
                            return role;
                        else
                            throw new SystemException("User Role Gagal Ditambah");
                    }
                    else
                    {
                        throw new SystemException("Role Tidak Ditemukan");
                    }
                   
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }   
        }

        public roles RemoveRole(string userId, string Id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    userroles userrole = new userroles { RoleId = Id, UserId = userId };
                    var role = db.Roles.Where(O => O.Id == Id).FirstOrDefault();
                    if (role != null)
                    {
                        if (db.UserRoles.Delete(O=>O.UserId==userId && O.RoleId==Id))
                            return role;
                        else
                            throw new SystemException("User Role Gagal Dihapus");
                    }
                    else
                    {
                        throw new SystemException("Role Tidak Ditemukan");
                    }

                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }



        public Task<bool> Login(string userName, string password)
        {

            using (var db = new OcphDbContext())
            {
                var result = db.Users.Where(O => O.UserName == userName).FirstOrDefault();
                if (result != null)
                    return Task.FromResult(true);
                return Task.FromResult(false);
            }
        }

        public bool Post( userprofile t, string userName)
        {
            using (var db = new OcphDbContext())
            {
                var u = db.Users.Where(O => O.UserName == userName).FirstOrDefault();
                t.UserCode = u.Id;
                return db.UserProfiles.Insert(t);
            }
        }


        public userprofile Put(int profileid, userprofile value,string name)
        {
            using (var db = new OcphDbContext())
            {
                string userid = Helpers.GetUserId(name);

                var isUpdated= db.UserProfiles.Update(O => new { O.FirstName, O.LastName, O.Address, O.Photo }, value,
                    O => O.UserId == profileid);
                if(isUpdated)
                {
                    return value;
                }else
                {
                    throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
                }

            }
        }
        

    }
}
