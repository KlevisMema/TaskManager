using Microsoft.AspNetCore.Identity;

namespace TaskManager.DAL.Models.BaseModels
{
    public abstract class BaseIdentity : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}