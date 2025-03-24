namespace plantool.Models.Entities;

public class Project
{
    public string Id { get; set; }
    public string? Customer { get; set; }
    public string? ProjectManager { get; set; }
    public ICollection<Network> Networks { get; set; }
}
