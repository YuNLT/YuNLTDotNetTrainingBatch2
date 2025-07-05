using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;
using YuNLTDotNetTrainingBatch2.Domain;

namespace YuNLTDotNetTrainingBatch2.POS
{
    public class ProductUI
    {
        ProductService _productService = new ProductService();
        public void ProductLists()
        {
            var lst = _productService.ProductList();
            foreach (var item in lst)
            {
                Console.WriteLine("ProductID => " + item.ProductId);
                Console.WriteLine("Product Name => " + item.ProductName);
                Console.WriteLine("Price => " + item.Price);
                Console.WriteLine("Create at => " + item.Createat);
            }
        }

        public void SearchProduct()
        {
        FirstPage:
            Console.Write("Enter ProductID: ");
            var input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int id);
            if (!isInt)
            {
                goto FirstPage;
            }
            
            var item = _productService.FindProduct(id);
            if (item is null)
            {
                Console.WriteLine($"Product with this {id} does not exist!");
                goto FirstPage;
            }
            Console.WriteLine("ProductID => " + item.ProductId);
            Console.WriteLine("Product Name => " + item.ProductName);
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
            var result = _productService.CreateProduct(name, price);
            Console.WriteLine(result > 0 ? "Create Succeed" : "Create failed");
        }

        public void Update()
        {
        ProductIdInput:
            Console.WriteLine("Enter ProductID : ");
            var idInput = Console.ReadLine()!;
            bool isInt = int.TryParse(idInput, out int id);
            if (!isInt) goto ProductIdInput;
            var product = _productService.FindProduct(id);
            if(product == null)
            {
                Console.WriteLine($"Product with this {id} does not exist");
                goto ProductIdInput;
            }

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
            var result = _productService.UpdateProduct(id, name, price, createdAt);
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
            var result = _productService.DeleteProduct(id);
            Console.WriteLine(result > 0 ? "Delete Success" : "Delete Failed");
        }

        public void Execute()
        {
        Result:
            Console.WriteLine("Product Menu");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("1.new Producr");
            Console.WriteLine("2.Producr List");
            Console.WriteLine("3.Update Producr");
            Console.WriteLine("4.Delete Producr");
            Console.WriteLine("5.Exit");
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine("Choose Menu");
            var input = Console.ReadLine()!;
            bool isInt = int.TryParse(input, out int no);
            if (!isInt)
            {
                Console.WriteLine("Invalid Product Menu. Please choose 1 to 5");
                goto Result;
            }
            EnumProductMenue menu = (EnumProductMenue)no;
            switch (menu)
            {
                case EnumProductMenue.NewProduct:
                    Create();
                    break;
                case EnumProductMenue.ProductList:
                    ProductLists();
                    break;
                case EnumProductMenue.UpdateProduct:
                    Update();
                    break;
                case EnumProductMenue.DeleteProduct:
                    Delete();
                    break;
                case EnumProductMenue.Exit:
                    goto End;
                case EnumProductMenue.None:
                default:
                    Console.WriteLine("Invalid product Menu. Please Choose 1 to 5");
                    goto Result;
            }
            Console.WriteLine("------------------------------------------------");
            goto Result;
        End:
            Console.WriteLine("Exiting Product Menu");
        }
    }

    public enum EnumProductMenue
    {
        None = 0,
        NewProduct,
        ProductList,
        UpdateProduct,
        DeleteProduct,
        Exit
    }

    public enum EnumPosMenue
    {
        None = 0,
        Product,
        Sale,
        Exit
    }
}
