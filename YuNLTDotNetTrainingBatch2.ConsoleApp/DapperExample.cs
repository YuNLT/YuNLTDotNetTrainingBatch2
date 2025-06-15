using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace YuNLTDotNetTrainingBatch2.ConsoleApp
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder= new SqlConnectionStringBuilder
        {
                DataSource = "LAPTOP-NFU692OK\\LOCALDB",
                InitialCatalog = "YuNLTDotNetTrainingBatch2",
                UserID = "sa",
                Password = "sasa@123",
                TrustServerCertificate = true
        };

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                var query = db.Query<BlogDto>("SELECT * FROM Tbl_Blog").ToList();
                foreach(var item in query)
                {
                    Console.WriteLine("BlogId => " + item.BlogId);
                    Console.WriteLine("BlogTitle => " + item.BlogTitle);
                    Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
                    Console.WriteLine("BlogContent => " + item.BlogContent);
                }
            }
        }

        public void Create()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine()!;

            Console.Write("Enter Author: ");
            string author = Console.ReadLine()!;

            Console.Write("Enter Content: ");
            string content = Console.ReadLine()!;

            BlogDto blog = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogId]
           ,[BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                int result = db.Execute(query, blog);
                Console.WriteLine(result > 0 ? "Saving Successful" : "Saving Failed");
            }
        }

        public void Update()
        {
        FirstPage:
            Console.Write("Enter BlogId: ");
            string input = Console.ReadLine()!;
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                Console.WriteLine("Invalid Id. Please enter a valid integer.");
                goto FirstPage;
            }
            Console.Write("Modify Title: ");
            string title = Console.ReadLine()!;

            Console.Write("Modify Author: ");
            string author = Console.ReadLine()!;

            Console.Write("Modify Content: ");
            string content = Console.ReadLine()!;

            BlogDto blog = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string query = @"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                    ,[BlogAuthor] = @BlogAuthor
                    ,[BlogContent] = @BlogContent
                    WHERE BlogId = @BlogId";
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                int result = db.Execute(query, blog);
                Console.WriteLine(result > 0 ? "Updating Successful" : "Updating Failed");
            }
        }

        public void Delete()
        {
        FirstPage:
            Console.Write("Enter BlogId: ");
            string input = Console.ReadLine()!;
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                Console.WriteLine("Invalid Id. Please enter a valid integer.");
                goto FirstPage;
            }
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                var item = db.QueryFirstOrDefault<BlogDto>("Select * FROM Blogs WHERE BlogId = @BlogId", new BlogDto { BlogId = id });
                if (item == null)
                {
                    Console.WriteLine("Blog not found with Id" + id);
                    return;
                }

                string query = @"DELETE FROM [dbo].[Tbl_Blog]
                        WHERE BlogId = @BlogId";
                var result = db.Execute(query, new { BlogId = id });
                Console.WriteLine(result > 0 ? "Deleting Successful" : "Deleting failed");
            }
        }

        public void Edit() // Search
        {
        FirstPage:
            Console.Write("Enter Id: ");
            //int id = Convert.ToInt32(Console.ReadLine());
            string input = Console.ReadLine()!;
            bool isInt = int.TryParse(input, out int id);
            if(!isInt)
            {
                Console.WriteLine("Invalid Id. Please enter a valid integer.");
                goto FirstPage;
            }
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                var item = db.QueryFirstOrDefault<BlogDto>("SELECT * FROM Blogs WHERE BlogId = @BlogId", new BlogDto { BlogId = id});
                if(item == null)
                {
                    Console.WriteLine("Blog not found with Id" + id);
                    return;
                }
                Console.WriteLine("BlogId => " + item.BlogId);
                Console.WriteLine("Title => " + item.BlogTitle);
                Console.WriteLine("Author => " + item.BlogAuthor);
                Console.WriteLine("Content => " + item.BlogContent);
            }
        }
    }
}
