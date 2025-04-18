using Microsoft.AspNetCore.Mvc;
using plantool.Services.SeedData;

namespace plantool.Controllers;

[ApiController]
[Route("[controller]")]
public class SeederController(SeederService seederService) : ControllerBase
{
    private readonly SeederService _seederService = seederService;

    [HttpPost]
    public async Task<IActionResult> Post(bool force = false)
    {
        return Ok(await _seederService.SeedTeamsAndEngineersAsync(force));
    }
}