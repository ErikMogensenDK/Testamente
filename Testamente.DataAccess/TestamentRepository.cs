namespace Testamente.DataAccess;

using System.Threading.Tasks;
using Testamente.Domain;

public class TestamentRepo : IInheritanceSplitRepository, IPersonRepository, IReportSectionRepository, ITestatorRepository
{
	private readonly TestamentContext _context;

    public Task SaveCreateAsync(InheritanceSplit split)
    {
        if(split == null)
            throw new ArgumentNullException(nameof(split));

        List<PersonEntity> inheritantsDtos = new ();
        foreach (var inheritant in split.Inheritants.Keys)
        {
            inheritantsDtos.Add(new(){
                PersonDtoId = inheritant.PersonId,
                Name = inheritant.Name,
                BirthDate = inheritant.BirthDate,
                Address = inheritant.Address,
                Children = new ()

	public List<PersonEntity> Children { get; set; }
	public bool IsAlive { get; set; } = true;
            })
        }
        var dbEntity = new InheritanceSplitDto
        {
            InheritanceSplitId = split.InheritanceSplitId,
            Inheritants = split.Inheritants,

        }
    }

    public Task SaveCreateAsync(Person person)
    {
        throw new NotImplementedException();
    }

    public Task SaveCreateAsync(ReportSection section)
    {
        throw new NotImplementedException();
    }

    public Task SaveCreateAsync(Testator testator)
    {
        throw new NotImplementedException();
    }
}
