namespace refactor_me.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductOption")]
    public partial class ProductOption
    {
        // Primary key
        [Key]
        public Guid Id { get; set; }

        // Foreign key
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
