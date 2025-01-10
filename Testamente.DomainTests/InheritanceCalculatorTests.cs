using Testamente.Domain;

namespace Testamente.DomainTests;

[TestClass]
public class InheritanceCalculatorTests
{
	[TestMethod]
	public void Calculate_returnsExpectedResultsWithSingleParentTestatorAndTwoChildren()
	{
		//Arrange
		double expectedShare = 0.5;
		Node Testator = new();
		Node ChildOne = new();
		Node ChildTwo= new();
		Testator.Children = new(){ChildOne, ChildTwo};
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Heir, double> inheritanceDict = calculator.CalculateInheritance(1, TestaTor);
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

    // [TestMethod]
    // public void Calculate_ReturnsExpectedCalculationOnThreeEqualHeirs()
    // {
	// 	//Arrange
	// 	double expectedShare = 1.0/3.0;
	// 	List<Heir> heirs = CreateListOfThreeEqualHeirs();
	// 	InheritanceCalculator calculator = new();
	// 	//Act
	// 	Dictionary<Heir, double> inheritanceDict = calculator.CalculateInheritance(1, heirs);
	// 	List<Heir> actualHeirs = new();
	// 	foreach(var heir in inheritanceDict.Keys)
	// 	{
	// 		Console.WriteLine(heir.Name);
	// 		actualHeirs.Add(heir);
	// 	}
	// 	//Assert
	// 	Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[0]]);
	// 	Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[1]]);
	// 	Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[2]]);
	// }
	// [TestMethod]
	// public void Calculate_returnsExpectedCalculationForTwoUnequalHeirs()
	// {
	// 	//Arrange
	// 	double expectedShareOne = 0.125;
	// 	double expectedShareTwo = 0.875;
	// 	List<Heir> heirs = CreateListOfUnequalHeirs();
	// 	InheritanceCalculator calculator = new();
	// 	//Act
	// 	Dictionary<Heir, double> inheritanceDict = calculator.CalculateInheritance(1, heirs);
	// 	List<Heir> actualHeirs = new();
	// 	foreach(var heir in inheritanceDict.Keys)
	// 	{
	// 		Console.WriteLine(heir.Name);
	// 		actualHeirs.Add(heir);
	// 	}
	// 	//Assert
	// 	Assert.AreEqual(expectedShareOne, inheritanceDict[actualHeirs[0]]);
	// 	Assert.AreEqual(expectedShareTwo, inheritanceDict[actualHeirs[1]]);
	// }

	// [TestMethod]
	// public void Calculate_SplitsInheritanceBetweenSecondClassHeirsIfNoFirstClassHeirs()
	// {
	// 	//Arrange
	// 	double expectedShareForOneAndTwo = 0.5;
	// 	Heir heirOne = new() { Name = "HeirOne", Address = "SOmeAdress", Id = 1, IsForcedHeir = true, IsFreeHeir = true, TypeOfHeir = RelationType.FirstClass };
	// 	Heir heirTwo = new() { Name = "HeirTwo", Address = "SomeOtherAdress", Id = 2, IsForcedHeir = true, IsFreeHeir = true, TypeOfHeir = RelationType.FirstClass };
	// 	Heir heirThree = new() { Name = "HeirThree", Address = "SomeThirdAdress", Id = 3, IsForcedHeir = false, IsFreeHeir = true, TypeOfHeir = RelationType.SecondClass};
	// 	List<Heir> heirs = new() { heirOne, heirTwo, heirThree };
	// 	InheritanceCalculator calculator = new();

	// 	//Act
	// 	Dictionary<Heir, double> inheritanceDict = calculator.CalculateInheritance(1, heirs);
	// 	List<Heir> actualHeirs = new();
	// 	foreach(var heir in inheritanceDict.Keys)
	// 	{
	// 		Console.WriteLine(heir.Name);
	// 		actualHeirs.Add(heir);
	// 	}

	// 	//Assert
	// 	Assert.AreEqual(expectedShareForOneAndTwo, inheritanceDict[actualHeirs[0]]);
	// 	Assert.AreEqual(expectedShareForOneAndTwo, inheritanceDict[actualHeirs[1]]);
	// 	Assert.AreEqual(0.0, inheritanceDict[actualHeirs[2]]);
	// }

    // private List<Heir> CreateListOfUnequalHeirs()
    // {
	// 	Heir heirOne = new(){Name="HeirOne", Address="SOmeAdress", Id=1, IsForcedHeir=true, IsFreeHeir=false};
	// 	Heir heirTwo = new() { Name = "HeirTwo", Address = "SomeOtherAdress", Id = 2, IsForcedHeir = true, IsFreeHeir = true};
	// 	List<Heir> heirs = new() { heirOne, heirTwo};
	// 	return heirs;
    // }

    // private List<Heir> CreateListOfThreeEqualHeirs()
    // {
	// 	Heir heirOne = new(){Name="HeirOne", Address="SOmeAdress", Id=1, IsForcedHeir=true, IsFreeHeir=true};
	// 	Heir heirTwo = new() { Name = "HeirTwo", Address = "SomeOtherAdress", Id = 2, IsForcedHeir = true, IsFreeHeir = true };
	// 	Heir heirThree = new() { Name = "HeirThree", Address = "SomeThirdAdress", Id = 3, IsForcedHeir = true, IsFreeHeir = true };
	// 	List<Heir> heirs = new() { heirOne, heirTwo, heirThree };
	// 	return heirs;
	// }
}
