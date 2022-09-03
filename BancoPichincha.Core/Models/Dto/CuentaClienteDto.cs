using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Core.Models.Dto
{
    public class CuentaClienteDto
    {
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
    }
}
