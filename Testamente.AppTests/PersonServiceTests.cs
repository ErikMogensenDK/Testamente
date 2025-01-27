using Testamente.Domain;
using Moq;
using Testamente.App.Services;
using Testamente.App.Models;

namespace Testamente.AppTests;

[TestClass]
public class PersonServiceTests
{
	[TestMethod]
	public async Task TestCreate_usesRepoAsExpected()
	{
		//send opret request
		var request = new CreatePersonRequest 
		{
			Name= "TestName ",
			Address = "some addresss",
			IsAlive = false,
			BirthDate = new(1985, 6, 3),
		};
		//se at der bliver gemt det rigtige domæne object
		var repo = new Mock<IPersonRepository>();
		Person saved = null;
		repo.Setup(r => r.SaveCreateAsync(It.IsAny<Person>()))
		.Callback<Person>(p => saved = p);

		var s = new PersonService(repo.Object);

		var id = Guid.NewGuid();
		await s.CreateAsync(id, request);

		Assert.IsNotNull(saved);
		Assert.AreEqual(id, saved.PersonId);
		Assert.AreEqual(request.Address, saved.Address);
		Assert.AreEqual(request.IsAlive, saved.IsAlive);
		Assert.AreEqual(request.BirthDate, saved.BirthDate);
	}
	[TestMethod]
	public async Task TestDelete_RemovesObjectAsExpected()
	{
		//send opret request
		var request = new CreatePersonRequest 
		{
			Name= "TestName ",
			Address = "some addresss",
			IsAlive = false,
			BirthDate = new(1985, 6, 3),
		};
		var id = Guid.NewGuid();
		//se at der bliver gemt det rigtige domæne object
		var repo = new Mock<IPersonRepository>();
		Person saved = null;
		repo.Setup(r => r.SaveCreateAsync(It.IsAny<Person>()))
		.Callback<Person>(p => saved = p);

		var s = new PersonService(repo.Object);

		await s.CreateAsync(id, request);
		await s.DeleteAsync(id);

		//Assert
		repo.Verify(r => r.DeleteAsync(id), Times.Once());
	}
	[TestMethod]
	public async Task TestUpdate_UpdatesAsExpected()
	{
		//send opret request
		var request = new CreatePersonRequest 
		{
			Name= "TestName ",
			Address = "some addresss",
			IsAlive = false,
			BirthDate = new(1985, 6, 3),
		};
		var newRequest = new CreatePersonRequest 
		{
			Name= "Updated name",
			Address = "some different address",
			IsAlive = true,
			BirthDate = new(1984, 6, 3),
		};
		var id = Guid.NewGuid();
		//se at der bliver gemt det rigtige domæne object
		var repo = new Mock<IPersonRepository>();
		Person updated = null;
		repo.Setup(r => r.SaveUpdateAsync(It.IsAny<Person>()))
		.Callback<Person>(p => updated = p);

		var s = new PersonService(repo.Object);

		await s.CreateAsync(id, request);
		await s.UpdateAsync(id, newRequest);

		//Assert
		repo.Verify(r => r.SaveUpdateAsync(It.IsAny<Person>()), Times.Once());
		Assert.IsNotNull(updated);
		Assert.AreEqual(id, updated.PersonId);
		Assert.AreEqual(newRequest.Address, updated.Address);
		Assert.AreEqual(newRequest.IsAlive, updated.IsAlive);
		Assert.AreEqual(newRequest.BirthDate, updated.BirthDate);
	}
}