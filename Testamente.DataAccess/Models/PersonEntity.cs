namespace Testamente.DataAccess;

public class PersonEntity
{
	public int PersonEntityId { get; set; }
	public string Name { get; set; }
	public DateOnly BirthDate { get; set; }
	public string Address { get; set; }
	public bool IsAlive { get; set; } = true;

    public int? MotherId { get; set; }
    public PersonEntity? Mother { get; set; }

    public int? FatherId { get; set; }
    public PersonEntity? Father { get; set; }

    public int? SpouseId { get; set; }
    public PersonEntity? Spouse { get; set; }
}
