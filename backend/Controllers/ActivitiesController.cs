using Microsoft.AspNetCore.Mvc;
using plantool.Domain.Dtos;
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

    [HttpPut("bulk-update")]
    public async Task<IActionResult> BulkUpdate([FromBody] BulkUpdateRequest request)
    {
        if (request == null || request.ActivityKeys == null || request.ActivityKeys.Count == 0)
            return BadRequest("Invalid request.");

        await _activitiesService.BulkUpdate(request);
        return Ok("Activities updated successfully.");
    }
}