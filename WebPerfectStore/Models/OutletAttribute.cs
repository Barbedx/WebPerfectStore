namespace WebPerfectStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class OutletAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Attrid { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
       

        [StringLength(255)]
        public string AttrName { get; set; }

        [StringLength(255)]
        public string AttrText { get; set; }

        [StringLength(50)]
        public string exid { get; set; }

        [ForeignKey("Id")]
        public virtual Outlet Outlet { get; set; }
    }
}
