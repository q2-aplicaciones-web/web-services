using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Entities;

namespace Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;
public record ProcessOrderCommand(Guid OrderId, string PaymentMethod);
