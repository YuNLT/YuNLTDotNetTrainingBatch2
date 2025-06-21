using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace YuNLTDotNetTrainingBatch2.ConsoleApp
{
    public class EfCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs
                .Where(x => x.DeleteFlag == false)
                .ToList();
            foreach (var item in lst)
            {
                Console.WriteLine("BlogId => " + item.BlogId);
                Console.WriteLine("BlogTitle => " + item.BlogTitle);
                Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
                Console.WriteLine("BlogContent => " + item.BlogContent);
            }
        }

        public void Edit()
        {
        FirstPage:
            Console.WriteLine("Enter BlogId : ");
            var input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.BlogId == id); //top 2. give first one 
            //SingleOrDefault => only one. error if the result is more than one
            if(item is null)
            {
                return;
            }
            Console.WriteLine("BlogId => " + item.BlogId);
            Console.WriteLine("BlogTitle => " + item.BlogTitle);
            Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
            Console.WriteLine("BlogContent => " + item.BlogContent);
        }

        public void Create()
        {
            Console.WriteLine("Enter BlogTitle: ");
            string title = Console.ReadLine()!;
            Console.WriteLine("Enter BlogAuthor: ");
            string author = Console.ReadLine()!;
            Console.WriteLine("Enter BlogContent: ");
            string content = Console.ReadLine()!;
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            int result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Create Successfully" : "Create Failed");
        }

        public void Update()
        {
        FirstPage:
            Console.WriteLine("Enter BlogId : ");
            var input = Console.ReadLine();
            Console.WriteLine("Enter BlogTitle: ");
            string title = Console.ReadLine()!;
            Console.WriteLine("Enter BlogAuthor: ");
            string author = Console.ReadLine()!;
            Console.WriteLine("Enter BlogContent: ");
            string content = Console.ReadLine()!;
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }

            var exit = IsExist(id);
            if (!exit) return;

            AppDbContext db = new AppDbContext();
            var item = db.Blogs.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.BlogId == id);
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Update Success" : "Update Failed");
        }

        public void Delete()
        {
        FirstPage:
            Console.WriteLine("Enter BlogId : ");
            var input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }
            var exit = IsExist(id);
            if (!exit) return;
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.First(x => x.BlogId == id);
            //db.Remove(item); // delete with entity only
            item.DeleteFlag = true;
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Delete Success" : "Delete Failed");
        }

        public void InsertDeletedData()
        {
            Console.WriteLine("Enter BlogId : ");
            string? idInput = Console.ReadLine();
            int blogId = 0;
            bool hasId = int.TryParse(idInput, out blogId);
            Console.WriteLine("Enter BlogTitle: ");
            string title = Console.ReadLine()!;
            Console.WriteLine("Enter BlogAuthor: ");
            string author = Console.ReadLine()!;
            Console.WriteLine("Enter BlogContent: ");
            string content = Console.ReadLine()!;
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
                DeleteFlag = false
            };
            if (hasId)
            {
                blog.BlogId = blogId;
            }
            AppDbContext db = new AppDbContext();
            if (hasId)
            {
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Tbl_Blog ON");
                db.Blogs.Add(blog);
                db.SaveChanges();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Tbl_Blog OFF");
            }
            else
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
            }
        }

        private bool IsExist(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.BlogId == id);
            return item != null;
        }
    }
}
