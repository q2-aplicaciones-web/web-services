using Cortex.Mediator.Notifications;
using Q2.Web_Service.API.Shared.Domain.Model.Events;

namespace Q2.Web_Service.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}