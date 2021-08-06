namespace Models.Framework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? Quantity { get; set; }

        public long CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public string Color { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string Brand { get; set; }

        public int? ViewCout { get; set; }

        public bool Status { get; set; }
    }
}
