using System;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Web_Service.API.OrderFulfillment.Application.Internal.Commandservices
{
    public class ManufacturerCommandServiceImpl : IManufacturerCommandService
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerCommandServiceImpl(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public Manufacturer? Handle(CreateManufacturerCommand command)
        {
            // Check if manufacturer already exists for this user
            var userId = new UserId(Guid.Parse(command.UserId));
            if (_manufacturerRepository.ExistsByUserId(userId))
            {
                return null; // Manufacturer already exists for this user
            }

            var manufacturer = new Manufacturer(
                command.UserId,
                command.Name,
                command.Address_Street,
                command.Address_City,
                command.Address_Country,
                command.Address_State,
                command.Address_Zip
            );

            try
            {
                _manufacturerRepository.Save(manufacturer);
                return manufacturer;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
