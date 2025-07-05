using System;
using System.Collections.Generic;

namespace YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime? Createat { get; set; }

    public bool DeleteFlag { get; set; }
}
