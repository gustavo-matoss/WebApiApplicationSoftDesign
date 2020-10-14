using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebApplication1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }
        public DbSet<Application> Arquivos { get; set; }

        
    }
}
