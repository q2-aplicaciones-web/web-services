using Q2.Web_Service.API.Analytics.Domain.Model.Aggregates;
using Q2.Web_Service.API.Analytics.Domain.Model.Commands;
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.API.Analytics.Application.Internal.Commandservices
{
    // [Service] // En C#, el registro del servicio se hace en la configuración de DI
    public class CustomerAnalyticsCommandServiceImpl
    {
        public User Handle(UpdateCustomerAnalyticsCommand command)
        {
            // Aquí va la lógica de actualización del agregado User
            CustomerAnalytics customerAnalytics = command.CustomerAnalytics;
            // En un caso real, aquí buscarías el agregado User y actualizarías solo la parte de customerAnalytics
            // Por simplicidad, se crea un User con solo customerAnalytics y manufacturerAnalytics en null
            return new User(customerAnalytics, null!);
        }
    }
}