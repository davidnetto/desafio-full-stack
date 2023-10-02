using Microsoft.EntityFrameworkCore;
using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Infrastructure
{
    public class MontadoraDbContext : DbContext
    {
        public MontadoraDbContext(DbContextOptions<MontadoraDbContext> options)
            :base(options)
        {
                
        }

        public DbSet<Montadora> Montadoras { get; set;}
    }
}
