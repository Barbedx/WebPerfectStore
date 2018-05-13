namespace WebPerfectStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Principal;

    public partial class Agent : IPrincipal//s: MembershipUser// IAgentUser
    { 
        public Agent()
        {
            Outlets = new HashSet<Outlet>();
            Orders = new HashSet<Order>();
        }


        //[Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FId { get; set; }

        public int ActiveFlag { get; set; }
       
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string VIP { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        [StringLength(8)]
        public string Login { get; set; }

        [MaxLength(8000)]
        public byte[] Password { get; set; }

        public int? gid { get; set; }
        
        [NotMapped]
        public IIdentity Identity { get; set; }

        [NotMapped]
        public bool IsAdmin => ((gid ?? 0) == 0);

        
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }


        public virtual ICollection<Outlet> Outlets { get; set; }
        public virtual ICollection<Order> Orders { get; internal set; }
    }
}
