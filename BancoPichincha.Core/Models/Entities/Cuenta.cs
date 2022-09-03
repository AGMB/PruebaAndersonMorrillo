using System;
using System.Collections.Generic;

namespace BancoPichincha.Core.Models.Entities
{
    public partial class Cuenta
    {
        public string NumeroCuenta { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
    }
}
