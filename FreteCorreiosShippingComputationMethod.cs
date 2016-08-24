using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Tracking;
using System.Web.Routing;

namespace Nop.Plugin.Shipping.FreteCorreios
{
    class FreteCorreiosShippingComputationMethod : BasePlugin, IShippingRateComputationMethod
    {
        public ShippingRateComputationMethodType ShippingRateComputationMethodType
        {
            get
            {
                return ShippingRateComputationMethodType.Realtime;
            }
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ShippingFreteCorreios";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Shipping.FreteCorreios.Controllers" }, { "area", null } };
        }

        public decimal? GetFixedRate(GetShippingOptionRequest getShippingOptionRequest)
        {
            throw new NotImplementedException();
        }

        public GetShippingOptionResponse GetShippingOptions(GetShippingOptionRequest getShippingOptionRequest)
        {
            throw new NotImplementedException();
        }

        public IShipmentTracker ShipmentTracker
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
