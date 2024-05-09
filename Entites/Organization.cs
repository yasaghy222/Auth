namespace Authenticate.Entities
{
    public class Organization
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}