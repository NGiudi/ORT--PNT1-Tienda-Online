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

        public DbSet<CompanyManager.Models.Product> Product { get; set; } = default!;

        public DbSet<CompanyManager.Models.User> User { get; set; }
    }
