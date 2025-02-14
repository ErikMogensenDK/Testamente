namespace Testamente.Domain;

public class Person
{
	public Guid PersonId { get; set; }
	public string Name { get; set; }
	public DateOnly BirthDate { get; set; }
	public string Address { get; set; }
	public bool IsAlive { get; set; } = true;
	public List<Person> Children { get; set; }
	public Person Spouse { get; set; }
	public Person Father { get; set; }
	public Person Mother { get; set; }

	public Person()
	{
	}
	public Person(Guid id, string name, DateOnly birthDate, string address, bool isAlive = true, List<Person> children = null, Person spouse = null, Person father = null, Person mother = null)
	{
		PersonId = id;
		Name = name;
		BirthDate = birthDate;
		Address = address;
		IsAlive = isAlive;
		Children = children;
		Spouse = spouse;
		Father = father;
		Mother = mother;
	}
}
