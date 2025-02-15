using Testamente.Domain;
namespace Testamente.App;

public class InheritanceCalculator
{
	public InheritanceCalculator()
	{
	}

	public Dictionary<Person, double> CalculateInheritance(double inheritance, Person testator)
	{
		Dictionary<Person, double> inheritanceDict = new();
		if (testator == null)
			return null;

		// cases with first class heirs
		inheritanceDict = SplitAmongFirstClassInheritantsIfAny(inheritance, testator);
		if (inheritanceDict.Count>0)
			return inheritanceDict;

		// 2nd inheritance class

		// TwoParents Alive
		if (!IsNullOrDead(testator.Father) && !IsNullOrDead(testator.Mother))
		{
			inheritanceDict[testator.Father] = inheritance/2;
			inheritanceDict[testator.Mother] = inheritance/2;
			return inheritanceDict;
		}

		// Only Father Alive, no siblings
		if (!IsNullOrDead(testator.Father) && IsNullOrEmpty(testator.Father?.Children) && IsNullOrDead(testator.Mother) && IsNullOrEmpty(testator.Mother?.Children)) 
		{
			inheritanceDict[testator.Father] = inheritance;
			return inheritanceDict;
		}

		// Only Mother Alive, no siblings
		if (IsNullOrDead(testator.Father) && !IsNullOrDead(testator.Mother) && IsNullOrEmpty(testator.Father?.Children) && IsNullOrEmpty(testator.Mother?.Children))
		{
			inheritanceDict[testator.Mother] = inheritance;
			return inheritanceDict;
		}

		List<Person> siblingsAndHalfSiblings = GetSiblingsAndHalfSiblingsIfAny(testator);
		// Only Father alive, with siblings
		if (!IsNullOrDead(testator.Father) && IsNullOrDead(testator.Mother) && siblingsAndHalfSiblings.Count > 0)
		{
			inheritanceDict[testator.Father] = inheritance / 2;
			int numOfSiblings = siblingsAndHalfSiblings.Count;
			foreach (var sibling in siblingsAndHalfSiblings)
			{
				inheritanceDict[sibling] = (inheritance / 2) / numOfSiblings;
			}
			return inheritanceDict;
		}

		// Only Mother alive, with siblings
		if (IsNullOrDead(testator.Father) && !IsNullOrDead(testator.Mother) && siblingsAndHalfSiblings.Count > 0)
		{
			inheritanceDict[testator.Mother] = inheritance / 2;
			int numOfSiblings = siblingsAndHalfSiblings.Count;
			foreach (var sibling in siblingsAndHalfSiblings)
			{
				inheritanceDict[sibling] = (inheritance / 2) / numOfSiblings;
			}
			return inheritanceDict;
		}

		// No parents but siblings
		if (IsNullOrDead(testator.Father) && IsNullOrDead(testator.Mother) && siblingsAndHalfSiblings.Count > 0)
		{
			int numOfSiblings = siblingsAndHalfSiblings.Count;
			foreach (var sibling in siblingsAndHalfSiblings)
			{
				inheritanceDict[sibling] = inheritance / numOfSiblings;
			}
			return inheritanceDict;
		}
		// No SecondClassInheritants!
		// Check if thirdClassInheritants
		var grandParents = GetGrandparentsIfAny(testator);
		if(!IsNullOrEmpty(grandParents))
		{
			// Add any grandparents to lists
			List<Person> inheritingGrandparents= grandParents.Where(x => x.IsAlive).ToList();
			List<Person> deadGrandparents = grandParents.Where(x => !x.IsAlive).ToList();
			int numOfDeadGrandparentsWithoutChildren = 0;
			//Count number of dead grandparents without living children (these nephews/nieces should NOT inherit)
			for(int i = 0; i<deadGrandparents.Count; i++)
			{
				if(IsNullOrEmpty(deadGrandparents[i].Children))
				{
					continue;
				}
				inheritingGrandparents.Add(deadGrandparents[i]);
			}
			double inheritanceSplit = inheritance / inheritingGrandparents.Count;
			foreach(var grandparent in inheritingGrandparents)
			{
				if (!IsNullOrDead(grandparent))
				{
					inheritanceDict[grandparent] = inheritanceSplit;
				}
				else
				{
					int numOfChildren = grandparent.Children.Count;
					foreach(var child in grandparent.Children)
					{
						inheritanceDict[child] = inheritanceSplit/numOfChildren;
					}
				}
			}
			return inheritanceDict;
		}

		//If no living relatives, inheritance goes to the state
		Person person = new(){Name = "Den Danske Stat"};
		inheritanceDict[person] = inheritance;
		return inheritanceDict;
	}

    private List<Person> GetGrandparentsIfAny(Person testator)
    {
		List<Person> grandParents = new();
		if (testator.Father?.Father != null)
			grandParents.Add(testator.Father.Father);
		if (testator.Father?.Mother != null)
			grandParents.Add(testator.Father.Mother);
		if (testator.Mother?.Father != null)
			grandParents.Add(testator.Mother.Father);
		if (testator.Mother?.Mother != null)
			grandParents.Add(testator.Mother.Mother);
		if (grandParents.Count == 0)
			return null;
		else
			return grandParents;
    }

    private Dictionary<Person, double> SplitAmongFirstClassInheritantsIfAny(double inheritance, Person testator)
    {
		Dictionary<Person,double> inheritanceDict = new();
		// No living spouse, but children
		if (IsNullOrDead(testator.Spouse) && !IsNullOrEmpty(testator.Children))
		{
			int numOfChildren = testator.Children.Count;
			foreach (var child in testator.Children)
			{
				double inheritanceToAssign= inheritance/numOfChildren;
				inheritanceDict = AssignInheritanceForAnyLivingDescendants(inheritanceDict, child, inheritanceToAssign, testator);
			}
			return inheritanceDict;
		}

		// Spouse and Children
		if (!IsNullOrDead(testator.Spouse) && !IsNullOrEmpty(testator.Children)) 
		{
			int numOfChildren = testator.Children.Count;
			inheritanceDict[testator.Spouse] = inheritance/2;
			foreach (var child in testator.Children)
			{
				inheritanceDict[child] = (inheritance / 2) / numOfChildren;
			}
			return inheritanceDict;
		}

		//Spouse and No Children
		if (!IsNullOrDead(testator.Spouse) && IsNullOrEmpty(testator.Children)) 
		{
			inheritanceDict[testator.Spouse] = inheritance;
			return inheritanceDict;
		}
		return new Dictionary<Person, double>();
	}

    private List<Person> GetSiblingsAndHalfSiblingsIfAny(Person testator)
    {
		List<Person> siblings = new();
		if (testator.Father != null && testator.Father.Children?.Count > 0)
		{
			foreach (var child in testator.Father.Children)
			{
				if (child.PersonId != testator.PersonId)
				{
					siblings.Add(child);
				}
			}
		}
		if (testator.Mother != null && testator.Mother.Children?.Count > 0)
		{
			foreach (var child in testator.Mother.Children)
			{
				if (child.PersonId != testator.PersonId)
				{
					siblings.Add(child);
				}
			}
		}
		return siblings;;
    }

    private Dictionary<Person, double> AssignInheritanceForAnyLivingDescendants(Dictionary<Person, double> inheritanceDict, Person heir, double inheritanceToAssign, Person testator)
    {
		if (heir.IsAlive == true && heir.PersonId != testator.PersonId)
		{
			inheritanceDict[heir] = inheritanceToAssign;
			return inheritanceDict;
		}
		else if (heir.Children != null && heir.Children.Count > 0)
		{
			List<Person> livingChildren = heir.Children.Where(x=> x.IsAlive).ToList();
			int numOfChildren = livingChildren.Count;
			double updatedInheritanceShare= inheritanceToAssign/numOfChildren;
			foreach(var child in heir.Children)
			{
				AssignInheritanceForAnyLivingDescendants(inheritanceDict, child, updatedInheritanceShare, testator);
			}
		}
		return inheritanceDict;
    }
	private bool IsNullOrDead(Person person)
	{
		if (person == null || person.IsAlive == false)
			return true;
		else
			return false;
	}
	private bool IsNullOrEmpty(List<Person> people)
	{
		if (people == null || people.Count < 1)
			return true;
		else
			return false;
	}

    public Dictionary<Person, double> CalculateForcedInheritance(double inheritance, Person testator)
    {
		return SplitAmongFirstClassInheritantsIfAny(0.25, testator);
    }
}