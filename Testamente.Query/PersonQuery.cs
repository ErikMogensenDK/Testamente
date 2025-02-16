namespace Testamente.Query;

public class PersonQuery: IPersonQuery
{
	private static readonly int _maxTextChars = 10;
	private readonly IDbConnectionProvider _connProvider;
	private readonly IQueryExecutor _exe;

	public PersonQuery(IDbConnectionProvider connProvider, IQueryExecutor exe)
	{
		_connProvider = connProvider;
		_exe = exe;
	}

	public PersonQueryDto? Get(Guid id)
	{
		var sql = CreateBasicGetSql(id);
		IEnumerable<PersonRowDto> queryResults = new List<PersonRowDto>();
		using (var conn = _connProvider.Get())
		{
			queryResults = _exe.Query<PersonRowDto>(conn, sql);
		}

		var match = queryResults.FirstOrDefault(q => q.Id == id);
		if (match == null)
			return null;

		PersonQueryDto dto = MapQueryDtoToRowDto(match);
		return dto;
	}

    private PersonQueryDto MapQueryDtoToRowDto(PersonRowDto match)
    {
		var dto = new PersonQueryDto
		{
			Id = match.Id,
			Name = match.Name,
			Address = match.Address,
			BirthDate = match.BirthDate,
			IsAlive = match.IsAlive,
			FatherId = match.FatherId,
			MotherId = match.MotherId,
			SpouseId= match.SpouseId
		};
		return dto;
    }

    public List<PersonQueryDto> GetAllPeopleAssociatedWithUserId(Guid id)
	{
		string query = CreateGetSqlForAssociatedUsers(id);

		IEnumerable<PersonRowDto> queryResults = new List<PersonRowDto>();
		using (var conn = _connProvider.Get())
		{
			queryResults = _exe.Query<PersonRowDto>(conn, query);
		}

		if (queryResults == null)
			return null;

		List<PersonQueryDto> listToReturn = new();
		foreach(var match in queryResults)
		{
			var dto = MapQueryDtoToRowDto(match);
			listToReturn.Add(dto);
		}
		return listToReturn;
	}

    private string CreateGetSqlForAssociatedUsers(Guid id)
    {
		return $"select PersonEntityId as id, Name, CAST(BirthDate AS DATE) BirthDate, Address, IsAlive, FatherId, MotherId, SpouseId from People where CreatedById = '{id}'";
    }

    private string CreateBasicGetSql(Guid id)
	{
		return $"select PersonEntityId as id, Name, CAST(BirthDate AS DATE) BirthDate, Address, IsAlive, FatherId, MotherId, SpouseId from People where PersonEntityId = '{id}'";
	}

}

public class PersonRowDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	public DateOnly BirthDate { get; set; }
	public bool IsAlive { get; set; }
	public Guid? FatherId { get; set; }
	public Guid? MotherId { get; set; }
	public Guid? SpouseId { get; set; }
	public Guid? CreatedByUserId { get; set; }
}