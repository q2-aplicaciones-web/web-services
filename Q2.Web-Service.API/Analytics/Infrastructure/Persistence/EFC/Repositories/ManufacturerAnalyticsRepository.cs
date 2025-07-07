using System;
using System.Linq;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.API.Analytics.Infrastructure.Persistence.EFC.Repositories
{
    public class ManufacturerAnalyticsRepository
    {
        private readonly AppDbContext _context;

        public ManufacturerAnalyticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public ManufacturerAnalytics? FindByManufacturerId(Guid manufacturerId)
        {
            return _context.Set<ManufacturerAnalytics>()
                .FirstOrDefault(ma => ma.ManufacturerId == manufacturerId);
        }

        // Implementar otros métodos CRUD si es necesario
    }
}