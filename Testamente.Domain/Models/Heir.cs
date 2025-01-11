namespace Testamente.Domain;

public class Person 
{
	public int PersonId { get; set; }
	public string Name { get; set; }
	public DateOnly BirthDate { get; set; }
	public string Address { get; set; }
	public List<Person> Children { get; set; }
	public bool IsAlive {get;set;} = true;
	//public bool IsForcedHeir { get; set; }
	//public bool IsFreeHeir { get; set; }
}

public class Inheritance {
	public double Value {get;set;}
}
public class Testator: Person
{
	public Person Spouse { get; set; }
	public Person Father { get; set; }
	public Person Mother { get; set; }
	public Person MothersFather { get; set; }
	public Person MothersMother{ get; set; }
	public Person FathersFather { get; set; }
	public Person FathersMother { get; set; }
}

public enum TypeOfRelative
{
	Father,
	Mother, 
	Spouse,
	Sibling,
	Child, 
	Nibling,
	GrandFather,
	GrandMother
}