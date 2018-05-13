namespace WebPerfectStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Answer
    {
 

        public Answer(int MasterFid, int orID, int ItemId, int AttrId, string Value)
        {
            this.MasterFid = MasterFid;
            this.ItemId = ItemId;
            this.AttrId = AttrId;
            this.Value = Value;
            this.OrderID = orID;
        }
        public Answer()
        {
                
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MasterFid { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttrId { get; set; }

        [StringLength(255)]
        public string Value { get; set; }
 
 
        public virtual Order Order { get; set; }
        
        public virtual Item Item { get; set; }

        public virtual Attribute Attribute { get; set; }

    }
}
