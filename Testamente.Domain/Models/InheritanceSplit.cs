namespace Testamente.Domain;

public class InheritanceSplit
{
	public int InheritanceSplitId { get; set; }
	public int TestatorId { get; set; }
	public Dictionary<Person, double> Inheritants { get; set; }
}
