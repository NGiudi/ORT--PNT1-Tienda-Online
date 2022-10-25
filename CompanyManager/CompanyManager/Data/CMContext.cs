using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;

    public class CMContext : DbContext
    {
        public CMContext (DbContextOptions<CMContext> options)
            : base(options)
        {
        }

        public override int SaveChanges() {
            foreach (var item in ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Deleted && 
                e.Metadata.GetProperties().Any(x => x.Name == "DeletedAt")))
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["DeletedAt"] = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        public DbSet<CompanyManager.Models.Product> Product { get; set; } = default!;

        public DbSet<CompanyManager.Models.User> User { get; set; }

        public DbSet<CompanyManager.Models.Sale> Sale { get; set; }
    }
