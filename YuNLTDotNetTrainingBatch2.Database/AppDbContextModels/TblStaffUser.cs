using System;
using System.Collections.Generic;

namespace YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

public partial class TblStaffUser
{
    public int StaffUserId { get; set; }

    public string StaffCode { get; set; } = null!;

    public string PositionCode { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreattedAt { get; set; }

    public bool? DeleteFlag { get; set; }
}
