using Microsoft.EntityFrameworkCore;
using Testamente.DataAccess;
using Testamente.Domain;

namespace Testamente.DataAccessTests;

[TestClass]
public class RepositoryTests
{
    [TestMethod]
    public async Task PersonRepository_UsesDbContextAsExpected()
    {
        var context = CreateTestContext();
        var repo = new PersonRepository(context);
        var father = new Person(){PersonId=2, Name="Father", Address="AddressTwo", BirthDate = new(1980,1,1), IsAlive=true};
        var d = new Person(){PersonId=1, Name="TestName", Address="Address", BirthDate = new(1996,3,6), IsAlive=true, Father=father};
        await repo.SaveCreateAsync(d);

        var saved = context.People.Single(p => p.PersonEntityId== d.PersonId);
        Assert.AreEqual(d.Name, saved.Name);
        Assert.AreEqual(d.IsAlive, saved.IsAlive);
        Assert.AreEqual(d.Address, saved.Address);
        Assert.AreEqual(d.Father.PersonId, saved.FatherId);
    }
    [TestMethod]
    public async Task ReportSectionRepository_UsesDbContextAsExpected()
    {
        var context = CreateTestContext();
        var repo = new ReportSectionRepository(context);
        var d = new ReportSection() { ReportSectionId = 1, Body = "myBody", Title = "TestTitle" };
        await repo.SaveCreateAsync(d);

        var saved = context.ReportSections.Single(p => p.ReportSectionEntityId== d.ReportSectionId);
        Assert.AreEqual(d.Title, saved.Title);
        Assert.AreEqual(d.Body, saved.Body);
    }

    private TestamenteContext CreateTestContext()
    {
        var options = new DbContextOptionsBuilder<TestamenteContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        return new TestamenteContext(options);
    }

}