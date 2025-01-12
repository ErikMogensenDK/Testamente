namespace Testamente.DataAccess;

public class TestatorEntity : PersonEntity
{
	public PersonEntity Spouse { get; set; }
	public PersonEntity Father { get; set; }
	public PersonEntity Mother { get; set; }
	public List<PersonEntity> Grandparents { get; set; }
}
