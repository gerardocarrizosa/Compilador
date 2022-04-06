using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public enum tipoError
    {
        Lexico,
        Sintactico,
        Semantico,
        codigoIntermedio,
        Ejecución
    }
    public class Error
    {
        private int codigo;
        private string mensajeError;
        private tipoError tipo;
        private int linea;

        public int Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public string MensajeError
        {
            get
            {
                return mensajeError;
            }

            set
            {
                mensajeError = value;
            }
        }

        public tipoError Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
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
    }
}
