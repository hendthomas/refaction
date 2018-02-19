namespace refactor_me.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public partial class Product
    {
        // Primary key
        [Key]
        public Guid Id { get; set; }
     
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal DeliveryPrice { get; set; }

        // Navigation property
        [JsonIgnore]
        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}