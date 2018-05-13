namespace WebPerfectStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DocType
    {
        public DocType()
        {
            Attributes = new HashSet<Attribute>();
            Items = new HashSet<Item>();
            Orders = new HashSet<Order>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocTypeID { get; set; }

        [StringLength(50)]
        public string DocTypeName { get; set; }

        [StringLength(50)]
        public string DocTypeShortName { get; set; }

        public int? Activeflag { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
