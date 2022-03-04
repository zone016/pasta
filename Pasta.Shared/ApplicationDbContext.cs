using Microsoft.EntityFrameworkCore;

using Pasta.Shared.Entities;

namespace Pasta.Shared;

public class ApplicationDbContext : DbContext
{
    public DbSet<ConfigurationEntity?> Configurations { get; set; }
    public DbSet<HeaderEntity> Headers { get; set; }
    public DbSet<PortEntity> Ports { get; set; }
    public DbSet<JobEntity> Jobs { get; set; }
    public DbSet<ReportEntity> Reports { get; set; }
    public DbSet<WebhookEntity> Webhooks { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}