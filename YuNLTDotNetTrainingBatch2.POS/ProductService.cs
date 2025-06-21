using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YuNLTDotNetTrainingBatch2.POS
{
    public class ProductService
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.TblProducts.Where(x => x.DeleteFlag == false).ToList();
            foreach (var item in lst)
            {
                Console.Write("ProductID => " + item.ProductId);
                Console.Write("Product Name => " + item.PName);
                Console.Write("Price => " + item.Price);
                Console.Write("Create at => " + item.Createat);
            }
        }

        public void Edit()
        {
        FirstPage:
            Console.Write("Enter ProductID: ");
            var input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }
            AppDbContext db = new AppDbContext();
            var item = db.TblProducts.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.ProductId == id);
            if (item is null) return;
            Console.WriteLine("ProductID => " + item.ProductId);
            Console.WriteLine("Product Name => " + item.PName);
            Console.WriteLine("Price => " + item.Price);
            Console.WriteLine("Create at => " + item.Createat);
        }

        public void Create()
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine()!;
        PriceInput:
            Console.Write("Enter Price: ");
            var input = Console.ReadLine()!;
            bool isDecimal = decimal.TryParse(input, out decimal price);
            if (!isDecimal)
            {
                goto PriceInput;
            }
        DateInput:
            Console.WriteLine("Enter Date(e.g., 2025-06-22 or MM/dd/yyyy):: ");
            var dateInput = Console.ReadLine()!;
            bool isDateTime = DateTime.TryParse(dateInput, out DateTime createdAt);
            if (!isDateTime)
            {
                goto DateInput;
            }
            var product = new TblProduct
            {
                PName = name,
                Price = price,
                Createat = createdAt
            };

            AppDbContext db = new AppDbContext();
            db.TblProducts.Add(product);
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Create Succeed" : "Create failed");
        }

        public void Update()
        {
        IntInput:
            Console.WriteLine("Enter ProductID : ");
            var idInput = Console.ReadLine()!;
            bool isInt = int.TryParse(idInput, out int id);
            if (!isInt) goto IntInput;
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine()!;
        PriceInput:
            Console.Write("Enter Price: ");
            var input = Console.ReadLine()!;
            bool isDecimal = decimal.TryParse(input, out decimal price);
            if (!isDecimal)
            {
                goto PriceInput;
            }
        DateInput:
            Console.WriteLine("Enter Date(e.g., 2025-06-22 or MM/dd/yyyy) : ");
            var dateInput = Console.ReadLine()!;
            bool isDateTime = DateTime.TryParse(dateInput, out DateTime createdAt);
            if (!isDateTime)
            {
                goto DateInput;
            }
            var exist = IsExist(id);
            if (!exist) return;
            AppDbContext db = new AppDbContext();
            var item = db.TblProducts.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.ProductId == id);
            if (item is null) return;
            item.PName = name;
            item.Price = price;
            item.Createat = createdAt;
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Update Success" : "Update Failed");
        }

        public void Delete()
        {
        FirstPage:
            Console.WriteLine("Enter ProductID : ");
            var input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }
            var exit = IsExist(id);
            if (!exit) return;
            AppDbContext db = new AppDbContext();
            var item = db.TblProducts.First(x => x.ProductId == id);
            item.DeleteFlag = true;
            var result = db.SaveChanges();
            Console.WriteLine(result > 0 ? "Delete Success" : "Delete Failed");
        }

        private bool IsExist(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            return item != null;
        }
    }
}
