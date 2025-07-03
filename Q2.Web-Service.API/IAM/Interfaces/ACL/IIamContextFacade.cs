namespace Q2.Web_Service.API.IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    Task<Guid> CreateUser(string username, string password);
    Task<Guid> FetchUserIdByUsername(string username);
    Task<string> FetchUsernameByUserId(Guid userId);
}