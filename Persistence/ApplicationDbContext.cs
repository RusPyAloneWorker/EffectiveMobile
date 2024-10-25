using Domain;

namespace Persistence;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
	public DbSet<Order> Orders { get; set; }
	
	public DbSet<Log> Logs { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

		Database.EnsureCreated();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.Entity<Log>()
			.Property(r => r.Status)
			.HasConversion(
				v => v.ToString(),
				v => (LogStatus)Enum.Parse(typeof(LogStatus), v));
		
		var orders = new List<Order>()
		{
			new (35, new DateTime(2024, 9, 23, 3, 56, 33), "Москва, Южная 25"),
			new (35, new DateTime(2024, 12, 13, 21, 56, 13), "Москва, Новороссийская 20")
		};

		modelBuilder.Entity<Order>().HasData(orders);
		
		base.OnModelCreating(modelBuilder);
	}
}
