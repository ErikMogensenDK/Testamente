namespace Testamente.Domain;

public class Person
{
	public int PersonId { get; set; }
	public string Name { get; set; }
	public DateOnly BirthDate { get; set; }
	public string Address { get; set; }
	public List<Person> Children { get; set; }
	public bool IsAlive { get; set; } = true;
}
