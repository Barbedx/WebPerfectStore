namespace WebPerfectStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Item
    {
        public Item()
        {
            DocTypes = new HashSet<DocType>();
            Answers = new HashSet<Answer>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }

        public int? Sort { get; set; }

        public virtual ICollection<DocType> DocTypes { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
}
