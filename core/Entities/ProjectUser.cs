using System.ComponentModel.DataAnnotations.Schema;
using core.Entities.Base;

namespace core.Entities;

public class ProjectUser: FullEntity
{
    public int ProjectId { get; set; }
    
    public int UserId { get; set; }
    
    [ForeignKey(nameof(ProjectId))]
    public Project? Project { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}