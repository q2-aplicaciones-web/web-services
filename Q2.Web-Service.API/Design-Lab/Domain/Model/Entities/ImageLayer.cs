namespace Q2.Web_Service.API.Design_Lab.Domain.Model.Aggregates;

public class ImageLayer : Layer
{
    public Uri ImageUrl { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}