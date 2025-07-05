using Microsoft.EntityFrameworkCore;
using YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YuNLTDotNetTrainingBatch2.Database
{
    public static class DevCode
    {
        public static async Task<string> GetNextCodeAsync(this DbContext context, string sequenceName, string prefix)
        {
            var sql = $"SELECT NEXT VALUE FOR {sequenceName} AS Number";

            var result = await context
                .Set<TempSequenceResult>()
                .FromSqlRaw(sql)
                .Select(x => x.Number)
                .FirstAsync();

            return $"{prefix}-{result.ToString("D4")}";
        }

        private class TempSequenceResult
        {
            public int Number { get; set; }
        }
    }
}
