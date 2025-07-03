namespace Q2.Web_Service.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(Guid Id, string Username, string Token);