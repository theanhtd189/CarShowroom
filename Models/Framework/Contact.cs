namespace Models.Framework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Contact")]
    public partial class Contact
    {
        public long ID { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content_Name { get; set; }

        public bool Status { get; set; }
    }
}
