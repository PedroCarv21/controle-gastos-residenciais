using Microsoft.EntityFrameworkCore;
using ResidentialExpenseControl.Api.Models;

namespace ResidentialExpenseControl.Api.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public DbSet<Person> People => Set<Person>();

	public DbSet<Transaction> Transactions => Set<Transaction>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Person>()
			.HasMany(person => person.Transactions)
			.WithOne(transaction => transaction.Person)
			.HasForeignKey(transaction => transaction.PersonId)
			.OnDelete(DeleteBehavior.Cascade);
		
			base.OnModelCreating(modelBuilder);
	}
}