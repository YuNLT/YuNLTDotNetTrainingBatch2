using System;
using System.Collections.Generic;

namespace YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

public partial class TblPosition
{
    public int PositionId { get; set; }

    public string PositionCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? DeleteFlag { get; set; }
}
