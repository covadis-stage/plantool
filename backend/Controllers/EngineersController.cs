using Microsoft.AspNetCore.Mvc;
using plantool.Domain.Dtos;
using plantool.Services.Engineers;

namespace plantool.Controllers;

[ApiController]
[Route("[controller]")]
public class EngineersController(EngineersService engineersService) : ControllerBase
{
    private readonly EngineersService _engineersService = engineersService;

    [HttpGet]
    public async Task<IActionResult> GetAllEngineers()
    {
        return Ok(await _engineersService.GetAllEngineersAsync());
    }

    [HttpGet("delegators")]
    public async Task<IActionResult> GetAllDelegators()
    {
        return Ok(await _engineersService.GetAllDelegatorsAsync());
    }
}