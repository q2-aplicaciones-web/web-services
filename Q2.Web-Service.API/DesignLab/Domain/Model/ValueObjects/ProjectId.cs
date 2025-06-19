namespace Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

public record ProjectId(Guid Id)
{
    public static ProjectId of(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Project ID cannot be null or empty.", nameof(id));
        }

        if (!Guid.TryParse(id, out var guid))
        {
            throw new ArgumentException("Invalid Project ID format.", nameof(id));
        }

        return new ProjectId(guid);
    }
}