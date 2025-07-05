using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/manufacturers")]
    public class ManufacturersController : ControllerBase
    {
        private readonly Q2.Web_Service.API.OrderFulfillment.Domain.Services.IManufacturerQueryService _manufacturerQueryService;
        private readonly Q2.Web_Service.API.OrderFulfillment.Domain.Services.IManufacturerCommandService _manufacturerCommandService;

        public ManufacturersController(
            Q2.Web_Service.API.OrderFulfillment.Domain.Services.IManufacturerQueryService manufacturerQueryService,
            Q2.Web_Service.API.OrderFulfillment.Domain.Services.IManufacturerCommandService manufacturerCommandService)
        {
            _manufacturerQueryService = manufacturerQueryService;
            _manufacturerCommandService = manufacturerCommandService;
        }

        [HttpGet]
        public ActionResult<IList<ManufacturerResource>> GetAllManufacturers()
        {
            var manufacturers = _manufacturerQueryService.Handle(new GetAllManufacturersQuery());
            if (manufacturers == null || manufacturers.Count == 0)
                return NotFound();
            var resources = new List<ManufacturerResource>();
            foreach (var m in manufacturers)
                resources.Add(ManufacturerResourceFromEntityAssembler.ToResourceFromEntity(m));
            return Ok(resources);
        }

        [HttpPost]
        public ActionResult<ManufacturerResource> CreateManufacturer([FromBody] CreateManufacturerResource resource)
        {
            var command = CreateManufacturerCommandFromResourceAssembler.ToCommandFromResource(resource);
            var manufacturer = _manufacturerCommandService.Handle(command);
            if (manufacturer == null)
                return BadRequest();
            var manufacturerResource = ManufacturerResourceFromEntityAssembler.ToResourceFromEntity(manufacturer);
            return CreatedAtAction(nameof(GetAllManufacturers), new { id = manufacturerResource.Id }, manufacturerResource);
        }
    }
}
