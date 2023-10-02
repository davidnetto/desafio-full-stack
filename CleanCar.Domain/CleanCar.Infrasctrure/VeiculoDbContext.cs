using CleanCar.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Infrastructure
{
    public class VeiculoDbContext :DbContext
    {
        public VeiculoDbContext(DbContextOptions<VeiculoDbContext> options)
            :base(options)
        {
                
        }

        public DbSet<Veiculo> Veiculos {get; set;}
    }
}
