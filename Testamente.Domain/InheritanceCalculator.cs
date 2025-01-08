





namespace Testamente.Domain;

public class InheritanceCalculator
{
	public InheritanceCalculator()
	{
		
	}

	public Dictionary<Heir, double> CalculateInheritance(double inheritance, List<Heir> heirs)
	{
		if (heirs == null || heirs.Count == 0)
			return null;

		Dictionary<Heir, double> inheritanceDictionary = CreateInitialInheritanceDictionary(heirs);

		int numberOfForcedHeirs = CountNumberOfForcedHeirs(heirs);
		if (numberOfForcedHeirs > 0)
		{
			double ForcedInheritance = 0.25 * inheritance;
			inheritanceDictionary = SplitForcedInheritanceAmongForcedHeirs(ForcedInheritance, inheritanceDictionary, numberOfForcedHeirs);
			inheritance = 0.75 * inheritance;
		}

		int numberOfFreeHeirs = CountNumberOfFreeHeirs(heirs);
		if (numberOfFreeHeirs > 0)
		{
			inheritanceDictionary = SplitFreeInheritanceEvenly(inheritance, inheritanceDictionary, numberOfFreeHeirs);
		}

		return inheritanceDictionary;
	}

    private Dictionary<Heir, double> SplitFreeInheritanceEvenly(double inheritance, Dictionary<Heir, double> inheritanceDictionary, int numberOfFreeHeirs)
    {
		double splitOfInheritance = inheritance/ numberOfFreeHeirs;
		foreach (var heir in inheritanceDictionary.Keys)
		{
			if (heir.IsFreeHeir)
				inheritanceDictionary[heir] += splitOfInheritance;
		}
		return inheritanceDictionary;
    }

    private int CountNumberOfFreeHeirs(List<Heir> heirs)
    {
		int numOfHeirs = 0;
		foreach (var heir in heirs)
		{
			if (heir.IsFreeHeir)
				numOfHeirs ++;
		}
		return numOfHeirs;
    }

    private Dictionary<Heir, double> SplitForcedInheritanceAmongForcedHeirs(double forcedInheritance, Dictionary<Heir, double>inheritanceDictionary, int numberOfForcedHeirs)
    {
		double splitOfInheritance = forcedInheritance / numberOfForcedHeirs;
		foreach (var heir in inheritanceDictionary.Keys)
		{
			if (heir.IsForcedHeir)
				inheritanceDictionary[heir] += splitOfInheritance;
		}
		return inheritanceDictionary;
	}

	private Dictionary<Heir, double> CreateInitialInheritanceDictionary(List<Heir> heirs)
    {
		Dictionary<Heir, double> inheritanceDictionary = new();
		foreach(var heir in heirs)
		{
			inheritanceDictionary[heir] = 0.0;
		}
		return inheritanceDictionary;
    }


    private int CountNumberOfForcedHeirs(List<Heir> heirs)
    {
		int numOfHeirs = 0;
		foreach (var heir in heirs)
		{
			if (heir.IsForcedHeir)
				numOfHeirs ++;
		}
		return numOfHeirs;
    }
}