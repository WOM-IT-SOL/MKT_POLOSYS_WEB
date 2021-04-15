using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MKT_POLOSYS_WEB;

#nullable disable

namespace MKT_POLOSYS_WEB.DataAccess
{
    public partial class WISE_STAGINGContext : DbContext
    {
        public WISE_STAGINGContext()
        {
        }

        public WISE_STAGINGContext(DbContextOptions<WISE_STAGINGContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json")
                 .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}