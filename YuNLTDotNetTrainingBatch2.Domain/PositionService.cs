using System.Text.RegularExpressions;
using YuNLTDotNetTrainingBatch2.Database;
using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YuNLTDotNetTrainingBatch2.Domain
{
    public class PositionService
    {
        AppDbContext _db = new AppDbContext();

        public TblPosition? FindPosition(int id)
        {
            var position = _db.TblPositions.FirstOrDefault(x => x.PositionId == id);
            return position;
        }

        public TblPosition? FindPositionByPositionCode(string positionCode)
        {
            var position = _db.TblPositions.FirstOrDefault(x => x.PositionCode == positionCode);
            return position;
        }

        public List<TblPosition> PositionList()
        {
            var positionList = _db.TblPositions.Where(x => x.DeleteFlag == false).ToList();
            return positionList;
        }

        public async Task<int> CreatePosition(TblPosition position)
        {
            var newPosition = new TblPosition
            {
                PositionCode = await _db.GetNextCodeAsync("PositionCodeSeq", "P"),
                Description = position.Description,
                CreatedAt = DateTime.Now,
                DeleteFlag = false
            };

            await _db.TblPositions.AddAsync(newPosition);
            var result = await _db.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdatePosition(TblPosition position)
        {
            var existingPosition = _db.TblPositions.
                Where(x => x.DeleteFlag == false)
                .FirstOrDefault(x => x.PositionId == position.PositionId);
            if (existingPosition is null)
            {
                return -1;
            }

            existingPosition.Description = position.Description;
            var result = await _db.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeletePosition(int id)
        {
            var existingPosition = _db.TblPositions
                .Where(x => x.DeleteFlag == false)
                .FirstOrDefault(x => x.PositionId == id);
            if (existingPosition is null)
            {
                return -1;
            }

            existingPosition.DeleteFlag = true;
            var result = await _db.SaveChangesAsync();  
            return result;
        }
    }
}
