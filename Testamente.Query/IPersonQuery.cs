namespace Testamente.Query;

public interface IPersonQuery
{
	PersonQueryDto? Get(Guid id);
	List<PersonQueryDto> GetAllPeopleAssociatedWithUserId(Guid id);
}
