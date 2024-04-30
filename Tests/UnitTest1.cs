using NUnit.Framework;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

using AbonentListDataLibrary;
using AbonentListModelLibrary.Models;

namespace Tests
{

  public class Tests
  {
    private AbonentListDBConfiguration mAbonentListDBConfiguration;

    [SetUp]
    public void Setup()
    {
      mAbonentListDBConfiguration = new AbonentListDBConfiguration();
      mAbonentListDBConfiguration.AddRandomAbonents();
    }

    [TearDown]
    public void Teardown()
    {
      mAbonentListDBConfiguration?.Dispose();
    }

    [Test]
    public void Test_FillDb()
    {
        var aAbonents = mAbonentListDBConfiguration.AbonentListDB.Abonents.Local;
        string aConnectionString = mAbonentListDBConfiguration.AbonentListDB.DatabaseConnectionString;
        Abonent[] aList2;
        using (var connection = new SqliteConnection(aConnectionString))
        {
            connection.Open();
            var res = connection.Query<Abonent>("SELECT * FROM Abonents");
            aList2 = res.ToArray();
        }
        Assert.That(aList2, Is.EquivalentTo(aAbonents));
    }
  }
}