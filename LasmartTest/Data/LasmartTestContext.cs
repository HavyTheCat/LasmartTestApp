using LasmartTest.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasmartTest.Data
{
    public class LasmartTestContext :  IdentityDbContext<AppUser>
    {
        public LasmartTestContext(DbContextOptions<LasmartTestContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        public DbSet<Equipment> Equipments { get; set; }
    }
}
