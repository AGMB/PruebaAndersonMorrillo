using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Core.Models.Dto
{
    public class MovimientoDto
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
