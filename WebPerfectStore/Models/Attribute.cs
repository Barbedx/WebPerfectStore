namespace WebPerfectStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Attribute
    {
        public Attribute()
        {
            DocTypes = new HashSet<DocType>();
            Answers = new HashSet<Answer>();
        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttrID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string AttrName { get; set; }

        [StringLength(50)]
        public string Exid { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActiveFlag { get; set; }

        [StringLength(255)]
        public string AttrShortName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sort { get; set; }

        public virtual ICollection<DocType> DocTypes { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
