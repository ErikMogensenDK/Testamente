namespace Testamente.DataAccess;

public class PersonEntity
{
	public int PersonEntityId { get; set; }
	public string Name { get; set; }
	public DateOnly BirthDate { get; set; }
	public string Address { get; set; }
	public bool IsAlive { get; set; } = true;
	public int? MotherId { get; set; }
	public int? FatherId { get; set; }
	public int? SpouseId { get; set; }
	//public List<PersonEntity> Children { get; set; }
}
