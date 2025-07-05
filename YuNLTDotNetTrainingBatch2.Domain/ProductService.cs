using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YuNLTDotNetTrainingBatch2.Domain
{
    public class ProductService
    {
        AppDbContext _db = new AppDbContext();
        public List<TblProduct> ProductList()
        {
            var lst = _db.TblProducts.Where(x => x.DeleteFlag == false).ToList();
            return lst;
        }

        public TblProduct? FindProduct(int id)
        {
            var item = _db.TblProducts.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.ProductId == id);
            return item;
        }

        public int CreateProduct(string name, decimal price)
        {
            var newproduct = new TblProduct
            {
                ProductName = name,
                Price = price,
                Createat = DateTime.Now,
            };

            _db.TblProducts.Add(newproduct);
            var result = _db.SaveChanges();
            return result;
        }

        public int UpdateProduct(int id,string name, decimal price, DateTime createdAt)
        {
            var item = _db.TblProducts.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.ProductId == id);
            if (item is null) return -1;
            item.ProductName = name;
            item.Price = price;
            item.Createat = createdAt;
            var result = _db.SaveChanges();
            return result;
        }

        public int DeleteProduct(int id)
        {
            var item = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            if(item is null) return -1;
            item.DeleteFlag = true;
            var result = _db.SaveChanges();
            return result;
        }
    }
}
