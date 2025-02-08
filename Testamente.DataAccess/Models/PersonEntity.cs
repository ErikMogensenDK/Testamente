namespace Testamente.DataAccess;

public class PersonEntity
{
	public Guid PersonEntityId { get; set; }
	public string Name { get; set; }
	public DateOnly BirthDate { get; set; }
	public string Address { get; set; }
	public bool IsAlive { get; set; } = true;

    public Guid? MotherId { get; set; }
    public PersonEntity? Mother { get; set; }

    public Guid? FatherId { get; set; }
    public PersonEntity? Father { get; set; }

    public Guid? SpouseId { get; set; }
    public PersonEntity? Spouse { get; set; }

    public bool IsDeleted { get; set; } = false;
    public Guid UserId { get; set; }
}
