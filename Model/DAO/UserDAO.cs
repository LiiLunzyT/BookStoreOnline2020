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
            return db.Users.FirstOrDefault(u => u.UserName == Username);
        }

        public String getNewID()
        {
            var countOfRows = db.Users.Count();
            if (countOfRows == 0) return "US-001";
            var lastRow = db.Users.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.UserID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "US-" + str.PadLeft(3, '0');
        }
        public void Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChangesAsync();
        }

        public Boolean Login(String UserName, String Password, bool isUser = false)
        {
            var result = db.Users.SingleOrDefault(u => u.UserName == UserName);
            if(result == null)
            {
                return false;
            } else
            {
                if(isUser == true)
                {
                    if(result.Password == Password && result.Role.RoleName != "Khách hàng")
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    if(result.Password == Password && result.Role.RoleName == "Khách hàng")
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
