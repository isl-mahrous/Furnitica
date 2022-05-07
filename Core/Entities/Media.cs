using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Media : BaseEntity
    {
        [JsonIgnore]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }

    
      
    }
}