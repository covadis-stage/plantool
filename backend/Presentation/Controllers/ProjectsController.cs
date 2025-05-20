using Microsoft.AspNetCore.Mvc;
using plantool.Services.Projects;

namespace plantool.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private readonly ProjectsService _projectsService;

    public ProjectsController(ILogger<ProjectsController> logger, ProjectsService projectsService) =>
        (_logger, _projectsService) = (logger, projectsService);

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var projects = await _projectsService.GetProjectsAsync();
        return Ok(projects);
    }
}