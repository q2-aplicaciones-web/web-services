namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record ErrorResource(
    string Message,
    string Error,
    int Status,
    DateTime Timestamp
);
