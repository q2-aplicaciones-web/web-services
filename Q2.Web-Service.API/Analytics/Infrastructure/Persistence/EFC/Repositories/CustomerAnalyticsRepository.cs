using System;
using System.Linq;
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Q2.Web_Service.API.analytics.infrastructure.persistence.jpa.repositories
{
    public class CustomerAnalyticsRepository
    {
        private readonly AppDbContext _context;

        public CustomerAnalyticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public CustomerAnalytics FindByUserId(Guid userId)
        {
            return _context.Set<CustomerAnalytics>()
                .FirstOrDefault(ca => ca.UserId == userId);
        }

        // Implementar otros métodos CRUD si es necesario
    }
}