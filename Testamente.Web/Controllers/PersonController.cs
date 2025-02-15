using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testamente.App;
using Testamente.App.Models;
using Testamente.App.Services;
using Testamente.Domain;
using Testamente.Query;
using Testamente.Web.Identity;

namespace Testamente.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class PersonController: ControllerBase
{
    private readonly IPersonService _service;
    private readonly IPersonQuery _query;
    private readonly IdentityContext _context;
    private readonly InheritanceCalculator _calculator;

    public PersonController(IPersonService service, IPersonQuery query, IdentityContext context, InheritanceCalculator calculator)
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
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]CreatePersonRequest request)
    {
        await _service.UpdateAsync(id, request);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
    [HttpGet("/ByCreatedBy/{UserId}")]
    public async Task<ActionResult<List<PersonQueryDto>>> GetAllAssociatedPeople([FromRoute]Guid id)
    {
        var result = _query.GetAllPeopleAssociatedWithUserId(id);
        return Ok(result);
    }
    [HttpGet("CalculateInheritanceForPersonWithGuid")]
    public async Task<ActionResult<Dictionary<Person, double>>> CalculateInheritanceForPerson(Guid id)
    {
        Person person = await _service.GetAndAssociateUsersCreatedByAsync(id);
        Dictionary<Person, double> inheritanceDict = _calculator.CalculateInheritance(1, person);
        return Ok(inheritanceDict);
    }
}