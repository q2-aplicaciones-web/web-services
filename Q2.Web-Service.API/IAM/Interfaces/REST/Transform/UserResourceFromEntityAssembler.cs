using Q2.Web_Service.API.IAM.Domain.Model.Aggregates;
using Q2.Web_Service.API.IAM.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}