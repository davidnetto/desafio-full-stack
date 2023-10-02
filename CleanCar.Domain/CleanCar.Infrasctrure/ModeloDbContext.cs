using Microsoft.EntityFrameworkCore;
using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Infrastructure
{
    public class ModeloDbContext : DbContext
    {
        public ModeloDbContext(DbContextOptions<ModeloDbContext> options)
            :base(options)
        {
                
        }

        public DbSet<Modelo> Modelos { get; set;}
    }
}
