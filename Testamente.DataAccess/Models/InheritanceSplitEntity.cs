namespace Testamente.DataAccess;

public class InheritanceSplitEntity
{
	public int InheritanceSplitEntityId { get; set; }
	public int TestatorId { get; set; }
	Dictionary<PersonEntity, double> Inheritants { get; set; }
	public string? Test { get; set; }
}





