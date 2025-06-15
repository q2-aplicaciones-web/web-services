using Q2.Web_Service.API.Design_Lab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Design_Lab.Domain.Model.Entities;

public class Project
{
    public ProjectId Id { get; private set; }
    public string Title { get; private set; }
    public UserId UserId { get; private set; }
    public Uri PreviewUrl { get; private set; }
    public EProjectStatus Status { get; private set; }
    
}