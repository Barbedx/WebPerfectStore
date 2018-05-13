namespace WebPerfectStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Order
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MasterFID { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Номер")]
        public string Number { get; set; }

        public int? OutletID { get; set; }
        [DataType(DataType.DateTime), Display(Name = "Время создания")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? OrderDate { get; set; }

        [StringLength(250)]
        [Display(Name ="Коментарий")]
        public string Comment { get; set; }

        [Display(Name = "Сумма")]
        public decimal? Sum { get; set; }

        public Order()
        {
            Answers = new HashSet<Answer>();
        }

        public int? DocTypeID { get; set; }
        
        public int Condition { get; set; }

        [NotMapped]
        [Display(Name = "Активность")]
        public bool Activity
        {
            get { return Condition==1; }
            set { Condition = value?1:0; }
        }


        public int? MasterDocID { get; set; }

        public int? MasterDocMasterfID { get; set; }


        public virtual ICollection<Answer> Answers { get; set; }
        //public virtual ICollection<Attachment> Attachments { get; set; }

        [Display( Name = "Точка")]
        public virtual Outlet Outlet { get; set; }

        [Display(Name = "Агент")]
        public virtual Agent MasterAgent { get; set; }

        [Display(Name = "Тип Документа")]
        public virtual DocType DocType { get; set; }

    }
}
