namespace Testamente.Domain;

public interface ICalculateableRule 
{
	Dictionary<Heir, double> CalculateInheritance(double inheritance, List<Heir> heirs);
}


public class Heir
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	public bool IsForcedHeir { get; set; }
	public bool IsFreeHeir { get; set; }
}