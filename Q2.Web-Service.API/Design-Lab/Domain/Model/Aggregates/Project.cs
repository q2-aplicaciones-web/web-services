using Q2.Web_Service.API.Design_Lab.Domain.Model.Aggregates;
using Q2.Web_Service.API.Design_Lab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Design_Lab.Domain.Model.Entities;

public partial class Project
{
    public ProjectId Id { get; private set; }
    public string Title { get; private set; }
    public UserId UserId { get; private set; }
    public Uri PreviewUrl { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public ICollection<Layer> Layers { get; private set; }
}