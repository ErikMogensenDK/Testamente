using Testamente.Domain;
using Testamente.App.Models;

namespace Testamente.App.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repo;

    public PersonService(IPersonRepository repo)
    {
        _repo = repo;
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
            IsAlive = request.IsAlive
        };
            //(id, request.Text, request.CreatorEmail);
        await _repo.SaveCreateAsync(person);
    }
}

public interface IPersonService
{
    public Task CreateAsync(Guid id, CreatePersonRequest request);
}