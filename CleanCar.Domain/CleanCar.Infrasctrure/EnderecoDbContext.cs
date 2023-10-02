using Microsoft.EntityFrameworkCore;
using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Infrastructure
{
    public class EnderecoDbContext : DbContext
    {
        public EnderecoDbContext(DbContextOptions<EnderecoDbContext> options)
            :base(options)
        {
                
        }

        public DbSet<Endereco> Enderecos { get; set;}
    }
}
