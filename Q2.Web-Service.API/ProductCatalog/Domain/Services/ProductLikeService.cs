using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Services
{
    public interface IProductLikeService
    {
        Task Handle(LikeProductCommand command);
        Task Handle(UnlikeProductCommand command);
        Task<bool> Handle(CheckIfUserLikedProductQuery query);
        Task<long> Handle(GetLikeCountByProductQuery query);
    }
}
