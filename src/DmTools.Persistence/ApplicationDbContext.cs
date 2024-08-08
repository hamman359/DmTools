using Microsoft.EntityFrameworkCore;

namespace DmTools.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    //Update-Database -a DmTools.Persistence -s DmTools.App
    //Add-Migration InitialCreate -a DmTools.Persistence -s DmTools.App
}
