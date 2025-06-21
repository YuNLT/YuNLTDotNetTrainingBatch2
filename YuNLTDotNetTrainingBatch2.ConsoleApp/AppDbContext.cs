using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YuNLTDotNetTrainingBatch2.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = "LAPTOP-NFU692OK\\LOCALDB",
                    InitialCatalog = "YuNLTDotNetTrainingBatch2",
                    UserID = "sa",
                    Password = "sasa@123",
                    TrustServerCertificate = true
                };
                optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
            }
        }

       public DbSet<BlogModel> Blogs { get; set; }
    }
    //Model First <=> Database Column Mapping
    [Table("Tbl_Blog")]
    public class BlogModel
    {
        [Key]
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }

        public bool DeleteFlag { get; set; }
    }
}
