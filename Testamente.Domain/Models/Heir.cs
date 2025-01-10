namespace Testamente.Domain;

public class Heir
{
	public int HeirId { get; set; }
	public int TestatorId {get;set;}
	public string Name { get; set; }
	public DateOnly BirthDate { get; set; }
	public string Address { get; set; }
	public double ShareOfInheritance { get; set; }
	//public bool IsForcedHeir { get; set; }
	//public bool IsFreeHeir { get; set; }
}

public class Inheritance {
	public double Value {get;set;}
}



public class Node
{
	public int NodeId {get;set;}
	public int HeirId { get; set; }
	public List<Node> Children { get; set; }
}