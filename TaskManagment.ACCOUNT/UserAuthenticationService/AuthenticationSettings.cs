namespace TaskManagment.SECURITY.UserAuthenticationService
{
    public class AuthenticationSettings
    {
        public const string SectionName = "Jwt";
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int LifeTime { get; set; } = 0;
    }
}