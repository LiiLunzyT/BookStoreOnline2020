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

        public List<Book> listByName(String name)
        {
            return db.Books.Where(b => b.BookName.Contains(name)).ToList();
        }

        public List<String> ListName(String keyword)
        {
            return db.Books.Where(x => x.BookName.Contains(keyword)).Select(x => x.BookName).ToList();
        }

        public List<Book> listNewBook(int c)
        {
            return db.Books.OrderBy(b => b.CreateByDate).Take(c).ToList();
        }

        public List<Book> listBestSeller(int c)
        {
            return db.Books.OrderBy(b => b.TotalSell).Take(c).ToList();
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

        public List<Book> ForKid()
        {
            return db.Readers.Find("RD-001").Books.OrderBy(b => b.TotalSell).Take(5).ToList();
        }

        public Book getBookByUrl(String Url)
        {
            return db.Books.Include(c => c.Author).Include(c => c.Producer).FirstOrDefault(c => c.Url == Url);
        }
    }
}
