using Q2.Web_Service.API.IAM.Domain.Model.Commands;
using Q2.Web_Service.API.IAM.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}