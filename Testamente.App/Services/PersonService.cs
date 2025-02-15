using Testamente.Domain;
using Testamente.App.Models;
using Testamente.Query;

namespace Testamente.App.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repo;
    private readonly IPersonQuery _query;

    public PersonService(IPersonRepository repo, IPersonQuery query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task CreateAsync(Guid id, CreatePersonRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        
        var person = new Person(){
            PersonId = id,
            Name = request.Name,
            BirthDate = request.BirthDate,
            Address = request.Address,
            IsAlive = request.IsAlive,
            Mother = request.MotherId.HasValue ? new() { PersonId = request.MotherId.Value } : null,
            Father = request.FatherId.HasValue ? new() { PersonId = request.FatherId.Value } : null,
            Spouse = request.SpouseId.HasValue ? new() { PersonId = request.SpouseId.Value } : null,
        };
        await _repo.SaveCreateAsync(person, request.CreatedById);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new InvalidCastException(nameof(id));
        await _repo.DeleteAsync(id);
    }

    public async Task UpdateAsync(Guid id, CreatePersonRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var person = new Person(){
            PersonId = id,
            Name = request.Name,
            BirthDate = request.BirthDate,
            Address = request.Address,
            IsAlive = request.IsAlive
        };
        await _repo.SaveUpdateAsync(person);
    }

    public async Task<Person> GetAndAssociateUsersCreatedByAsync(Guid id)
    {
        var dtos = _query.GetAllPeopleAssociatedWithUserId(id);
        List<Person> people = new();
        foreach(var dto in dtos)
        {
            Person person = MapQueryDtoToPerson(dto);
            people.Add(person);
        }
        var testatorDto = dtos.Where(p => p.Id == id).FirstOrDefault();

        people = AssociateRelatedPeople(people, dtos);
        var testator = people.Where(p=>p.PersonId == testatorDto.Id).FirstOrDefault();
        var spouse = people.Where(p=>p.PersonId == testatorDto.SpouseId).FirstOrDefault();
        if (spouse != null && testator != null)
            testator.Spouse = spouse;

        return testator;
    }

    private List<Person> AssociateRelatedPeople(List<Person> people, List<PersonQueryDto> dtos)
    {
        // find "bottom" of node network i.e. any nodes with no child-nodes
        List<PersonQueryDto> parentNodes = new();
        List<PersonQueryDto> childNodes= new();
        PersonQueryDto spouse = new();
        foreach(var node in dtos)
        {
            // bool found = false;
            foreach(var dto in dtos)
            {
                if (node.Id == dto.MotherId || node.Id == dto.FatherId)
                {
                    //this node is actually someones parent!
                    parentNodes.Add(node);
                    //found=true;
                    continue;
                }
            }
            // if (found)
            //     continue;
            if (node.FatherId != null || node.MotherId != null)
                childNodes.Add(node);
        }
        // seems to be an issue with the fact that the testator does not come out like a child node
        foreach (var child in childNodes)
        {
            foreach(var parent in parentNodes)
            {
                if (child.FatherId == parent.Id)
                {
                    people = AddChildToParent(people, child, parent);
                    people = AddFatherToChild(people, child, parent);
                }
                if (child.MotherId == parent.Id)
                {
                    people = AddChildToParent(people, child, parent);
                    people = AddMotherToChild(people, child, parent);
                }
            }
        }
        //add spouse
        return people;
    }

    private List<Person> AddMotherToChild(List<Person> people, PersonQueryDto child, PersonQueryDto parent)
    {
        var mother = people.Where(p => p.PersonId == parent.Id).First();
        var localChild = people.Where(c => c.PersonId == child.Id).First();
        localChild.Mother = mother;
        return people;
    }

    private List<Person> AddFatherToChild(List<Person> people, PersonQueryDto child, PersonQueryDto parent)
    {
        var father = people.Where(p => p.PersonId == parent.Id).First();
        var localChild = people.Where(c => c.PersonId == child.Id).First();
        localChild.Father = father;
        return people;
    }

    private List<Person> AddChildToParent(List<Person> people, PersonQueryDto child, PersonQueryDto parent)
    {
        var personToAddTo = people.Where(p => p.PersonId == parent.Id).First();
        var childToAdd = people.Where(c=> c.PersonId == child.Id).First();
        if (personToAddTo.Children == null )
            personToAddTo.Children = new();
        if(!personToAddTo.Children.Contains(childToAdd))
            personToAddTo.Children.Add(childToAdd);
        return people;
    }

    private Person MapQueryDtoToPerson(PersonQueryDto dto)
    {
        Person person = new()
        {
            PersonId = dto.Id,
            Name = dto.Name,
            Address = dto.Address,
            BirthDate = dto.BirthDate,
            IsAlive = dto.IsAlive
        };

        return person;
    }
}
