using Microsoft.AspNetCore.Mvc;
using plantool.Services;

namespace plantool.Controllers;

[ApiController]
[Route("ProcessCsv")]
public class CsvProcessingController : ControllerBase
{
    private readonly CsvProcessingService _service;
    private readonly ILogger<CsvProcessingController> _logger;
    public CsvProcessingController(CsvProcessingService service, ILogger<CsvProcessingController> logger) => (_service, _logger) = (service, logger);

    [HttpPost(Name = "ProcessCsv")]
    public async Task<IActionResult> ProcessCsv()
    {
        await _service.ProcessCsv();
        return Ok();
    }
}