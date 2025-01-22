using Testamente.Domain;
namespace Testamente.App;

public interface IInheritanceCalculator
{
	public Dictionary<Person, double> CalculateInheritance(double inheritance, Person testator);
    public Dictionary<Person, double> CalculateForcedInheritance(double inheritance, Person testator);
}