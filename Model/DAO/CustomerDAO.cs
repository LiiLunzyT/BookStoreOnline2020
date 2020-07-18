using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CustomerDAO
    {
        BookStore db;

        public CustomerDAO()
        {
            db = new BookStore();
        }
        public String getNewID()
        {
            var countOfRows = db.Customers.Count();
            if (countOfRows == 0) return "CS-001";
            var lastRow = db.Customers.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.CustomerID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "CS-" + str.PadLeft(3, '0');
        }

        public void Insert(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChangesAsync();
        }

        public Customer GetCustomerFromUserID(String UserID)
        {
            return db.Customers.FirstOrDefault(c => c.UserID.Equals(UserID));
        }
    }
}
