using System;
using System.Collections.Generic;

namespace BancoPichincha.Core.Models.Entities
{
    public partial class Movimiento
    {
        public int MovimientoId { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public decimal Valor { get; set; }
        public decimal SaldoFinal { get; set; }
    }
}
