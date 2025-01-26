namespace Testamente.Domain;

public interface IPersonRepository
{
	Task SaveCreateAsync(Person person);
	Task SaveUpdateAsync(Person person);
}