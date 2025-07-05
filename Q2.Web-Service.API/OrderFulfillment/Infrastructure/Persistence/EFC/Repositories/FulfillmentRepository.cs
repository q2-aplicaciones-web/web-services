using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Web_Service.API.OrderFulfillment.Infrastructure.Persistence.EFC.Repositories
{
using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class FulfillmentRepository : IFulfillmentRepository
{
    private readonly AppDbContext _context;

    public FulfillmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public Fulfillment? FindById(Guid id)
    {
        return _context.Fulfillments
            .Include(f => f.Manufacturer)
            .FirstOrDefault(f => f.Id == id);
    }

    public IList<Fulfillment> FindByManufacturerId(Guid manufacturerId)
    {
        return _context.Fulfillments
            .Include(f => f.Manufacturer)
            .Where(f => f.Manufacturer != null && f.Manufacturer.Id == manufacturerId)
            .ToList();
    }

    public Fulfillment? FindByOrderId(Guid orderId)
    {
        return _context.Fulfillments
            .Include(f => f.Manufacturer)
            .FirstOrDefault(f => f.OrderId.Value == orderId);
    }

    public Fulfillment Save(Fulfillment fulfillment)
    {
        var existing = _context.Fulfillments.Find(fulfillment.Id);
        if (existing == null)
        {
            _context.Fulfillments.Add(fulfillment);
        }
        else
        {
            _context.Entry(existing).CurrentValues.SetValues(fulfillment);
        }
        _context.SaveChanges();
        return fulfillment;
    }
}
}
