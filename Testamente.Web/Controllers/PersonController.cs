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
    // private readonly IdentityContext _context;
    private readonly IInheritanceCalculator _calculator;

    public PersonController(IPersonService service, IPersonQuery query, IInheritanceCalculator calculator)
    {
        _service = service;
        _query = query;
        // _context = context;
        _calculator = calculator;
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
    [HttpGet("ByCreatedBy/{id}")]
    public async Task<ActionResult<List<PersonQueryDto>>> GetAllAssociatedPeople([FromRoute]Guid id)
    {
        var result = _query.GetAllPeopleAssociatedWithUserId(id);
        return Ok(result);
    }
    [HttpGet("CalculateInheritanceForPersonWithGuid/{id}")]
    public async Task<ActionResult<Dictionary<Person, double>>> CalculateInheritanceForPerson([FromRoute] Guid id)
    {
        Person person = await _service.GetAndAssociateUsersCreatedByAsync(id);
        if (person == null)
            return NotFound();
        Dictionary<Person, double> inheritanceDict = _calculator.CalculateInheritance(1, person);
        Dictionary<Guid, double> inheritanceDictToReturn = MapDictToGuid(inheritanceDict);
        return Ok(inheritanceDictToReturn);
    }

    private Dictionary<Guid, double> MapDictToGuid(Dictionary<Person, double> inheritanceDict)
    {
        Dictionary<Guid, double> d = new();
        foreach (var person in inheritanceDict.Keys)
        {
            d[person.PersonId] = inheritanceDict[person];
        }
        return d;
    }
}