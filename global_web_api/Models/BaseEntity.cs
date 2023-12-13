namespace global_web_api.Models
{
    public class BaseEntity<IDtype>
    {
        public IDtype Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}
