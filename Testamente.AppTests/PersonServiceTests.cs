using Testamente.Domain;
using Moq;
using Testamente.App.Services;
using Testamente.App.Models;
using Testamente.Query;

namespace Testamente.AppTests;

[TestClass]
public class PersonServiceTests
{
	[TestMethod]
	public async Task TestCreate_usesRepoAsExpected()
	{
		//send opret request
		var createdById= Guid.NewGuid();
		var request = new CreatePersonRequest 
		{
			Name= "TestName ",
			Address = "some addresss",
			IsAlive = false,
			BirthDate = new(1985, 6, 3),
			CreatedById= createdById
		};
		//se at der bliver gemt det rigtige domæne object
		var repo = new Mock<IPersonRepository>();
		Person saved = null;
		repo.Setup(r => r.SaveCreateAsync(It.IsAny<Person>(), It.Is<Guid>(g => g == createdById)))
		.Callback((Person p, Guid id) => saved = p);

		var query = new Mock<IPersonQuery>();

		var s = new PersonService(repo.Object, query.Object);

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
		var createdById= Guid.NewGuid();
		var request = new CreatePersonRequest 
		{
			Name= "TestName ",
			Address = "some addresss",
			IsAlive = false,
			BirthDate = new(1985, 6, 3),
			CreatedById = createdById
		};
		var id = Guid.NewGuid();
		//se at der bliver gemt det rigtige domæne object
		var repo = new Mock<IPersonRepository>();
		Person saved = null;
		repo.Setup(r => r.SaveCreateAsync(It.IsAny<Person>(), It.Is<Guid>(g => g == createdById)))
		.Callback((Person p, Guid id) => saved = p);

		var query = new Mock<IPersonQuery>();

		var s = new PersonService(repo.Object, query.Object);

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

		var query = new Mock<IPersonQuery>();

		var s = new PersonService(repo.Object, query.Object);

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

	[TestMethod]
	public async Task GetAndAssociateUsers_ReturnsExpectedUserTree()
	{
		//Arrange
		var repo = new Mock<IPersonRepository>();
		var query = new Mock<IPersonQuery>();
		string id = "968b93e1-2f8a-4641-b9b4-21527a366c8f";
		Guid userGuid = new("968b93e1-2f8a-4641-b9b4-21527a366c8f");
		Person expectedPerson = CreateExpectedPersonTree(id);

		query.Setup(r => r.GetAllPeopleAssociatedWithUserId(new(id))).Returns(GetPersonQueryDtosFromPerson(expectedPerson));
		PersonService service = new(repo.Object, query.Object);

		//act
		var actualPerson = await service.GetAndAssociateUsersCreatedByAsync(userGuid);

		//Assert
		//Check first child
		Assert.AreEqual(expectedPerson.Children[0].PersonId, actualPerson.Children[0].PersonId);
		Assert.AreEqual(expectedPerson.Children[0].Name, actualPerson.Children[0].Name);
		Assert.AreEqual(expectedPerson.Children[0].Address, actualPerson.Children[0].Address);
		Assert.AreEqual(expectedPerson.Children[0].BirthDate, actualPerson.Children[0].BirthDate);
		Assert.AreEqual(expectedPerson.Children[0].IsAlive, actualPerson.Children[0].IsAlive);

		//Check second child
		Assert.AreEqual(expectedPerson.Children[1].PersonId, actualPerson.Children[1].PersonId);
		Assert.AreEqual(expectedPerson.Children[1].Name, actualPerson.Children[1].Name);
		Assert.AreEqual(expectedPerson.Children[1].Address, actualPerson.Children[1].Address);
		Assert.AreEqual(expectedPerson.Children[1].BirthDate, actualPerson.Children[1].BirthDate);
		Assert.AreEqual(expectedPerson.Children[1].IsAlive, actualPerson.Children[1].IsAlive);

		//Check spouse 
		Assert.AreEqual(expectedPerson.Spouse.PersonId, actualPerson.Spouse.PersonId);
		Assert.AreEqual(expectedPerson.Spouse.Name, actualPerson.Spouse.Name);
		Assert.AreEqual(expectedPerson.Spouse.Address, actualPerson.Spouse.Address);
		Assert.AreEqual(expectedPerson.Spouse.BirthDate, actualPerson.Spouse.BirthDate);
		Assert.AreEqual(expectedPerson.Spouse.IsAlive, actualPerson.Spouse.IsAlive);

		//Check father 
		Assert.AreEqual(expectedPerson.Father.PersonId, actualPerson.Father.PersonId);
		Assert.AreEqual(expectedPerson.Father.Name, actualPerson.Father.Name);
		Assert.AreEqual(expectedPerson.Father.Address, actualPerson.Father.Address);
		Assert.AreEqual(expectedPerson.Father.BirthDate, actualPerson.Father.BirthDate);
		Assert.AreEqual(expectedPerson.Father.IsAlive, actualPerson.Father.IsAlive);

		//Check Mother 
		Assert.AreEqual(expectedPerson.Mother.PersonId, actualPerson.Mother.PersonId);
		Assert.AreEqual(expectedPerson.Mother.Name, actualPerson.Mother.Name);
		Assert.AreEqual(expectedPerson.Mother.Address, actualPerson.Mother.Address);
		Assert.AreEqual(expectedPerson.Mother.BirthDate, actualPerson.Mother.BirthDate);
		Assert.AreEqual(expectedPerson.Mother.IsAlive, actualPerson.Mother.IsAlive);
	}

    private Person CreateExpectedPersonTree(string id)
    {
		Person person = new()
		{
			PersonId = new(id),
			Name = "OriginalPersonName",
			BirthDate = new(2000, 5, 5),
			Address = "Some Address",
			Children = new List<Person>() { new(Guid.NewGuid(), "ChildOne", new(2015, 10, 10), "childAddressOne"), new(Guid.NewGuid(), "ChildTwo", new(2015, 11, 11), "childAddressTwo") },
			Spouse = new(Guid.NewGuid(), "Spouse", new(2000, 1, 1), "SpouseAddress"),
			Father = new(Guid.NewGuid(), "Father", new(1980, 1, 4), "FatherAddress", false),
			Mother = new(Guid.NewGuid(), "Mother", new(1981, 2, 1), "MotherAddress"),
			IsAlive = true
		};
		return person;
	}
	private List<PersonQueryDto> GetPersonQueryDtosFromPerson(Person person)
	{
		List<PersonQueryDto> people = new();
		PersonQueryDto testator = new(){
			Id = person.PersonId,
			Name = person.Name,
			Address = person.Address,
			IsAlive = person.IsAlive,
			BirthDate = person.BirthDate,
			FatherId = person.Father.PersonId,
			MotherId = person.Mother.PersonId,
			SpouseId = person.Spouse.PersonId
		};
		people.Add(testator);

		PersonQueryDto ChildOne = new(){
			Id = person.Children[0].PersonId,
			Name = person.Children[0].Name,
			Address = person.Children[0].Address,
			BirthDate = person.Children[0].BirthDate,
			IsAlive = person.Children[0].IsAlive,
			FatherId = person.PersonId
		};
		people.Add(ChildOne);

		PersonQueryDto ChildTwo = new(){
			Id = person.Children[1].PersonId,
			Name = person.Children[1].Name,
			BirthDate = person.Children[1].BirthDate,
			Address = person.Children[1].Address,
			IsAlive = person.Children[1].IsAlive,
			FatherId = person.PersonId
		};
		people.Add(ChildTwo);

		PersonQueryDto spouse = new(){
			Id = person.Spouse.PersonId,
			Name = person.Spouse.Name,
			Address = person.Spouse.Address,
			IsAlive = person.Spouse.IsAlive,
			BirthDate = person.Spouse.BirthDate
		};
		people.Add(spouse);

		PersonQueryDto father = new(){
			Id = person.Father.PersonId,
			Name = person.Father.Name,
			Address = person.Father.Address,
			IsAlive = person.Father.IsAlive,
			BirthDate = person.Father.BirthDate
		};
		people.Add(father);

		PersonQueryDto mother = new(){
			Id = person.Mother.PersonId,
			Name = person.Mother.Name,
			Address = person.Mother.Address,
			IsAlive = person.Mother.IsAlive,
			BirthDate = person.Mother.BirthDate
		};
		people.Add(mother);

		return people;
	}

}