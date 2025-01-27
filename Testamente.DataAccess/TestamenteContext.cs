using Microsoft.EntityFrameworkCore;

namespace Testamente.DataAccess;

public class TestamenteContext : DbContext
{
	public TestamenteContext(DbContextOptions<TestamenteContext> options) : base(options)
	{

	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PersonEntity>()
            // Self-referencing relationship for family connections
            .HasOne(p => p.Mother)
            .WithMany()
            .HasForeignKey(p => p.MotherId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PersonEntity>()
            .HasOne(p => p.Father)
            .WithMany()
            .HasForeignKey(p => p.FatherId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PersonEntity>()
            .HasOne(p => p.Spouse)
            .WithMany()
            .HasForeignKey(p => p.SpouseId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }


	public DbSet<ReportSectionEntity> ReportSections { get; set; }
	public DbSet<PersonEntity> People { get; set; }
}