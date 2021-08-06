namespace Models.Framework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserGroup")]
    public partial class UserGroup
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
