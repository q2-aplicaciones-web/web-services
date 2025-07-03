using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Entities;

public class ImageLayer : Layer
{
    public Uri ImageUrl { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    
    public ImageLayer(CreateImageLayerCommand command) : base(command)
    {
        ImageUrl = new Uri(command.ImageUrl);
        Width = command.Width;
        Height = command.Height;
    }
    
    protected ImageLayer() { }
}