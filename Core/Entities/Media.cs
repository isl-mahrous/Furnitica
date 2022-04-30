namespace Core.Entities
{
    public class Media:BaseEntity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}