using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using plantool.Services.Projects;
using plantool.Services.SeedData;

namespace plantool.Controllers;

[ApiController]
[Route("[controller]")]
public class SeederController(ILogger<SeederController> logger, SeederService seederService) : ControllerBase
{
    private readonly ILogger<SeederController> _logger = logger;
    private readonly SeederService _seederService = seederService;

    [HttpPost]
    public async Task<IActionResult> Post(bool force = false)
    {
        return Ok(await _seederService.SeedTeamsAndEngineersAsync(force));
    }
}