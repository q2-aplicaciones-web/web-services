using System;
using System.Collections.Generic;
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
        return _context.FulfillmentItems
            .FirstOrDefault(i => i.Id.Value == id.Value);
    }

    public IList<FulfillmentItem> FindByFulfillmentId(Guid fulfillmentId)
    {
        return _context.FulfillmentItems
            .Where(i => i.FulfillmentId == fulfillmentId)
            .ToList();
    }

    public FulfillmentItem Save(FulfillmentItem item)
    {
        var existing = _context.FulfillmentItems.Find(item.Id);
        if (existing == null)
        {
            _context.FulfillmentItems.Add(item);
        }
        else
        {
            _context.Entry(existing).CurrentValues.SetValues(item);
        }
        _context.SaveChanges();
        return item;
    }
}
}
