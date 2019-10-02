using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.GUI.Models;
using Messages;
using System.Dynamic;
using System.Threading;
using NServiceBus;

namespace Client.GUI.Controllers
{
    public class HomeController : Controller
    {
        private IEndpointInstance _endpointInstance;
        private static int messagesSent;

        public HomeController(IEndpointInstance endpointInstance)
        {
            _endpointInstance = endpointInstance;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            var orderId = Guid.NewGuid().ToString().Substring(0, 8);

            var command = new PlaceOrder { OrderId = orderId };

            // Send the command
            await _endpointInstance.Send(command).ConfigureAwait(false);

            dynamic model = new ExpandoObject();
            model.OrderId = orderId;
            model.MessagesSent = Interlocked.Increment(ref messagesSent);

            return View(model);
        }
    }
}
