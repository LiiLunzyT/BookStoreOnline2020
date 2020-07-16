using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class CategoryDAO
    {
        BookStore db = null;

        public CategoryDAO()
        {
            db = new BookStore();
        }

        public List<Category> listAll()
        {
            return db.Categories.ToList();
        }

        public List<Category> listByGroup(String GroupID)
        {
            return db.Categories.Where(c => c.CateGroupID == GroupID).ToList();
        }
        public Category getCategoryByUrl(String Url)
        {
            return db.Categories.FirstOrDefault(c => c.Url == Url) ?? new Category();
        }

        public String Add(Category enity)
        {
            db.Categories.Add(enity);
            db.SaveChanges();
            return enity.CategoryID;
        }
    }

    public class CategoryGroupDAO
    {
        BookStore db = null;

        public CategoryGroupDAO()
        {
            db = new BookStore();
        }

        public List<CategoryGroup> listAll()
        {
            return db.CategoryGroups.ToList();
        }

        public String Add(CategoryGroup enity)
        {
            db.CategoryGroups.Add(enity);
            db.SaveChanges();
            return enity.GroupID;
        }
    }
}
