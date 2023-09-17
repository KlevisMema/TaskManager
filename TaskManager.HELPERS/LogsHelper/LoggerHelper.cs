/*
    This helper class provides methods for logging exceptions and actions into the database using the application's database context.

    - LogException: Logs an exception with its message, stack trace, and occurrence timestamp.
    - LogAction: Logs a custom action with a message and occurrence timestamp.
*/

using TaskManager.DAL.Context;

namespace TaskManager.HELPERS.LogsHelper
{
    /// <summary>
    /// Helper class for logging exceptions and actions.
    /// </summary>
    public static class LoggerHelper
    {
        /// <summary>
        /// Logs an exception into the database.
        /// </summary>
        /// <param name="ex">The exception to be logged.</param>
        /// <param name="dbContext">The application's database context.</param>
        public static async Task
        LogException
        (
            Exception ex,
            ApplicationDbContext dbContext
        )
        {
            try
            {
                using (ApplicationDbContext db = new())
                {
                    var exceptionLog = new DAL.Models.Logger
                    {
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        OccurredAt = DateTime.UtcNow
                    };

                    db.Logs.Add(exceptionLog);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex2)
            {
                LogExceptionInFile(ex2, "LogException");
            }
        }

        /// <summary>
        /// Logs an action into the database.
        /// </summary>
        /// <param name="action">The action to be logged.</param>
        /// <param name="dbContext">The application's database context.</param>
        public static async Task
        LogAction
        (
            string action,
            ApplicationDbContext dbContext
        )
        {
            try
            {
                var exceptionLog = new DAL.Models.Logger
                {
                    Message = action,
                    StackTrace = null,
                    OccurredAt = DateTime.UtcNow
                };

                dbContext.Logs.Add(exceptionLog);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                LogExceptionInFile(ex, "LogAction");
            }
        }

        /// <summary>
        ///     Save the exeption in a file if something 
        ///     went wrong creating when logs.
        /// </summary>
        /// <param name="ex"> The exeption object of type <see cref="Exception"/> </param>
        /// <param name="method"> Method value of type <see cref="string"/> </param>
        /// <returns> Nothing </returns>
        private static void
        LogExceptionInFile
        (
            Exception ex,
            string method
        )
        {
            string projectDirectory = System.IO.Path.GetDirectoryName(typeof(LoggerHelper).Assembly.Location)!;
            string logDirectory = Path.Combine(projectDirectory, "Logs");

            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            string exceptionsDirectory = Path.Combine(logDirectory, "Exceptions");

            if (!Directory.Exists(exceptionsDirectory))
                Directory.CreateDirectory(exceptionsDirectory);

            string logFileName = $"exception_{DateTime.Now:yyyyMMdd}.log";
            string logFilePath = Path.Combine(exceptionsDirectory, logFileName);

            if (File.Exists(logFilePath))
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]  ==> Method {method}");
                    sw.WriteLine($"Exception type: {ex.GetType().FullName}.. ==> Exception source: {ex.Source} ==> Exception message: {ex.Message}");
                    sw.WriteLine($"Stack trace: {ex.StackTrace}");

                    Exception innerException = ex.InnerException!;
                    int innerExceptionCount = 1;

                    while (innerException != null)
                    {
                        sw.WriteLine($"Inner exception {innerExceptionCount++}: ==> Exception type: {innerException.GetType().FullName} ==> Exception source: {innerException.Source} ==> Exception message: {innerException.Message}");
                        sw.WriteLine($"Stack trace: {innerException.StackTrace} \n");

                        innerException = innerException.InnerException!;
                    }

                    sw.WriteLine("\n");
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(logFilePath))
                {
                    sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]  ==> Method {method}");
                    sw.WriteLine($"Exception type: {ex.GetType().FullName}.. ==> Exception source: {ex.Source} ==> Exception message: {ex.Message}");
                    sw.WriteLine($"Stack trace: {ex.StackTrace}");

                    Exception innerException = ex.InnerException!;
                    int innerExceptionCount = 1;

                    while (innerException != null)
                    {
                        sw.WriteLine($"Inner exception {innerExceptionCount++}: ==> Exception type: {innerException.GetType().FullName} ==> Exception source: {innerException.Source} ==> Exception message: {innerException.Message}");
                        sw.WriteLine($"Stack trace: {innerException.StackTrace} \n");

                        innerException = innerException.InnerException!;
                    }

                    sw.WriteLine("\n");
                }
            }
        }
    }
}