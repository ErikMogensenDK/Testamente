namespace Testamente.Domain;

public interface ITestatorRepository
{
	Task SaveCreateAsync(Testator testator);
}