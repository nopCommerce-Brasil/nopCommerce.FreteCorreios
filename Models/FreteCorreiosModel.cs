using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Nop.Plugin.Shipping.FreteCorreios.Models
{
    public class FreteCorreiosModel
    {
        [DisplayName("URL")]
        public string Url { get; set; }

        [DisplayName("Código da Empresa")]
        public string CodigoEmpresa { get; set; }

        [DisplayName("Senha")]
        public string Senha { get; set; }

        [DisplayName("Custo adicional de envio")]
        public string CustoAdicionalEnvio { get; set; }

        [DisplayName("Valor Declarado?")]
        public bool IncluirValorDeclarado { get; set; }

        [DisplayName("Dias uteis adicionais ao prazo")]
        public int DiasUteisAdicionais { get; set; }

        [DisplayName("Utiliza Valor Minimo")]
        public bool UtilizaValorMinimo { get; set; }

        [DisplayName("Valor Minimo")]
        public Decimal ValorMinimo { get; set; }
    }
}
