using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Services.Localization;
using Nop.Services.Configuration;
using Nop.Plugin.Shipping.FreteCorreios.Models;

namespace Nop.Plugin.Shipping.FreteCorreios.Controllers
{
    [AdminAuthorize]
    public class ShippingFreteCorreiosController : BasePluginController
    {
        private readonly FreteCorreiosSetting _FreteCorreiosSettings;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;

        public ShippingFreteCorreiosController(FreteCorreiosSetting canadaPostSettings, ILocalizationService localizationService, ISettingService settingService)
        {
            this._FreteCorreiosSettings = canadaPostSettings;
            this._localizationService = localizationService;
            this._settingService = settingService;
        }

        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new FreteCorreiosModel
            {
                CodigoEmpresa = _FreteCorreiosSettings.CodigoEmpresa,
                Senha = _FreteCorreiosSettings.Senha,
                Url = _FreteCorreiosSettings.Url,
                CustoAdicionalEnvio = _FreteCorreiosSettings.CustoAdicionalEnvio,
                IncluirValorDeclarado = _FreteCorreiosSettings.IncluirValorDeclarado,
                UtilizaValorMinimo = _FreteCorreiosSettings.UtilizaValorMinimo,
                ValorMinimo = _FreteCorreiosSettings.ValorMinimo,
                DiasUteisAdicionais = _FreteCorreiosSettings.DiasUteisAdicionais
            };

            return View("~/Plugin.Shipping.FreteCorreios/View/FreteCorreios/Configure.cshtml", model);
        }

        [HttpPost]
        [ChildActionOnly]
        public ActionResult Configure(FreteCorreiosModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            //save settings
            _FreteCorreiosSettings.CodigoEmpresa        = model.CodigoEmpresa;
            _FreteCorreiosSettings.Senha                = model.Senha;
            _FreteCorreiosSettings.Url                  = model.Url;
            _FreteCorreiosSettings.CustoAdicionalEnvio  = model.CustoAdicionalEnvio;
            _FreteCorreiosSettings.IncluirValorDeclarado = model.IncluirValorDeclarado;
            _FreteCorreiosSettings.UtilizaValorMinimo   = model.UtilizaValorMinimo;
            _FreteCorreiosSettings.ValorMinimo = model.ValorMinimo;
            _FreteCorreiosSettings.DiasUteisAdicionais  = model.DiasUteisAdicionais;

            _settingService.SaveSetting(_FreteCorreiosSettings);

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return View("~/Plugin.Shipping.FreteCorreios/View/FreteCorreios/Configure.cshtml", model);
        }
    }
}
