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
		Testator testator = new(){PersonId=1};
		Person ChildOne = new(){PersonId = 2,Name ="NameOne"};
		Person ChildTwo= new(){PersonId= 3,Name="NameTwo"};
		testator.Children = new(){ChildOne, ChildTwo};
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[1]]);
	}
	[TestMethod]
	public void Calculate_returnsExpectedResultsWithTestatorWithSpouseAndTwoChildren()
	{
		//Arrange
		double expectedShareForChildren = 0.25;
		double expectedShareForSpouse= 0.50;
		Testator testator = new();
		Person ChildOne = new(){Name ="NameOne"};
		Person ChildTwo= new(){Name="NameTwo"};
		Person spouse = new(){Name ="NameOfSpouse"};
		testator.Children = new(){ChildOne, ChildTwo};
		testator.Spouse= spouse;
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShareForSpouse, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(expectedShareForChildren, inheritanceDict[actualHeirs[1]]);
		Assert.AreEqual(expectedShareForChildren, inheritanceDict[actualHeirs[2]]);
	}

	[TestMethod]
	public void Calculate_returnsExpectedResultsWithTestatorWithSpouseAndNoChildren()
	{
		//Arrange
		double expectedShareForSpouse= 1;
		Testator testator = new();
		Person spouse = new(){Name ="NameOfSpouse"};
		testator.Spouse= spouse;
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShareForSpouse, inheritanceDict[actualHeirs[0]]);
	}

	[TestMethod]
	public void Calculate_returnsExpectedResultsWithNoFirstClassHeirsBut2Parents()
	{
		//Arrange
		double expectedShare = 0.5;
		Testator testator = new();
		Person father = new(){Name ="NameOfFather"};
		Person mother = new(){Name="NameOfMother"};
		testator.Father = father;
		testator.Mother = mother;
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(father.Name, actualHeirs[0].Name);
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[1]]);
		Assert.AreEqual(mother.Name, actualHeirs[1].Name);
	}
	[TestMethod]
	public void Calculate_returnsExpectedResultsWithNoFirstClassHeirsButOnlyFather()
	{
		//Arrange
		double expectedShare = 1;
		Testator testator = new() { PersonId = 1 };
		Person father = new(){Name ="NameOfFather", PersonId=2};
		testator.Father = father;
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(father.Name, actualHeirs[0].Name);
	}
	[TestMethod]
	public void Calculate_returnsExpectedResultsWithNoFirstClassHeirsButOnlyMother()
	{
		//Arrange
		double expectedShare = 1;
		Testator testator = new();
		Person mother = new(){Name ="NameOfMother"};
		testator.Mother= mother;
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(mother.Name, actualHeirs[0].Name);
	}
	[TestMethod]
	public void Calculate_returnsExpectedResultWithNoFirstClassHeirsFatherAnd2Siblings()
	{
		//Arrange
		double expectedShareFather = 0.5;
		double expectedShareSibling = 0.25;
		Testator testator = new(){PersonId=1};
		Person father = new(){PersonId=2,Name ="NameOfFather"};
		testator.Father = father;
		Person brother = new() { PersonId = 3, Name = "NameOfBrother" };
		Person sister = new() { PersonId = 4, Name = "NameOfSister" };
		//testator.Siblings = new(){brother,sister};
		father.Children=new(){brother,sister};
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShareFather, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(father.Name, actualHeirs[0].Name);
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[1]]);
		Assert.AreEqual(brother.Name, actualHeirs[1].Name);
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[2]]);
		Assert.AreEqual(sister.Name, actualHeirs[2].Name);
	}
	[TestMethod]
	public void Calculate_returnsExpectedResultWithNoFirstClassHeirsMotherAnd1Siblings()
	{
		//Arrange
		double expectedShare= 0.5;
		Testator testator = new(){PersonId=1};
		Person mother = new(){PersonId=2, Name ="NameOfMother"};
		testator.Mother= mother;
		Person sister = new() { PersonId = 3, Name = "NameOfSister" };
		mother.Children=new(){sister};
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(mother.Name, actualHeirs[0].Name);
		Assert.AreEqual(expectedShare, inheritanceDict[actualHeirs[1]]);
		Assert.AreEqual(sister.Name, actualHeirs[1].Name);
	}
	[TestMethod]
	public void Calculate_returnsExpectedResultWithNoFirstClassHeirsNoParentsAndSiblings()
	{
		//Arrange
		double expectedShareSibling = 0.25;
		Testator testator = new() { PersonId = 1 };
		Person brother = new() { PersonId = 2, Name = "NameOfBrother" };
		Person sister = new() { PersonId = 3, Name = "NameOfSister" };
		Person secondBrother = new() { PersonId = 4, Name = "NameOfsecondBrother" };
		Person secondSister = new() { PersonId = 5, Name = "NameOfsecondSister" };
		Person father = new() { PersonId = 6, IsAlive = false, Children = new(){ testator, brother, sister, secondBrother, secondSister } };
		testator.Father = father;
		//testator.Siblings = new(){brother,sister,secondBrother,secondSister};
		InheritanceCalculator calculator = new();

		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(brother.Name, actualHeirs[0].Name);
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[1]]);
		Assert.AreEqual(sister.Name, actualHeirs[1].Name);
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[2]]);
		Assert.AreEqual(secondBrother.Name, actualHeirs[2].Name);
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[3]]);
		Assert.AreEqual(secondSister.Name, actualHeirs[3].Name);
	}

	[TestMethod]
	public void Calcualte_returnsExpectedResultWithNoFirstClassHeirsNoParentsAndSiblings()
	{
		//Arrange
		double expectedShareSibling = 0.25;
		Testator testator = new(){PersonId=1};
		Person brother = new(){Name ="NameOfBrother", PersonId=2};
		Person sister= new(){Name ="NameOfSister", PersonId=3};
		Person secondBrother = new(){Name ="NameOfsecondBrother", PersonId=4};
		Person secondSister= new(){Name ="NameOfsecondSister", PersonId=5};
		Person father = new() { PersonId = 6, IsAlive = false, Children = new() { brother, sister, secondBrother, secondSister } };
		testator.Father=father;
		//testator.Siblings = new(){brother,sister,secondBrother,secondSister};
		InheritanceCalculator calculator = new();

		//Act
		Dictionary<Person , double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in inheritanceDict.Keys)
		{
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(brother.Name, actualHeirs[0].Name);
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[1]]);
		Assert.AreEqual(sister.Name, actualHeirs[1].Name);
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[2]]);
		Assert.AreEqual(secondBrother.Name, actualHeirs[2].Name);
		Assert.AreEqual(expectedShareSibling, inheritanceDict[actualHeirs[3]]);
		Assert.AreEqual(secondSister.Name, actualHeirs[3].Name);
	}
	[TestMethod]
	public void Calculate_returnsExpectedResultWithNoHeirs()
	{
		//Arrange
		double expectedShare = 1;
		Testator testator = new();
		InheritanceCalculator calculator = new();

		//Act
		var actualHeirsDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach(var heir in actualHeirsDict.Keys)
		{
			actualHeirs.Add(heir);
		}

		//Assert
		Assert.IsTrue(actualHeirs.Count == 1);
		Assert.AreEqual(actualHeirs[0].Name, "Den Danske Stat");
	}

	[TestMethod]
	public void Calculate_returnsExpectedResultsWithOneAliveChildAndTwoChildrenOfADeceasedHeir()
	{
		//Arrange
		double expectedShareAdult = 0.5;
		double expectedShareChildren = 0.25;
		Testator testator = new() { PersonId = 1 };
		Person childOne = new() { PersonId = 2, Name = "NameOne" };
		Person deceasedChild = new() { PersonId = 3, Name = "NameTwo", IsAlive = false };
		deceasedChild.Children = new() { new() { PersonId = 4, Name = "GrandChildOne" }, new() { PersonId = 5, Name = "GrandChildTwo" } };
		testator.Children = new() { childOne, deceasedChild };
		InheritanceCalculator calculator = new();
		//Act
		Dictionary<Person, double> inheritanceDict = calculator.CalculateInheritance(1, testator);
		List<Person> actualHeirs = new();
		foreach (var heir in inheritanceDict.Keys)
		{
			Console.WriteLine(heir.Name);
			actualHeirs.Add(heir);
		}
		//Assert
		Assert.AreEqual(3, actualHeirs.Count);
		Assert.AreEqual(expectedShareAdult, inheritanceDict[actualHeirs[0]]);
		Assert.AreEqual(childOne.Name, actualHeirs[0].Name);
		Assert.AreEqual(expectedShareChildren, inheritanceDict[actualHeirs[1]]);
		Assert.AreEqual(deceasedChild.Children[0].Name, actualHeirs[1].Name);
		Assert.AreEqual(expectedShareChildren, inheritanceDict[actualHeirs[2]]);
		Assert.AreEqual(deceasedChild.Children[1].Name, actualHeirs[2].Name);
	}
	//Nevøer/niecer?
	//Bedsteforældre
	// Er det bedre med liste af "relatives?", og en relative-enum?
}
