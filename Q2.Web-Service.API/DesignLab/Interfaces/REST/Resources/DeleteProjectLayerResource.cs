using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record DeleteProjectLayerResource(LayerId LayerId, Guid ProjectId);
