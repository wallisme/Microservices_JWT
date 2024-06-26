namespace JWTService.Domain.Entities
{
    public class ApplicationUser
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

    }
}
