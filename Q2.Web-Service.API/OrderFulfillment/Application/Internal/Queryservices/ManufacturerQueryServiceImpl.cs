using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Web_Service.API.OrderFulfillment.Application.Internal.Queryservices
{
    public class ManufacturerQueryServiceImpl : IManufacturerQueryService
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerQueryServiceImpl(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public IList<Manufacturer> Handle(GetAllManufacturersQuery query)
        {
            return _manufacturerRepository.FindAll();
        }

        public Manufacturer? Handle(GetManufacturerByUserIdQuery query)
        {
            return _manufacturerRepository.FindByUserId(query.UserId);
        }
    }
}
