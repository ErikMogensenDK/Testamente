using Testamente.Domain;

namespace Testamente.DomainTests;

[TestClass]
public class InheritanceCalculatorTests
{
    [TestMethod]
    public void Calculate_ReturnsExpectedCalculationOnThreeEqualHeirs()
    {
		//Arrange
		double expectedShare = 1.0/3.0;
		List<Heir> heirs = CreateListOfHeirs();
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Heir, double> inheritanceDict = calculator.CalculateInheritance(1, heirs);
		List<Heir> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			Console.WriteLine(heir.Name);
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[1]]);
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[2]]);
	}

    private List<Heir> CreateListOfHeirs()
    {
		Heir heirOne = new(){Name="HeirOne", Address="SOmeAdress", Id=1, IsForcedHeir=true, IsFreeHeir=true};
		Heir heirTwo = new() { Name = "HeirTwo", Address = "SomeOtherAdress", Id = 2, IsForcedHeir = true, IsFreeHeir = true };
		Heir heirThree = new() { Name = "HeirThree", Address = "SomeThirdAdress", Id = 3, IsForcedHeir = true, IsFreeHeir = true };
		List<Heir> heirs = new() { heirOne, heirTwo, heirThree };
		return heirs;
	}
}
