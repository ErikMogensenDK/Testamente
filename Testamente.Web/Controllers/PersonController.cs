using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testamente.App.Models;
using Testamente.App.Services;
using Testamente.Query;
using Testamente.Web.Identity;

namespace Testamente.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PersonController: ControllerBase
{
    private readonly IPersonService _service;
    private readonly IPersonQuery _query;
    private readonly IdentityContext _context;

    public PersonController(IPersonService service, IPersonQuery query, IdentityContext context)
    {
        _service = service;
        _query = query;
        _context = context;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody]CreatePersonRequest request)
    {
        if (id == Guid.NewGuid())
            return BadRequest();
        await _service.CreateAsync(id, request);
        return Ok();
    }

    [HttpGet("{id}")]
    public ActionResult<PersonQueryDto> Get([FromRoute]Guid id)
    {
        var result = _query.Get(id);
        if(result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult<PersonQueryDto> Update([FromRoute]Guid id, [FromBody]CreatePersonRequest request)
    {
        var result = _service.UpdateAsync(id, request);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult<PersonQueryDto> Delete([FromRoute]Guid id)
    {
        var result = _service.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

}