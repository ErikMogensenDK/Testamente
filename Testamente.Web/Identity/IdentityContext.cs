using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Testamente.Web;
namespace Testamente.Web.Identity;

public class IdentityContext: IdentityDbContext<User>
{
	public IdentityContext(DbContextOptions<IdentityContext> options) 
	: base(options)
	{
		
	}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

		builder.HasDefaultSchema("identity");
    }

}