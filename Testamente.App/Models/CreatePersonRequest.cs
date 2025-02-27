namespace Testamente.App.Models;

public class CreatePersonRequest
{
	public string Name { get; set; }
	public string Address { get; set; }
	public bool IsAlive { get; set; }
	public DateOnly BirthDate { get; set; }
    public Guid? MotherId { get; set; }
    public Guid? FatherId { get; set; }
    public Guid? SpouseId { get; set; }
	public Guid CreatedById { get; set; }
}
