namespace TaskManager.DAL.DTO_s.User
{
    public class UserDto
    {
        public string? Id { get; set; }
        public Guid ProjectId { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
    }
}
