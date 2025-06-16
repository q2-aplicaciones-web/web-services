using Q2.Web_Service.API.Design_Lab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Design_Lab.Domain.Model.Entities;

/// <summary>
/// Here is the Project Information declaration.
/// 
/// </summary>
public partial class Project
{
    public EProjectStatus Status { get; private set; }
    public EGarmentGender Gender { get; private set; }
    public EGarmentColor Color { get; private set; }
    public EGarmentSize Size { get; private set; }
}