namespace TaskManager.DAL.Models
{
    public class Logger
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public DateTime OccurredAt { get; set; }
    }
}