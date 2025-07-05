using System;
using System.Collections.Generic;

namespace YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

public partial class TblStaffRegistration
{
    public int StaffId { get; set; }

    public string StaffCode { get; set; } = null!;

    public string PositionCode { get; set; } = null!;

    public string StaffName { get; set; } = null!;

    public string Nrc { get; set; } = null!;

    public string Education { get; set; } = null!;

    public string FatherName { get; set; } = null!;

    public string MotherName { get; set; } = null!;

    public string MaritalStatus { get; set; } = null!;

    public string SpouseName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedUserCode { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? ModeifiedUserCode { get; set; }

    public bool? DeleteFlag { get; set; }
}
