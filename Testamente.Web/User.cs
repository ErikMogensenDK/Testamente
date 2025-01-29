using Microsoft.AspNetCore.Identity;

namespace Testamente.Web;

public class User: IdentityUser
{
	public string? Initials {get;set;}
}