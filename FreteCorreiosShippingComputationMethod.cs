using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Tracking;
using System.Web.Routing;
using Nop.Services.Logging;
using Nop.Services.Configuration;
using FreteCorreios;
using Nop.Core.Domain.Shipping;

namespace Nop.Plugin.Shipping.FreteCorreios
{
    public class FreteCorreiosShippingComputationMethod : BasePlugin, IShippingRateComputationMethod
    {
        //private readonly ICurrencyService _currencyService;
        private readonly FreteCorreiosSetting _FreteCorreiosSetting;
        private readonly ILogger _logger;
        private readonly ISettingService _settingService;
        private readonly IShippingService _shippingService;

        public FreteCorreiosShippingComputationMethod(FreteCorreiosSetting freteCorreiosSetting, ILogger logger, ISettingService settingServices, IShippingService shippingServices)
        {
            _FreteCorreiosSetting = freteCorreiosSetting;
            _logger = logger;
            _settingService = settingServices;
            _shippingService = shippingServices;
    }

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
            if (getShippingOptionRequest == null)
                throw new ArgumentNullException("getShippingOptionRequest");

            if (getShippingOptionRequest.Items == null)
                return new GetShippingOptionResponse { Errors = new List<string> { "Sem itens poara envioNo." } };

            if (getShippingOptionRequest.ShippingAddress == null)
                return new GetShippingOptionResponse { Errors = new List<string> { "Endereço não foi informado." } };

            //if (getShippingOptionRequest.ShippingAddress.Country == null)
            //    return new GetShippingOptionResponse { Errors = new List<string> { "País não foi Shipping country is not set" } };

            if (string.IsNullOrEmpty(getShippingOptionRequest.ZipPostalCodeFrom))
                return new GetShippingOptionResponse { Errors = new List<string> { "CEP de origem não foi informado." } };

            //create object for the get rates requests
            var result = new GetShippingOptionResponse();
            //get rate for all available services
            var errorSummary = new StringBuilder();

            //write errors
            var errorString = errorSummary.ToString();

            var frete = new FreteCorreiosWS().CalcPrecoAsync("", "", "41106", getShippingOptionRequest.ZipPostalCodeFrom, getShippingOptionRequest.ShippingAddress.ZipPostalCode, getShippingOptionRequest.Items[0].ShoppingCartItem.Product.Weight.ToString(), 1, 16, 5, 11, 0, "N", getShippingOptionRequest.Items[0].ShoppingCartItem.Product.Price, "N");

            result.ShippingOptions.Add(new ShippingOption
            {
                Name = "PAC",
                Rate = 0,
                 Description = frete.Result.Servicos.ToList().ToString()
                });         


            if (!string.IsNullOrEmpty(errorString))
                _logger.Error(errorString);

            if (!result.ShippingOptions.Any())
                result.AddError(errorString);

            return result;
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
