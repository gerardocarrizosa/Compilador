using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public enum TipoToken
    {
        SimboloSimple,
        OperadorAritmetico,
        OperadorLogico,
        OperadorRelacional,
        PalabraReservada,
        SimboloDoble,
        Identificador,
        NumeroEntero,
        NumeroDecimal,
        Cadena,
        Booleano,
        Nada
    }

    public class Token
    {
        private TipoToken tipoToken;
        private int valorToken;
        private string lexema;
        private int linea;

        public int ValorToken
        {
            get
            {
                return valorToken;
            }

            set
            {
                valorToken = value;
            }
        }

        public string Lexema
        {
            get
            {
                return lexema;
            }

            set
            {
                lexema = value;
            }
        }

        public int Linea
        {
            get
            {
                return linea;
            }

            set
            {
                linea = value;
            }
        }

        public TipoToken TipoToken
        {
            get
            {
                return tipoToken;
            }

            set
            {
                tipoToken = value;
            }
        }
    }
}
