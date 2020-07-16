using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class UserDAO
    {
        BookStore db = null;

        public UserDAO()
        {
            db = new BookStore();
        }

        public User GetByUsername(String Username)
        {
            return db.Users.SingleOrDefault(u => u.UserName == Username);
        }

        public Boolean Login(String UserName, String Password, bool isAdmin = false)
        {
            var result = db.Users.SingleOrDefault(u => u.UserName == UserName);
            if(result == null)
            {
                return false;
            } else
            {
                if(isAdmin == true)
                {
                    if(result.Password == Password)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    if(result.Password == Password)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
