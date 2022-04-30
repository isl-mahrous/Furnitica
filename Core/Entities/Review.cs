using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Review: BaseEntity
    {
        public string UserId { get; set; }

        [ForeignKey("Id")]
        public AppUser User { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int Stars { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}