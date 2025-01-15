using Testamente.Domain;
namespace Testamente.App;

public interface IInheritanceCalculator
{
	public Dictionary<Person, double> CalculateInheritance(double inheritance, Testator testator);
    public Dictionary<Person, double> CalculateForcedInheritance(double inheritance, Testator testator);
}