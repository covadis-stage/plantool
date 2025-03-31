using Microsoft.AspNetCore.Mvc;

namespace plantool.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly ILogger<ActivityController> _logger;

    public ActivityController(ILogger<ActivityController> logger) =>
        (_logger) = (logger);

    [HttpGet]
    public IActionResult Get()
    {
        // Simulate fetching activities from a database or service
        var activities = new List<string> { "Activity1", "Activity2", "Activity3" };
        return Ok(activities);
    }
}