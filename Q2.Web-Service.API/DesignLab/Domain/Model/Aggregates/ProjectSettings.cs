
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;

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