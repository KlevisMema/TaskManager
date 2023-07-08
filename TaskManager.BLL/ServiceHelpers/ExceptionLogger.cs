using TaskManager.DAL.Models;
using TaskManager.DAL.Context;

namespace TaskManager.BLL.ServiceHelpers
{
    public static class ExceptionLogger
    {
        public static async System.Threading.Tasks.Task
        LogException
        (
            Exception ex,
            ApplicationDbContext dbContext
        )
        {
            var exceptionLog = new ExceptionLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                OccurredAt = DateTime.UtcNow
            };

            dbContext.Exceptions.Add(exceptionLog);
            await dbContext.SaveChangesAsync();
        }
    }
}