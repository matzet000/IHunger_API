﻿using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Services;
using IHunger.WebAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.V1.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}")]
    public class OrderStatusController : MainController
    {
        private readonly IOrderStatusService _orderStatusService;

        public OrderStatusController(
            IOrderStatusService orderStatusService,
            INotifier notifier, 
            IUser appUser) : base(notifier, appUser)
        {
            _orderStatusService = orderStatusService;
        }
    }
}
