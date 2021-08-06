namespace Models.Framework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeedBack")]
    public partial class FeedBack
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [Column(TypeName = "ntext")]
        public string Content_FeedBack { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}
