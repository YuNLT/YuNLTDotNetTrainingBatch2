using System.Threading.Tasks;
using YuNLTDotNetTrainingBatch2.Database;
using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YuNLTDotNetTrainingBatch2.Domain
{
    public class StaffRegistrationService
    {
        AppDbContext _db = new AppDbContext();
        public TblStaffRegistration? FindStaff(int id)
        {
            var staff = _db.TblStaffRegistrations.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.StaffId == id);
            return staff;
        }

        public TblStaffRegistration? FindStaffByStaffCode(string staffCode)
        {
            var staff = _db.TblStaffRegistrations.Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.StaffCode == staffCode);
            return staff;
        }

        public List<TblStaffRegistration> StaffList()
        {
            var lst = _db.TblStaffRegistrations.Where(x=> x.DeleteFlag != false).ToList();
            return lst;
        }

        public async Task<int> RegisterStaff(TblStaffRegistration staff)
        {
            var staffRegistration = new TblStaffRegistration
            {
                StaffCode = await _db.GetNextCodeAsync("StaffCodeSeq", "STF"),
                StaffName = staff.StaffName,
                Nrc = staff.Nrc,
                Education = staff.Education,
                FatherName = staff.FatherName,
                MotherName = staff.MotherName,
                MaritalStatus = staff.MaritalStatus,
                SpouseName = staff.SpouseName,
                Address = staff.Address,
                PhoneNo = staff.PhoneNo,
                Email = staff.Email,
                Status = staff.Status,
                CreatedAt = staff.CreatedAt,
                CreatedUserCode = staff.CreatedUserCode,
                DeleteFlag = false,
            };

            _db.TblStaffRegistrations.Add(staffRegistration);
            var result = _db.SaveChanges();
            return result;
        }

        public int UpdateRegisterSatff(TblStaffRegistration staff)
        {
            var existingStaff = _db.TblStaffRegistrations.FirstOrDefault(x => x.StaffId == staff.StaffId);
            if(existingStaff is null)
            {
                return -1;
            }
            existingStaff.StaffName = staff.StaffName;
            existingStaff.Nrc = staff.Nrc;
            existingStaff.Education = staff.Education;
            existingStaff.FatherName = staff.FatherName;
            existingStaff.MotherName = staff.MotherName;
            existingStaff.MaritalStatus = staff.MaritalStatus;
            existingStaff.SpouseName = staff.SpouseName;
            existingStaff.Address = staff.Address;
            existingStaff.PhoneNo = staff.PhoneNo;
            existingStaff.Email = staff.Email;
            existingStaff.Status = staff.Status;
            existingStaff.ModifiedAt = staff.ModifiedAt;
            existingStaff.ModeifiedUserCode = staff.ModeifiedUserCode;
            var result = _db.SaveChanges();
            return result;
        }

        public int DeleteRegisterStaff(int id)
        {
            var existingStaff = _db.TblStaffRegistrations.FirstOrDefault(x => x.StaffId==id);
            if(existingStaff == null) { return -1; }
            existingStaff.DeleteFlag = true;
            var result = _db.SaveChanges();
            return result;
        }
    }
}
