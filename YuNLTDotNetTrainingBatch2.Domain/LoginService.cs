using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YuNLTDotNetTrainingBatch2.Domain
{
    public class LoginService
    {
        AppDbContext _db = new AppDbContext();

        public int SignIn(string staffCode, string positionCode, string phoneNo, string email, string password)
        {
            var signIn = new TblStaffUser
            {
                StaffCode = staffCode,
                PositionCode = positionCode,
                PhoneNo = phoneNo,
                Email = email,
                Password = password
            };

            _db.Add(signIn);
            var result = _db.SaveChanges();
            return result;
        }

        public int Login(string email, string password)
        {
            var existingStaffuser = _db.TblStaffUsers.FirstOrDefault(x => x.Email == email);
            if(existingStaffuser is null)
            {
                return -1;
            }

            if(existingStaffuser.Password != password)
            {
                return -1;
            }

            return 1;
        }
    }
}
