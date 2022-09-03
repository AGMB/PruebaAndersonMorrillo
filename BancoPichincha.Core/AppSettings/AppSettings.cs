using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Core.AppSettings
{
    public class AppSettings
    {
        public ConnectionStrings connectionStrings { get; set; }
        public Configuracion configuracion { get; set; }
    }

    public class ConnectionStrings
    {
        public string testBancoPichincha { get; set; }
    }

    public class Configuracion
    {
        public decimal LimiteDiario { get; set; }
    }
}
