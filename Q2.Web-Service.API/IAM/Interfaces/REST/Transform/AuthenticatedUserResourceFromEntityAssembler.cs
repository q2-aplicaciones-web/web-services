using Q2.Web_Service.API.IAM.Domain.Model.Aggregates;
using Q2.Web_Service.API.IAM.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}