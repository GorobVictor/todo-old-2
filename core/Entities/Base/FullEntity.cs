namespace core.Entities.Base;

public class FullEntity
{
    public int Id { get; set; }
    
    public bool Deleted { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public int? UpdatedBy { get; set; }
}