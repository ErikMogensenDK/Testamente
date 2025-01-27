using Microsoft.EntityFrameworkCore;
using Testamente.Domain;

namespace Testamente.DataAccess;

public class PersonRepository: IPersonRepository
{
	private readonly TestamenteContext _context;

	public PersonRepository(TestamenteContext context)
	{
		_context = context;
	}

    public async Task DeleteAsync(Guid id)
    {
		var entity = _context.People.SingleOrDefault(e => e.PersonEntityId == id);
		if (entity != null)
		{
			_context.People.Remove(entity);
			await _context.SaveChangesAsync();
		}
		else
			throw new KeyNotFoundException("Could not locate id to delete in db");
    }

    public async Task SaveCreateAsync(Person person)
    {
		if (person == null)
			throw new ArgumentNullException(nameof(person));
		var dbEntity = MapObjectToEntity(person);
		_context.People.Add(dbEntity);
		
		await _context.SaveChangesAsync();
    }

    public async Task SaveUpdateAsync(Person person)
    {
		if (person == null)
			throw new ArgumentNullException(nameof(person));

		var entity = _context.People.SingleOrDefault(e => e.PersonEntityId == person.PersonId);
		if (entity != null)
		{
			var dbEntity = MapObjectToEntity(person);
			_context.People.Update(dbEntity);

			await _context.SaveChangesAsync();
		}
		else
			throw new KeyNotFoundException("Could not locate id to update in db");
    }

	private PersonEntity MapObjectToEntity(Person person)
	{
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
		return dbEntity;
	}
}