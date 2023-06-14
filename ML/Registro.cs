using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Registro
    {
        public int IdRegistro { get; set; }
        public int Numero { get; set; }
        public int Resultado { get; set; }
        public string Fecha { get; set; }
        public Usuario Usuario { get; set; }
        public List<Object>? Registros { get; set; }

    }
}
