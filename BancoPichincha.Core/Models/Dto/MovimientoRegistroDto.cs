using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Core.Models.Dto
{
    public class MovimientoRegistroDto
    {
        public string NumeroCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal Monto { get; set; }
    }
}
