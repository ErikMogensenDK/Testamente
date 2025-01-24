using Testamente.Domain;

namespace Testamente.DataAccess;

public class PersonRepository: IPersonRepository
{
	private readonly TestamenteContext _context;

	public PersonRepository(TestamenteContext context)
	{
		_context = context;
	}

    public async Task SaveCreateAsync(Person person)
    {
		if (person == null)
		{
			throw new ArgumentNullException(nameof(person));
		}

		var dbEntity = new PersonEntity
		{
			PersonEntityId = person.PersonId,
			Name = person.Name,
			BirthDate = person.BirthDate,
			Address = person.Address,
			IsAlive = person.IsAlive,
			MotherId = person.Mother?.PersonId,
			FatherId = person.Father?.PersonId,
			SpouseId = person.Spouse?.PersonId
		};
		_context.People.Add(dbEntity);
		
		await _context.SaveChangesAsync();
    }
}