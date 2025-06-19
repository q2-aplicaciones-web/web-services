namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record CreateImageLayerResource(string ProjectId, string ImageUrl, int Width, int Height);
