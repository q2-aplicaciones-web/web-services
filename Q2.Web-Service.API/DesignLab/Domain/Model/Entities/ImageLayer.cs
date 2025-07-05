using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Entities;

public class ImageLayer : Layer
{
    public Uri ImageUrl { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    
    // Constructor protegido requerido por EF Core
    protected ImageLayer() : base()
    {
        ImageUrl = null!; // Will be set by EF Core
    }
    
    public ImageLayer(CreateImageLayerCommand command) : base(command.ProjectId, ELayerType.Image)
    {
        ImageUrl = new Uri(command.ImageUrl);
        Width = command.Width;
        Height = command.Height;
    }

    public void UpdateDetails(UpdateImageLayerCommand command)
    {
        ImageUrl = new Uri(command.ImageUrl);
        Width = command.Width;
        Height = command.Height;
        // UpdatedAt will be handled by the base Layer class if we add it
    }
}