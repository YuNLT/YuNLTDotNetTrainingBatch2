using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;
using YuNLTDotNetTrainingBatch2.Domain;

namespace YuNLTDotNetTrainingBatch2.POS
{
    public class SaleUI
    {
        SaleService _saleService = new SaleService();
        public void CreateSale()
        {
            List<TblSaleDetail> list = new List<TblSaleDetail>();
            
        SaleFirstPage:

            #region Prepare Product

            var id = ReadInt("Please Enter ProductId: ");
            var product = _saleService.FindProduct(id);
            if (product is null)
            {
                Console.WriteLine($"Product with this {id} does not exist.");
                goto SaleFirstPage;
            }

            Console.WriteLine("Product Name => " + product.ProductName);
            Console.WriteLine("Price => " + product.Price);
            var quantity = ReadInt("Please Enter Quantity: ");
            list.Add(new TblSaleDetail
            {
                ProductId = product.ProductId,
                Price = product.Price,
                Quantity = quantity,
            });

        #endregion

            #region Add more product or Continue
            Console.WriteLine("Are you sure want to add more? Y/N");
            var choice = Console.ReadLine();
            if(choice == "Y")
            {
                goto SaleFirstPage;
            }

            #endregion

            #region Sale Process

            var result = _saleService.Sale(list);

            Console.WriteLine(result > 0 ? "Slae Create Succeed" : "Sale Create failed");
            #endregion
        }

        public int ReadInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()!;
                if (int.TryParse(input, out value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        public void SaleList()
        {
            var lst = _saleService.SaleList();
            if (lst is null)
            {
                Console.WriteLine("There is no Sale Yet");
                return;
            }
            foreach (var item in lst)
            {
                Console.WriteLine("SaleID => " + item.SaleId);
                Console.WriteLine("Voucher Number => " + item.VoucherNo);
                Console.WriteLine("Total Amount => " + item.TotalAmount);
                Console.WriteLine("Slae Date => " + item.SaleDate);
            }
        }

        public void SaleDetailList()
        {
            var lst = _saleService.SaleDetailList();
            if(lst is null)
            {
                Console.WriteLine("There is no SaleDetail Yet");
                return;
            }
            foreach (var item in lst)
            {
                Console.WriteLine("SaleDetailID => " + item.SaleDetailId);
                Console.WriteLine("ProductID => " + item.ProductId);
                Console.WriteLine("SaleID => " + item.SaleId);
                Console.WriteLine("Quantity => " + item.Quantity);
                Console.WriteLine("Price => " + item.Price);
            }
        }

        public void SaleDetailListBySale()
        {
        Firstpage:
            Console.WriteLine("Please Enter SaleId: ");
            var input = Console.ReadLine()!;
            bool isInt = int.TryParse(input, out var id);
            if (!isInt)
            {
                Console.WriteLine("Invalid saleDetail Id");
                goto Firstpage;
            }
            var lst = _saleService.SaleDetailListBySaleId(id);
            if (lst is null)
            {
                Console.WriteLine("There is no SaleDetail with this {id}");
                goto Firstpage;
            }
            foreach (var item in lst)
            {
                Console.WriteLine("SaleDetailID => " + item.SaleDetailId);
                Console.WriteLine("ProductID => " + item.ProductId);
                Console.WriteLine("SaleID => " + item.SaleId);
                Console.WriteLine("Quantity => " + item.Quantity);
                Console.WriteLine("Price => " + item.Price);
            }
        }

        public void Execute()
        {
        Result:
            Console.WriteLine("Sale Menu");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("1.New sale");
            Console.WriteLine("2.Sale List");
            Console.WriteLine("3.SaleDetail List");
            Console.WriteLine("4.SaleDetail List By Sale");
            Console.WriteLine("5.Exit");
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine("Choose Menu");
            var input = Console.ReadLine()!;
            bool isInt = int.TryParse(input, out int no);
            if (!isInt)
            {
                Console.WriteLine("Invalid Product Menu. Please choose 1 to 4");
                goto Result;
            }
            Enumsale menu = (Enumsale)no;
            switch (menu)
            {
                case Enumsale.NewSale:
                    CreateSale();
                    break;
                case Enumsale.SaleList:
                    SaleList();
                    break;
                case Enumsale.SaleDetailList:
                    SaleDetailList();
                    break;
                case Enumsale.SaleDetailListBySale:
                    SaleDetailListBySale();
                    break;
                case Enumsale.Exit:
                    goto End;
                case Enumsale.None:
                default:
                    Console.WriteLine("Invalid Sale Menu. Please Choose 1 to 5");
                    goto Result;
            }
            Console.WriteLine("------------------------------------------------");
            goto Result;
        End:
            Console.WriteLine("Exiting Product Menu");
        }
    }

    public enum Enumsale
    {
        None = 0,
        NewSale,
        SaleList,
        SaleDetailList,
        SaleDetailListBySale,
        Exit
    }
}
