﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

        [JsonIgnore]
        [Required]
        public virtual AppUser User { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}