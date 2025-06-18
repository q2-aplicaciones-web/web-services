namespace Q2.Web_Service.API.Shared.Infrastructure.ASP.Configuration.Extensions;
using System.Text.RegularExpressions;

public static partial class StringExtensions
{
    public static string ToKebabCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return KebabCaseRegex().Replace(str, "-$1").Trim().ToLower();
    }
    
    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
}