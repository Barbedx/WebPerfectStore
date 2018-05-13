namespace WebPerfectStore.Models
{
    using System;
    using System.Data.Entity;

    public partial class Na_project : DbContext
    {
        public Na_project()
            : base("name=Na_project") {
            Database.SetInitializer(new MyDbInitializer());
        }

        public virtual IDbSet<Agent> Agents { get; set; }
        public virtual IDbSet<OutletAttribute> OutletAttributes { get; set; }
        public virtual IDbSet<Outlet> Outlets { get; set; }

        public virtual IDbSet<Attribute> Attributes { get; set; }
        public virtual IDbSet<DocType> DocTypes { get; set; }
        public virtual IDbSet<Item> Items { get; set; }
        public virtual IDbSet<Order> Orders { get; set; }
        public virtual IDbSet<Answer> Answers { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.Sum)
                .HasPrecision(19, 9);

            #region  mapping to DB Tables
            modelBuilder.Entity<Agent>().ToTable("web_Agents");
            modelBuilder.Entity<Outlet>().ToTable("web_Outlets");
            modelBuilder.Entity<OutletAttribute>().ToTable("web_OutletAttributes");
            modelBuilder.Entity<Order>().ToTable("web_Orders");
            modelBuilder.Entity<Attribute>().ToTable("web_Attributes");
            modelBuilder.Entity<DocType>().ToTable("web_DocTypes");
            modelBuilder.Entity<Item>().ToTable("web_Items");
            modelBuilder.Entity<Answer>().ToTable("web_Orders_Items_Attributes");
            #endregion


            #region Map PK
            modelBuilder.Entity<Agent>().HasKey(k => k.FId);
            modelBuilder.Entity<Outlet>().HasKey(k => k.fID);
            modelBuilder.Entity<OutletAttribute>().HasKey(k => new { k.Attrid, k.Id });
            modelBuilder.Entity<Order>().HasKey(k => new { k.ID, k.MasterFID });
            modelBuilder.Entity<Attribute>().HasKey(k => k.AttrID);
            modelBuilder.Entity<DocType>().HasKey(k => k.DocTypeID);
            modelBuilder.Entity<Item>().HasKey(k => k.ID);
            modelBuilder.Entity<Answer>().HasKey(k => new { k.AttrId, k.MasterFid, k.ItemId, k.OrderID });
            #endregion

            #region Relationships
            #region Many-to-many
            modelBuilder.Entity<DocType>()
                .HasMany(dt => dt.Attributes)
                .WithMany(a => a.DocTypes)
                .Map(m =>
                {
                    m.MapLeftKey("MerTypeId");
                    m.MapRightKey("AttrId");
                    m.ToTable("DS_MerTypeAttributes");
                });

            modelBuilder.Entity<Item>()
                .HasMany(h => h.DocTypes)
                .WithMany(g => g.Items)
                .Map(m =>
                {
                    m.MapLeftKey("Id");
                    m.MapRightKey("MerTypeId");
                    m.ToTable("DS_MerObjects");
                });

            modelBuilder.Entity<Agent>()
                .HasMany(x => x.Outlets)
                .WithMany(x => x.Agents)
                .Map(m =>
                {
                    m.MapLeftKey("MasterFID");
                    m.MapRightKey("mFid");
                    m.ToTable("MobFaces");
                });

            #endregion
            #region One-to-many

            modelBuilder.Entity<Order>()
                .HasRequired(f => f.MasterAgent)
                .WithMany(o => o.Orders)
                .HasForeignKey(k => k.MasterFID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasRequired(f => f.Outlet)
                .WithMany(o => o.Orders)
                .HasForeignKey(k => k.OutletID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
                .HasRequired(a => a.Attribute)
                .WithMany(a => a.Answers)
                .HasForeignKey(k => k.AttrId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
                .HasRequired(o => o.Order)
                .WithMany(a => a.Answers)
                .HasForeignKey(k => new { k.OrderID, k.MasterFid })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
                .HasRequired(i => i.Item)
                .WithMany(a => a.Answers)
                .HasForeignKey(k => k.ItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OutletAttribute>()
                .HasRequired(x => x.Outlet)
                .WithMany(x => x.OutletAttributes)
                .HasForeignKey(c => c.Id)
                .WillCascadeOnDelete(false);
            #endregion
            #endregion

            #region Mapping to stored procedures

            modelBuilder.Entity<Order>()
                .MapToStoredProcedures(sp =>
                {
                    sp.Insert(i => i.HasName("web_DS_Set_DocHeaders")

                                    .Parameter(p => p.MasterFID, "MasterFID")
                                    .Parameter(p => p.OutletID, "mfID")
                                    .Parameter(p => p.DocTypeID, "orType")
                                    .Parameter(p => p.Comment, "orComment")
                                    .Parameter(p => p.Sum, "orSum")

                                    .Result(r => r.Number, "orNumber")
                                    .Result(r => r.OrderDate, "orDate")
                                    .Result(r => r.ID, "orID"));

                    sp.Update(i => i.HasName("web_DS_Set_DocHeaders")
                    
                                    .Parameter(p => p.ID, "orId")
                                    .Parameter(p => p.MasterFID, "MasterFID")
                                    .Parameter(p => p.OutletID, "mfID")
                                    .Parameter(p => p.DocTypeID, "orType")
                                    .Parameter(p => p.Comment, "orComment")
                                    .Parameter(p => p.Sum, "orSum")

                                    .Result(r => r.Number, "orNumber")
                                    .Result(r => r.OrderDate, "orDate"));

                });

            modelBuilder.Entity<Answer>()
                .MapToStoredProcedures(sp =>
                {
                    sp.Insert(i =>
                    {
                        i.HasName("web_opl_add_answer_to_order");

                        i.Parameter(p => p.OrderID, "OrId");
                        i.Parameter(p => p.MasterFid, "MasterFid");
                        i.Parameter(p => p.ItemId, "Id");
                        i.Parameter(p => p.AttrId, "AttrId");
                        i.Parameter(p => p.Value, "AttrText");
                    });
                });
            #endregion

        }
    }
}
