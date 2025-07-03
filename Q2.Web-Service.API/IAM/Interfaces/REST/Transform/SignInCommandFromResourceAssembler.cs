using Q2.Web_Service.API.IAM.Domain.Model.Commands;
using Q2.Web_Service.API.IAM.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}