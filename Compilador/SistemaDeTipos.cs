using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public class SistemaDeTipos
    {
        #region Aritmeticos
        public string SistemaDeTiposSuma(int op1, int op2) {
            return Suma[op1, op2];
        }
        string[,] Suma = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"NumeroEntero","NumeroDecimal", null , null },
            /*deci*/{"NumeroDecimal","NumeroDecimal", null , null },
            /*cade*/{ null , null ,"Cadena", null },
            /*bool*/{ null , null , null , null }
        };

        public string SistemaDeTiposResta(int op1, int op2) {
            return Resta[op1, op2];
        }
        string[,] Resta = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"NumeroEntero","NumeroDecimal", null , null },
            /*deci*/{"NumeroDecimal","NumeroDecimal", null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null , null }
        };

        public string SistemaDeTiposMultiplicacion(int op1, int op2) {
            return Multiplicacion[op1, op2];
        }
        string[,] Multiplicacion = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"NumeroEntero","NumeroDecimal", null , null },
            /*deci*/{"NumeroDecimal","NumeroDecimal", null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null , null }
        };

        public string SistemaDeTiposDivision(int op1, int op2) {
            return Division[op1, op2];
        }
        string[,] Division = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"NumeroEntero","NumeroDecimal", null , null },
            /*deci*/{"NumeroDecimal","NumeroDecimal", null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null , null }
        };
        #endregion

        #region Relacionales
        public string SistemaDeTiposMayorQue(int op1, int op2)
        {
            return MayorQue[op1, op2];
        }
        string[,] MayorQue = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"Booleano","Booleano", null , null },
            /*deci*/{"Booleano","Booleano", null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null , null }
        };
        public string SistemaDeTiposMenorQue(int op1, int op2)
        {
            return MenorQue[op1, op2];
        }
        string[,] MenorQue = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"Booleano","Booleano", null , null },
            /*deci*/{"Booleano","Booleano", null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null , null }
        };
        public string SistemaDeTiposMayorOIgualQue(int op1, int op2)
        {
            return MayorOIgualQue[op1, op2];
        }
        string[,] MayorOIgualQue = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"Booleano","Booleano", null , null },
            /*deci*/{"Booleano","Booleano", null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null , null }
        };
        public string SistemaDeTiposMenorOIgualQue(int op1, int op2)
        {
            return MenorOIgualQue[op1, op2];
        }
        string[,] MenorOIgualQue = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"Booleano","Booleano", null , null },
            /*deci*/{"Booleano","Booleano", null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null , null }
        };
        public string SistemaDeTiposIgualA(int op1, int op2)
        {
            return IgualA[op1, op2];
        }
        string[,] IgualA = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"Booleano","Booleano", null , null },
            /*deci*/{"Booleano","Booleano", null , null },
            /*cade*/{ null , null ,"Booleano", null },
            /*bool*/{ null , null , null ,"Booleano"}
        };
        public string SistemaDeTiposDiferenteA(int op1, int op2)
        {
            return DiferenteA[op1, op2];
        }
        string[,] DiferenteA = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{"Booleano","Booleano", null , null },
            /*deci*/{"Booleano","Booleano", null , null },
            /*cade*/{ null , null ,"Booleano", null },
            /*bool*/{ null , null , null ,"Booleano"}
        };
        #endregion

        #region Booleanos
        public string SistemaDeTiposAND(int op1, int op2)
        {
            return AND[op1, op2];
        }
        string[,] AND = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{ null , null , null , null },
            /*deci*/{ null , null , null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null ,"Booleano"}
        };
        public string SistemaDeTiposOR(int op1, int op2)
        {
            return OR[op1, op2];
        }
        string[,] OR = new string[,] {
            /*------- ente | deci | cade | bool */
            /*ente*/{ null , null , null , null },
            /*deci*/{ null , null , null , null },
            /*cade*/{ null , null , null , null },
            /*bool*/{ null , null , null ,"Booleano"}
        };
        public string SistemaDeTiposNOT(int op)
        {
            return NOT[op];
        }
        string[] NOT = new string[] {
            /*ente | deci | cade | bool */
             null , null , null ,"Booleano"
        };
        #endregion
    }
}
