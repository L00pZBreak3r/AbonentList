using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

using AbonentListModelLibrary.Models;

namespace AbonentListDataLibrary.EF
{
  public class AbonentListDBContext : DbContext
  {
    public readonly string DatabaseName;
    public readonly string DatabaseConnectionString;

    private readonly SqliteConnection mSqliteSqlConnection;

    public DbSet<Abonent> Abonents { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AssignedStreet> AssignedStreets { get; set; }

    public AbonentListDBContext(string aDatabaseName)
    {
      DatabaseName = aDatabaseName;
      DatabaseConnectionString = "Data Source=" + DatabaseName + ";Mode=Memory;Cache=Shared;";
      mSqliteSqlConnection = new SqliteConnection(DatabaseConnectionString);
      mSqliteSqlConnection.Open();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Abonent>()
        .HasIndex(p => new { p.LastName, p.FirstName, p.MiddleName }).IsUnique();

      modelBuilder.Entity<AssignedStreet>()
        .HasIndex(p => p.Street).IsUnique();

      modelBuilder.Entity<Address>()
        .HasIndex(p => new { p.Country, p.State, p.City, p.Street, p.Building, p.Apartment }).IsUnique();

      var aEntityPhoneNumber = modelBuilder.Entity<PhoneNumber>();
      aEntityPhoneNumber
        .HasIndex(p => new { p.Number, p.RegionCode, p.RegionNumber }).IsUnique();

      aEntityPhoneNumber
        .Property(c => c.PhoneNumberType)
        .HasConversion<int>();

      base.OnModelCreating(modelBuilder);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite(DatabaseConnectionString);
    }

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          mSqliteSqlConnection.Dispose();
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }
  }
}
