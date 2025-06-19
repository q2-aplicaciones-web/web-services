using System;
using System.Linq;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.API.analytics.infrastructure.persistence.jpa.repositories
{
    public class ManufacturerAnalyticsRepository
    {
        private readonly AppDbContext _context;

        public ManufacturerAnalyticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public ManufacturerAnalytics FindByUserId(Guid userId)
        {
            return _context.Set<ManufacturerAnalytics>()
                .FirstOrDefault(ma => ma.UserId == userId);
        }

        // Implementar otros métodos CRUD si es necesario
    }
}