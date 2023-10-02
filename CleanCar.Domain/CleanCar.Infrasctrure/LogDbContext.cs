using Microsoft.EntityFrameworkCore;
using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Infrastructure
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {

        }

        public DbSet<Log> Log { get; set; }
    }
}
