using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Repositories
{
    public interface IManufacturerRepository
    {
        Manufacturer? FindById(Guid id);
        Manufacturer? FindByUserId(UserId userId);
        bool ExistsByUserId(UserId userId);
        IList<Manufacturer> FindAll();
        Manufacturer Save(Manufacturer manufacturer);
    }
}
