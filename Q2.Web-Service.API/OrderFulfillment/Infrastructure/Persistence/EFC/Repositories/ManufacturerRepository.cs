using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;

using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Web_Service.API.OrderFulfillment.Infrastructure.Persistence.EFC.Repositories
{
using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly AppDbContext _context;

    public ManufacturerRepository(AppDbContext context)
    {
        _context = context;
    }

    public Manufacturer? FindById(Guid id)
    {
        return _context.Manufacturers
            .FirstOrDefault(m => m.Id == id);
    }

    public Manufacturer? FindByUserId(UserId userId)
    {
        return _context.Manufacturers
            .FirstOrDefault(m => m.UserId.Value == userId.Value);
    }

    public bool ExistsByUserId(UserId userId)
    {
        return _context.Manufacturers.Any(m => m.UserId == userId);
    }

    public IList<Manufacturer> FindAll()
    {
        return _context.Manufacturers.ToList();
    }

    public Manufacturer Save(Manufacturer manufacturer)
    {
        var existing = _context.Manufacturers.Find(manufacturer.Id);
        if (existing == null)
        {
            _context.Manufacturers.Add(manufacturer);
        }
        else
        {
            _context.Entry(existing).CurrentValues.SetValues(manufacturer);
        }
        _context.SaveChanges();
        return manufacturer;
    }
}
}
