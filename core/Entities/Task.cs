using System.ComponentModel.DataAnnotations.Schema;
using core.Entities.Base;

namespace core.Entities;

public class Task: FullEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public int Order { get; set; }
    
    public int StatusId { get; set; }
    
    public int UserId { get; set; }
    
    [ForeignKey(nameof(StatusId))]
    public Status? Status { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}