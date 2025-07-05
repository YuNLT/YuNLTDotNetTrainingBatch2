using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YuNLTDotNetTrainingBatch2.Domain
{
    public class SaleService
    {
        AppDbContext _db = new AppDbContext();
        public TblProduct? FindProduct(int id)
        {
            var product = _db.TblProducts.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.ProductId == id);
            return product;
        }

        public int Sale(List<TblSaleDetail> list)
        {

            #region Generate Sale 

            var sale = new TblSale
            {
                SaleDate = DateTime.Now,
                DeleteFlag = false,
                VoucherNo = Guid.NewGuid().ToString(),
                TotalAmount = list.Sum(x => x.Quantity * x.Price),
            };
            _db.TblSales.Add(sale);
            _db.SaveChanges();

            #endregion

            #region Create Sale Detail

            foreach (var item in list)
            {
                item.SaleId = sale.SaleId;
            }
            _db.TblSaleDetails.AddRange(list);
            var result = _db.SaveChanges();
            return result;
            #endregion
        }

        public List<TblSale> SaleList()
        {
            var lst = _db.TblSales.Where(x => x.DeleteFlag == false).ToList();
            return lst;
        }

        public List<TblSaleDetail> SaleDetailList()
        {
            var lst = _db.TblSaleDetails.Where(x => x.DeleteFlag == false).ToList();
            return lst;
        }

        public List<TblSaleDetail> SaleDetailListBySaleId(int id)
        {
            var lst = _db.TblSaleDetails.Where(x =>x.SaleId == id).ToList();
            return lst;
        }
    }
}
