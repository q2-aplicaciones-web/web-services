namespace Q2.Web_Service.API.IAM.Infrastructure.Tokens.JWT.Configuration;

/**
 * <summary>
 *     This class is used to store the token settings.
 *     It is used to configure the token settings in the app settings .json file.
 * </summary>
 */
public class TokenSettings
{
    public required string Secret { get; set; }
}