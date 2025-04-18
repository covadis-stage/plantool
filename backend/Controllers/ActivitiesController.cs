using Microsoft.AspNetCore.Mvc;
using plantool.Services.Activities;

namespace plantool.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly ILogger<ActivitiesController> _logger;
    private readonly ActivitiesService _activitiesService;

    public ActivitiesController(ILogger<ActivitiesController> logger, ActivitiesService activitiesService) =>
        (_logger, _activitiesService) = (logger, activitiesService);

    [HttpGet("project/{projectKey}")]
    public async Task<IActionResult> GetActivitiesByProjectKey(string projectKey)
    {
        var activities = await _activitiesService.GetActivitiesByProjectKeyAsync(projectKey);
        return Ok(activities);
    }
}