using Microsoft.Data.SqlClient;
using System.Data;
namespace YuNLTDotNetTrainingBatch2.ConsoleApp
{
    public class AdoDotNetExample
    {
        //Ctrl + R , R
        SqlConnectionStringBuilder _sqlConnectionString = new SqlConnectionStringBuilder
        {
            DataSource = "LAPTOP-NFU692OK\\LOCALDB",
            InitialCatalog = "YuNLTDotNetTrainingBatch2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };

        public void Read()
        {

            
            SqlConnection connection = new SqlConnection(_sqlConnectionString.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Tbl_Student";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();

            //DataSet
            //DataTable
            //DataRow
            //DataColumn

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                Console.WriteLine(i);
                Console.WriteLine(row["Id"]);
                Console.WriteLine(row["Student_ID"]);
            }

        }

        //read => data read
        //create => data insert
        //update => data update
        //delete => data delete

        public void Create()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine()!;

            Console.Write("Enter Author: ");
            string author = Console.ReadLine()!;

            Console.Write("Enter Content: ");
            string content = Console.ReadLine()!;

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('@Title'
           ,'@Author'
           ,'@Content')";

            SqlConnection connection = new SqlConnection(_sqlConnectionString.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            int result = command.ExecuteNonQuery();
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Author", author);
            command.Parameters.AddWithValue("@Content", content);
            connection.Close();
            Console.WriteLine(result > 0 ? "Insert Succeed!" : "Insert Failed");
        }

        public void Delete ()
        {
            Console.Write("Enter BlogId: ");
            string blogId = Console.ReadLine()!;

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE @BlogId ";

            SqlConnection connection = new SqlConnection(_sqlConnectionString.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", blogId);
            int result = command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result > 0 ? "Insert Succeed!" : "Insert Failed");
        }

        public void Update()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine()!;

            Console.Write("Enter Author: ");
            string author = Console.ReadLine()!;

            Console.Write("Enter Content: ");
            string content = Console.ReadLine()!;

            Console.Write("Enter BlogId: ");
            string blogId = Console.ReadLine()!;

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET
      ,[BlogTitle] = '@Title'
      ,[BlogAuthor] = '@Author'
      ,[BlogContent] = '@Content'
 WHERE [BlogId] = @BlogId";

            SqlConnection connection = new SqlConnection(_sqlConnectionString.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Author", author);
            command.Parameters.AddWithValue("@Content", content);
            command.Parameters.AddWithValue("@BlogId", blogId);
            int result = command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result > 0 ? "Update Succeed!" : "Update Failed");
        }
    }
}
