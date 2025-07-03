using Q2.Web_Service.API.IAM.Domain.Model.Commands;
using Q2.Web_Service.API.IAM.Domain.Model.Queries;
using Q2.Web_Service.API.IAM.Domain.Services;

namespace Q2.Web_Service.API.IAM.Interfaces.ACL.Services;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<Guid> CreateUser(string username, string password)
    {
        var signUpCommand = new SignUpCommand(username, password);
        await userCommandService.Handle(signUpCommand);
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? Guid.Empty;
    }

    public async Task<Guid> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? Guid.Empty;
    }

    public async Task<string> FetchUsernameByUserId(Guid userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Username ?? string.Empty;
    }
}