using Microsoft.AspNetCore.Mvc;
using Testamente.App.Models;
using Testamente.App.Services;
using Testamente.Query;

namespace Testamente.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController: ControllerBase
{
    private readonly IPersonService _service;
    private readonly IPersonQuery _query;

    public PersonController(IPersonService service, IPersonQuery query)
    {
        _service = service;
        _query = query;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody]CreatePersonRequest request)
    {
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
}