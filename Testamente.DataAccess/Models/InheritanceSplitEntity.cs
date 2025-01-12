namespace Testamente.DataAccess;

public class InheritanceSplitEntity
{
	public int InheritanceSplitEntityId { get; set; }
	public int TestatorId { get; set; }
	public Dictionary<PersonEntity, double> Inheritants { get; set; }
}
