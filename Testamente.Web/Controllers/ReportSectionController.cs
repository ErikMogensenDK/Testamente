// using Microsoft.AspNetCore.Mvc;
// using Testamente.App;
// using Testamente.Query;

// namespace Testamente.Web.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class ReportSectionController: ControllerBase
// {
//     private readonly IReportSectionPostService _service;
//     //private readonly IReportSectionQuery _query;

//     public ReportSectionController(IPersonPostService service)//, IPersonQuery query)
//     {
//         _service = service;
//         //_query = query;
//     }

//     [HttpPost("{id}")]
//     public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody]CreateReportSectionRequest request)
//     {
//         await _service.CreateAsync(id, request);
//         return Ok();
//     }

//     //[HttpGet("{id}")]
//     //public ActionResult<ReportSectionQueryDto> Get([FromRoute]Guid id)
//     //{
//     //    var result = _query.Get(id);
//     //    if(result == null)
//     //        return NotFound();

//     //    return Ok(result);
//     //}
// }