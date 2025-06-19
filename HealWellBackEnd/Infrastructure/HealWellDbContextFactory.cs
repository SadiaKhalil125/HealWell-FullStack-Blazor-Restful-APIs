using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure;

public class HealWellDbContextFactory : IDesignTimeDbContextFactory<HealWellDbContext>
{
    public HealWellDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HealWellDbContext>();

        // Use your actual connection string here
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HealWellDB;Integrated Security=True;");

        return new HealWellDbContext(optionsBuilder.Options);
    }
}
