using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Core.Models.Dto
{
    public class ClienteDto
    {
        public int ClienteID { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }
    }
}
