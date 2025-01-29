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
        await _repo.SaveCreateAsync(person);
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
}

public interface IPersonService
{
    public Task CreateAsync(Guid id, CreatePersonRequest request);
    public Task UpdateAsync(Guid id, CreatePersonRequest request);
    public Task DeleteAsync(Guid Id);
}