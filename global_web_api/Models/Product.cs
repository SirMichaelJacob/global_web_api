namespace global_web_api.Models
{
    public class Product : BaseEntity<int>
    {
        public string Price { get; set; }
        public string Description { get; set; }
    }
}
