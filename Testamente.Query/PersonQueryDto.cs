namespace Testamente.Query;

public class PersonQueryDto
{
	public Guid Id {get;set;}
	public string Name { get; set; }
	public string Address { get; set; }
	public DateOnly BirthDate { get; set; }
	public bool IsAlive { get; set; }
	public Guid? FatherId { get; set; }
	public Guid? MotherId { get; set; }
	public Guid? SpouseId { get; set; }
}