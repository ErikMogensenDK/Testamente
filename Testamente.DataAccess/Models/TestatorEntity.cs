namespace Testamente.DataAccess;

public class TestatorEntity : PersonEntity
{
	public PersonEntity SpouseId { get; set; }
	public PersonEntity FatherId { get; set; }
	public PersonEntity MotherId { get; set; }
}
