using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Shipping.FreteCorreios
{
    public class FreteCorreiosSetting : ISettings
    {
        public string Url { get; set; }

        public string CodigoEmpresa { get; set; }

        public string Senha { get; set; }

        public string CustoAdicionalEnvio { get; set; }

        public bool IncluirValorDeclarado { get; set; }

        public int DiasUteisAdicionais { get; set; }

        public bool UtilizaValorMinimo { get; set; }

        public Decimal ValorMinimo { get; set; }
    }
}
