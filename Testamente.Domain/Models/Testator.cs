namespace Testamente.Domain;

public class Testator : Person
{
	public Person Spouse { get; set; }
	public Person Father { get; set; }
	public Person Mother { get; set; }
	public List<Person> Grandparents { get; set; }
}
