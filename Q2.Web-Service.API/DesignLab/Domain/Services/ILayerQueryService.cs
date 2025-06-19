using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;

namespace Q2.Web_Service.API.DesignLab.Domain.Services;

public interface ILayerQueryService
{
    public Layer Handle(GetLayerByIdQuery query);
}