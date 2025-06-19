namespace Q2.Web_Service.API.DesignLab.Domain.Model.Entities;

public class ImageLayer : Layer
{
    public Uri ImageUrl { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}