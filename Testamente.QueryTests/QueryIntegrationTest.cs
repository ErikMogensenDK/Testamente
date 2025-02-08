using System.Data;
using Moq;
using Testamente.Query;

namespace Testamente.QueryTests;

[TestClass]
public class QueryIntegrationTest 
{
    [TestMethod]
    public async Task PersonGet_UsesDbConnectionAndQueryExecutorAsExpected()
    {
        var id = Guid.NewGuid();
        var sql = $"select PersonEntityId as id, Name, CAST(BirthDate AS DATE) BirthDate, Address, IsAlive, FatherId, MotherId, SpouseId from People where IsDeleted = 'FALSE' AND PersonEntityId = '{id}'";
        var inputRowDto = new PersonRowDto 
        {
            Id = id,
            Name = "Testname for database",
            Address = "Adresse 123, 5000 Odense C",
            BirthDate = new(1996, 07, 03),
            IsAlive = true,
            FatherId = Guid.NewGuid(),
            MotherId = Guid.NewGuid(),
            SpouseId = Guid.NewGuid()
        };
        var conn = new Mock<IDbConnection>().Object;
        var connProvider = new Mock<IDbConnectionProvider>();
        connProvider.SetReturnsDefault(conn);
        var exe = new Mock<IQueryExecutor>();
        exe.Setup(e => e.Query<PersonRowDto>(conn, sql, null,null,true,null,null))
        .Returns(new List<PersonRowDto>{inputRowDto});
        var q = new GetAllPeopleAssocaitedWithUserId(connProvider.Object, exe.Object);

        var PersonDtoFromDb = q.Get(id);

        //test sql was called 
        exe.Verify(e => e.Query<PersonRowDto>(conn, sql, null,null,true,null,null), Times.Once);
        //test response was mapped

        Assert.AreEqual(inputRowDto.Id, PersonDtoFromDb.Id);
        Assert.AreEqual(inputRowDto.Name, PersonDtoFromDb.Name);
        Assert.AreEqual(inputRowDto.Address, PersonDtoFromDb.Address);
        Assert.AreEqual(inputRowDto.BirthDate, PersonDtoFromDb.BirthDate);
        Assert.AreEqual(inputRowDto.FatherId, PersonDtoFromDb.FatherId);
        Assert.AreEqual(inputRowDto.MotherId, PersonDtoFromDb.MotherId);
        Assert.AreEqual(inputRowDto.SpouseId, PersonDtoFromDb.SpouseId);
    }
}