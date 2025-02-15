namespace Testamente.Domain;

public interface IPersonRepository
{
	Task SaveCreateAsync(Person person, Guid createdById);
	Task SaveUpdateAsync(Person person);
	Task DeleteAsync(Guid id);
}