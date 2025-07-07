using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/manufacturers")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerQueryService _manufacturerQueryService;
        private readonly IManufacturerCommandService _manufacturerCommandService;

        public ManufacturersController(
            IManufacturerQueryService manufacturerQueryService,
            IManufacturerCommandService manufacturerCommandService)
        {
            _manufacturerQueryService = manufacturerQueryService;
            _manufacturerCommandService = manufacturerCommandService;
        }

        [HttpGet]
        public ActionResult GetAllManufacturers()
        {
            var manufacturers = _manufacturerQueryService.Handle(new GetAllManufacturersQuery());
            if (manufacturers == null || manufacturers.Count == 0)
                return NotFound();
            var resources = new List<ManufacturerResource>();
            foreach (var m in manufacturers)
                resources.Add(ManufacturerResourceFromEntityAssembler.ToResourceFromEntity(m));
            return Ok(resources);
        }
        
        [HttpGet("users/{userId:guid}")]
        public ActionResult GetAllManufacturers([FromRoute] Guid userId)
        {
            var query = new GetManufacturerByUserIdQuery(new UserId(userId));
            var manufacturer = _manufacturerQueryService.Handle(query);
            if (manufacturer == null)
                return NotFound();

            var resource = ManufacturerResourceFromEntityAssembler.ToResourceFromEntity(manufacturer);
            return Ok(resource);
        }
        
        

        [HttpPost]
        public ActionResult<ManufacturerResource> CreateManufacturer([FromBody] CreateManufacturerResource resource)
        {
            var command = CreateManufacturerCommandFromResourceAssembler.ToCommandFromResource(resource);
            var manufacturer = _manufacturerCommandService.Handle(command);
            if (manufacturer == null)
                return BadRequest();
            var manufacturerResource = ManufacturerResourceFromEntityAssembler.ToResourceFromEntity(manufacturer);
            return CreatedAtAction(nameof(GetAllManufacturers), new { id = manufacturerResource.Id },
                manufacturerResource);
        }
    }
}