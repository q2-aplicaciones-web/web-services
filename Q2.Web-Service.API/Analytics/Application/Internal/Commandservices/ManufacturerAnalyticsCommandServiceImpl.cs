using Q2.Web_Service.API.Analytics.Domain.Model.Aggregates;
using Q2.Web_Service.API.Analytics.Domain.Model.Commands;
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.API.Analytics.Application.Internal.Commandservices
{
    // Registrar este servicio en el contenedor de dependencias en Startup.cs o Program.cs
    public class ManufacturerAnalyticsCommandServiceImpl
    {
        public User Handle(UpdateManufacturerAnalyticsCommand command)
        {
            ManufacturerAnalytics manufacturerAnalytics = command.ManufacturerAnalytics;
            // En un caso real, aquí buscarías el agregado User y actualizarías solo la parte de manufacturerAnalytics
            // Por simplicidad, se crea un User con manufacturerAnalytics y customerAnalytics en null
            return new User(null!, manufacturerAnalytics);
        }
    }
}