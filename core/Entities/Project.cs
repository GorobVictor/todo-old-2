using core.Entities.Base;

namespace core.Entities;

public class Project: FullEntity
{
    public string? Name { get; set; }
    
    public int Order { get; set; }
}