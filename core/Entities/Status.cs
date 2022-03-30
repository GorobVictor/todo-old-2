using core.Entities.Base;

namespace core.Entities;

public class Status: FullEntity
{
    public string? Name { get; set; }
    
    public bool IsDefault { get; set; }
}