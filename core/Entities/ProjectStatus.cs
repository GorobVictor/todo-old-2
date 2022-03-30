using System.ComponentModel.DataAnnotations.Schema;
using core.Entities.Base;

namespace core.Entities;

public class ProjectStatus: FullEntity
{
    public int ProjectId { get; set; }
    
    public int StatusId { get; set; }
    
    public int Order { get; set; }
    
    [ForeignKey(nameof(ProjectId))]
    public Project? Project { get; set; }
    
    [ForeignKey(nameof(StatusId))]
    public Status? Status { get; set; }
}