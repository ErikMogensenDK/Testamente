using Testamente.App.Models;

namespace Testamente.App.Services;

public interface IPersonService
{
    public Task CreateAsync(Guid id, CreatePersonRequest request);
    public Task UpdateAsync(Guid id, CreatePersonRequest request);
    public Task DeleteAsync(Guid Id);
}