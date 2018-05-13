namespace WebPerfectStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Outlet
    {
        public Outlet()
        {
            Agents = new HashSet<Agent>();
            OutletAttributes = new HashSet<OutletAttribute>();
            Orders = new HashSet<Order>();
        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fID { get; set; }
        
        public int fActiveFlag { get; set; }

        [StringLength(100)]
        [Display(Name = "Название точки",ShortName ="Точка")]
        public string fName { get; set; }

        [Display(Name = "Адресс точки",ShortName ="Адресс")]
        [StringLength(100)]
        public string fAddress { get; set; }

        public int? CityC { get; set; }

        [StringLength(255)]
        [Display(Name = "Название точки", ShortName = "Точка")]
        public string POP { get; set; }

        public int? naid { get; set; }

        public int? outletTC { get; set; }

        [StringLength(50)]
        [Display(Name = "Площадь точки",ShortName ="Площадь")]
        public string StoreArea { get; set; }

        [StringLength(50)]
        
        public string Tier { get; set; }

        [StringLength(50)]
        public string Traffic { get; set; }

        [Display(Name = "Комментарий")]
        [StringLength(250)]
        public string fcomment { get; set; }

        [StringLength(50)]
        [Display(Name = "Координаты точки",ShortName ="Координаты")]
        public string Coordinates { get; set; }
        //public int? masterfid { get; set; } 

        [Display(Name = "Агенты точки", ShortName = "Агенты")]
        public virtual ICollection<Agent> Agents { get; set; }

        [Display(Name = "Атрибуты точки",ShortName ="Атрибуты")]
        public virtual ICollection<OutletAttribute> OutletAttributes { get; set; }

        [Display(Name = "Документы точки",ShortName ="Документы")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
