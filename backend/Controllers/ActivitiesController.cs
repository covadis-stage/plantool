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

    [HttpPut("{activityKey}/general-remark")]
    public async Task<IActionResult> SetGeneralRemark(string activityKey, [FromQuery] string generalRemark)
    {
        if (string.IsNullOrWhiteSpace(activityKey)) return BadRequest("Invalid activity key.");

        var result = await _activitiesService.SetGeneralRemark(activityKey, generalRemark);
        return result ? Ok("Remark updated successfully.") : NotFound("Activity not found.");
    }

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

    [HttpDelete("bulk-delete")]
    public async Task<IActionResult> BulkDelete([FromBody] BulkDeleteRequest request)
    {
        if (request == null || request.ActivityKeys == null || request.ActivityKeys.Count == 0)
            return BadRequest("Invalid request.");

        await _activitiesService.BulkDelete(request);
        return Ok("Activities deleted successfully.");
    }
}