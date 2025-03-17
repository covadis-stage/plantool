namespace plantool.Models;

public class Project
{
    public string Id { get; set; }
    public string? Customer { get; set; }
    public string? ProjectManager { get; set; }
    public List<Network> Networks { get; set; }
}
