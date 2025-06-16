using Q2.Web_Service.API.Design_Lab.Domain.Model.Aggregates;

namespace Q2.Web_Service.API.DesignLab.Domain.Repositories;

public interface ILayerRepository
{
    Task<IEnumerable<Layer>> UpdateLayerAsync(Layer layer);
}