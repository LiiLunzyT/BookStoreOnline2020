using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDAO
    {
        BookStore db = new BookStore();

        public bool NewOrder(Order nOrder, List<OrderDetail> listDetail)
        {
            db.Orders.Add(nOrder);
            db.Entry(nOrder).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

            foreach (var detail in listDetail)
            {
                db.OrderDetails.Add(detail);
                db.Entry(detail).State = System.Data.Entity.EntityState.Added;
            }
            db.SaveChanges();

            return true;
        }

        public String getNewID()
        {
            var countOfRows = db.Orders.Count();
            if (countOfRows == 0) return "OD-000001";
            var lastRow = db.Orders.OrderBy(c => 1 == 1).Skip(countOfRows - 1).FirstOrDefault();
            String lastID = lastRow.OrderID;
            int id = int.Parse(lastID.Split('-')[1]);
            String str = "" + (id + 1);

            return "OD-" + str.PadLeft(6, '0');
        }
    }
}
