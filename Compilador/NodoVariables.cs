using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public class NodoVariables
    {
        string lexema;
        string tipo;
        NodoVariables siguiente = null;

        public NodoVariables(string lexema, string tipo) {
            this.lexema = lexema;
            this.tipo = tipo;
        }
    }
}
