using System;
using System.Collections.Generic;

namespace BancoPichincha.Core.Models.Entities
{
    public partial class Cliente
    {
        public int ClienteId { get; set; }
        public string ClienteIdentificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
