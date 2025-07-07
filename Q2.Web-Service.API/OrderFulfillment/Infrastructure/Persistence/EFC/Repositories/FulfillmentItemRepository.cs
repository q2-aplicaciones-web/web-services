using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities;

using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Web_Service.API.OrderFulfillment.Infrastructure.Persistence.EFC.Repositories
{
using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class FulfillmentItemRepository : IFulfillmentItemRepository
{
    private readonly AppDbContext _context;

    public FulfillmentItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public FulfillmentItem? FindById(FulfillmentItemId id)
    {
        // Solución universal: trae a memoria y compara por value object
        return _context.FulfillmentItems
            .AsEnumerable()
            .FirstOrDefault(i => i.Id.Equals(id));
    }

    public IList<FulfillmentItem> FindByFulfillmentId(Guid fulfillmentId)
    {
        return _context.FulfillmentItems
            .Where(i => i.FulfillmentId == fulfillmentId)
            .ToList();
    }

    public FulfillmentItem Save(FulfillmentItem item)
    {
        // Solución universal: trae a memoria y compara por value object
        var existing = _context.FulfillmentItems.AsEnumerable().FirstOrDefault(i => i.Id.Equals(item.Id));
        if (existing == null)
        {
            _context.FulfillmentItems.Add(item);
            _context.SaveChanges();
            _context.Entry(item).Reload();
            return item;
        }
        else
        {
            // El objeto ya está trackeado y modificado por el dominio
            _context.SaveChanges();
            _context.Entry(existing).Reload();
            return existing;
        }
    }
}
}
