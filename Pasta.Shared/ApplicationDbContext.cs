using Microsoft.EntityFrameworkCore;

using Pasta.Shared.Entities;

namespace Pasta.Shared;

public class ApplicationDbContext : DbContext
{
    public DbSet<ConfigurationEntity?> Configurations { get; set; }
    public DbSet<Header> Headers { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Webhook> Webhooks { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}