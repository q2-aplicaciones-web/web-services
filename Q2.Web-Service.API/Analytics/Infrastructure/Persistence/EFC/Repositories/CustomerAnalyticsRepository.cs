using System;
using System.Linq;
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Q2.Web_Service.API.Analytics.Infrastructure.Persistence.EFC.Repositories
{
    public class CustomerAnalyticsRepository
    {
        private readonly AppDbContext _context;

        public CustomerAnalyticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public CustomerAnalytics? FindByUserId(Guid userId)
        {
            // UserId is a Guid, so nullability warning is only for possible not found
            return _context.Set<CustomerAnalytics>()
                .FirstOrDefault(ca => ca.UserId == userId);
        }

        // Implementar otros métodos CRUD si es necesario
    }
}