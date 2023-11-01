
namespace Ivanoav_Glazki
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GlazkiEntities : DbContext

    {
        public GlazkiEntities()
             : base("name=GlazkiEntities")
        {
        }
      private static GlazkiEntities _context;
        public static GlazkiEntities GetContext()
        {
            if (_context == null)
                _context = new GlazkiEntities();
            return _context;
        }



        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentPriorityHistory> AgentPriorityHistory { get; set; }
        public virtual DbSet<AgentType> AgentType { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<MaterialCountHistory> MaterialCountHistory { get; set; }
        public virtual DbSet<MaterialType> MaterialType { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCostHistory> ProductCostHistory { get; set; }
        public virtual DbSet<ProductMaterial> ProductMaterial { get; set; }
        public virtual DbSet<ProductSale> ProductSale { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
    }
}
