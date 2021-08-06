namespace Models.Framework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        public long CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }


        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public int? ViewCout { get; set; }

        public bool Status { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        [Required]
        [StringLength(550)]
        [AllowHtml]
        public string MetaDescriptions { get; set; }
    }
}
