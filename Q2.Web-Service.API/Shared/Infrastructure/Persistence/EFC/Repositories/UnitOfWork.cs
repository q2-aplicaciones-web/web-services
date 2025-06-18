using Q2.Web_Service.API.Shared.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Unit of work implementation
/// </summary>
/// <remarks>
///     This class implements the basic operations for a unit of work.
///     It requires the context to be passed in the constructor.
/// </remarks>
/// <see cref="IUnitOfWork" />
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    /// <inheritdoc />
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}