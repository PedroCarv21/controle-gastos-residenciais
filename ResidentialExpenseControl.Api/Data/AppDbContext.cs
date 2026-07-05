using Microsoft.EntityFrameworkCore;
using ResidentialExpenseControl.Api.Models;

namespace ResidentialExpenseControl.Api.Data;

/// <summary>
/// This class represents the database session. 
/// It is responsible for managing operations such as querying, insertion, updating, and deletion. 
/// To do this, it is necessary to create a subclass of DbContext.
/// </summary>
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

    /// <summary>
    /// The two properties, People and Transactions, are used 
	/// so that EF Core understands the structure of the database tables.
    /// </summary>
    public DbSet<Person> People => Set<Person>();
	public DbSet<Transaction> Transactions => Set<Transaction>();

    /// <summary>
    /// This method will be used to configure the database rules.
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder object</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Person>()
			.HasMany(person => person.Transactions) // Indicates that a person may have one or more transactions.
            .WithOne(transaction => transaction.Person) // Indicates that a transaction is associated with a single. person.
            .HasForeignKey(transaction => transaction.PersonId) // personId will be the foreign key of Transaction.
            .OnDelete(DeleteBehavior.Cascade); // If a person is deleted, all transactions linked to them will also be deleted.

        base.OnModelCreating(modelBuilder); // Executes the previously defined settings.
    }
}