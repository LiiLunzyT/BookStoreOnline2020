using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class BookDAO
    {
        BookStore db = null;

        public BookDAO()
        {
            db = new BookStore();
        }

        public List<Book> listAll()
        {
            return db.Books.OrderBy(b => b.CreateByDate).ToList();
        }

        public Book getBookByID(String BookID)
        {
            return db.Books.Find(BookID);
        }
        
        public String Add(Book enity)
        {
            db.Books.Add(enity);
            db.SaveChanges();
            return enity.BookID;
        }

        public Book getBookByUrl(String Url)
        {
            return db.Books.Include(c => c.Author).Include(c => c.Producer).FirstOrDefault(c => c.Url == Url);
        }
    }
}
