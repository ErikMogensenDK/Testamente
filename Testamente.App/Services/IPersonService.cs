using Testamente.App.Models;
using Testamente.Domain;

namespace Testamente.App.Services;

public interface IPersonService
{
    public Task CreateAsync(Guid id, CreatePersonRequest request);
    public Task UpdateAsync(Guid id, CreatePersonRequest request);
    public Task DeleteAsync(Guid Id);
    public Task<Person> GetAndAssociateUsersCreatedByAsync(Guid id);
}