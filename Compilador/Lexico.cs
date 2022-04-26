using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    class Lexico
    {
        public List<Error> listaErrorLexico;
        public List<Token> listaToken;

        private string codigoFuente;
        public int linea;

        private int[,] matrizTransicion =
        {
                    //   0       1       2       3       4       5       6       7       8       9      10      11      12      13      14      15      16      17      18      19      20      21      22      23      24      25      26      27      28
                    //  dig  ║  pal  ║   '   ║   +   ║   -   ║   *   ║   /   ║   <   ║   >   ║   =   ║   !   ║   &   ║   {   ║   }   ║   (   ║   )   ║   [   ║   ]   ║   |   ║   .   ║   ,   ║   ;   ║   :   ║   ?   ║  esp  ║  ent  ║  eof  ║  tab  ║ desc 
            /* 0  */ {   2   ,   1   ,   5   ,   6   ,   7   ,   8   ,   9   ,   11  ,   10  ,   12  ,   13  ,   15  ,  -23  ,  -24  ,  -21  ,  -22  ,  -25  ,  -26  ,   14  ,  -32  ,  -27  ,  -28  ,  -29  ,  -30  ,   0   ,   0   ,   0   ,   0   , -500 },
            /* 1  */ {   1   ,   1   , -500  ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   ,   -1  , -500  , -500  ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   ,  -1   , -500  ,  -1   ,  -1   ,  -1   ,  -1   , -500 },
            /* 2  */ {   2   , -501  , -501  ,  -2   ,  -2   ,  -2   ,  -2   ,  -2   ,  -2   ,   -2  ,  -2   ,  -2   ,  -2   ,  -2   ,  -2   ,  -2   ,  -2   ,  -2   ,  -2   ,   3   ,  -2   ,  -2   , -501  , -501  ,  -2   ,  -2   ,  -2   ,  -2   , -500 },
            /* 3  */ {   4   , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -502  , -500 },
            /* 4  */ {   4   , -502  , -502  ,  -3   ,  -3   ,  -3   ,  -3   ,  -3   ,  -3   ,  -3   , -502  ,  -3   , -502  ,  -3   , -502  ,  -3   , -502  ,  -3   , -502  , -502  ,  -3   ,  -3   , -502  , -502  ,  -3   ,  -3   ,  -3   ,  -3   , -500 },
            /* 5  */ {   5   ,   5   ,  -4   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   , -500  ,   5   ,   5   ,   5   ,   5   ,   5   ,   5   , -503  , -503  ,   5   , -500 },
            /* 6  */ {  -6   ,  -6   ,  -6   ,  -19  ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   , -500  ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   ,  -6   , -500 },
            /* 7  */ {  -7   ,  -7   ,  -7   ,  -7   ,  -20  ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   , -500  ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   ,  -7   , -500 },
            /* 8  */ {  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   , -500  ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   ,  -8   , -500 },
            /* 9  */ {  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   , -500  ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   ,  -9   , -500 },
            /* 10 */ {  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -13  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  , -500  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  ,  -10  , -500 },
            /* 11 */ {  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -12  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  , -500  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  ,  -11  , -500 },
            /* 12 */ {  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -14  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  , -500  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  ,  -31  , -500 },
            /* 13 */ { -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  ,  -15  ,  -18  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -500  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -500 },
            /* 14 */ { -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  ,  -17  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -500 },
            /* 15 */ { -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  ,  -16  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -505  , -500 }

        };

        public Lexico(String codigoFuenteInterface)
        {
            codigoFuente = codigoFuenteInterface + " ";
            listaToken = new List<Token>();
            listaErrorLexico = new List<Error>();
        }

        private int PalabraReservada(string lexema)
        {
            switch (lexema)
            {
                case "vacio":
                    return -34;
                case "cadena":
                    return -35;
                case "ent":
                    return -36;
                case "doble":
                    return -37;
                case "bool":
                    return -38;
                case "clase":
                    return -39;
                case "si":
                    return -40;
                case "importar":
                    return -41;
                case "sino":
                    return -42;
                case "para":
                    return -43;
                case "mientras":
                    return -44;
                case "haz":
                    return -45;
                case "nulo":
                    return -46;
                case "verdadero":
                    return -47;
                case "falso":
                    return -48;
                case "cambiar":
                    return -49;
                case "caso":
                    return -50;
                case "romper":
                    return -51;
                case "var":
                    return -52;
                case "imprimir":
                    return -53;
                case "default":
                    return -54;
                case "leer":
                    return -55;
                default:
                    return -1;
            }
        }

        private char SiguienteCaracter(int i)
        {
            return Convert.ToChar(codigoFuente.Substring(i, 1));
        }

        private int RegresarColumna(char caracter, object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (char.IsDigit(caracter))
            {
                return 0;
            }
            else if (char.IsLetter(caracter))
            {
                return 1;
            }
            else if (caracter.Equals('\''))
            {
                return 2;
            }
            else if (caracter.Equals('+'))
            {
                return 3;
            }
            else if (caracter.Equals('-'))
            {
                return 4;
            }
            else if (caracter.Equals('*'))
            {
                return 5;
            }
            else if (caracter.Equals('/'))
            {
                return 6;
            }
            else if (caracter.Equals('<'))
            {
                return 7;
            }
            else if (caracter.Equals('>'))
            {
                return 8;
            }
            else if (caracter.Equals('='))
            {
                return 9;
            }
            else if (caracter.Equals('!'))
            {
                return 10;
            }
            else if (caracter.Equals('&'))
            {
                return 11;
            }
            else if (caracter.Equals('{'))
            {
                return 12;
            }
            else if (caracter.Equals('}'))
            {
                return 13;
            }
            else if (caracter.Equals('('))
            {
                return 14;
            }
            else if (caracter.Equals(')'))
            {
                return 15;
            }
            else if (caracter.Equals('['))
            {
                return 16;
            }
            else if (caracter.Equals(']'))
            {
                return 17;
            }
            else if (caracter.Equals('|'))
            {
                return 18;
            }
            else if (caracter.Equals('.'))
            {
                return 19;
            }
            else if (caracter.Equals(','))
            {
                return 20;
            }
            else if (caracter.Equals(';'))
            {
                return 21;
            }
            else if (caracter.Equals(':'))
            {
                return 22;
            }
            else if (caracter.Equals('?'))
            {
                return 23;
            }
            else if (caracter.Equals(' '))
            {
                return 24;
            }
            else if (e.KeyChar == (char)13)
            {
                return 25;
            }
            else if (caracter.Equals('\t'))
            {
                return 27;
            }
            else
            {
                return 28;
            }
        }

        private TipoToken Tipo(int estado)
        {
            switch (estado)
            {
                case -1:
                    return TipoToken.Identificador;

                case -2:
                    return TipoToken.NumeroEntero;

                case -3:
                    return TipoToken.NumeroDecimal;

                case -4:
                    return TipoToken.Cadena;

                case -5:
                    return TipoToken.Booleano;

                case -6:
                    return TipoToken.OperadorAritmetico;

                case -7:
                    return TipoToken.OperadorAritmetico;

                case -8:
                    return TipoToken.OperadorAritmetico;

                case -9:
                    return TipoToken.OperadorAritmetico;

                case -10:
                    return TipoToken.OperadorRelacional;

                case -11:
                    return TipoToken.OperadorRelacional;

                case -12:
                    return TipoToken.OperadorRelacional;

                case -13:
                    return TipoToken.OperadorRelacional;

                case -14:
                    return TipoToken.OperadorRelacional;

                case -15:
                    return TipoToken.OperadorRelacional;

                case -16:
                    return TipoToken.OperadorLogico;

                case -17:
                    return TipoToken.OperadorLogico;

                case -18:
                    return TipoToken.OperadorLogico;

                case -19:
                    return TipoToken.SimboloDoble;

                case -20:
                    return TipoToken.SimboloDoble;

                case -21:
                    return TipoToken.SimboloSimple;

                case -22:
                    return TipoToken.SimboloSimple;

                case -23:
                    return TipoToken.SimboloSimple;

                case -24:
                    return TipoToken.SimboloSimple;

                case -25:
                    return TipoToken.SimboloSimple;

                case -26:
                    return TipoToken.SimboloSimple;

                case -27:
                    return TipoToken.SimboloSimple;

                case -28:
                    return TipoToken.SimboloSimple;

                case -29:
                    return TipoToken.SimboloSimple;

                case -30:
                    return TipoToken.SimboloSimple;

                case -31:
                    return TipoToken.SimboloSimple;

                case -32:
                    return TipoToken.SimboloSimple;

                case -33:
                    return TipoToken.SimboloSimple;

                case -34:
                    return TipoToken.PalabraReservada;

                case -35:
                    return TipoToken.PalabraReservada;

                case -36:
                    return TipoToken.PalabraReservada;

                case -37:
                    return TipoToken.PalabraReservada;

                case -38:
                    return TipoToken.PalabraReservada;

                case -39:
                    return TipoToken.PalabraReservada;

                case -40:
                    return TipoToken.PalabraReservada;

                case -41:
                    return TipoToken.PalabraReservada;

                case -42:
                    return TipoToken.PalabraReservada;

                case -43:
                    return TipoToken.PalabraReservada;

                case -44:
                    return TipoToken.PalabraReservada;

                case -45:
                    return TipoToken.PalabraReservada;

                case -46:
                    return TipoToken.PalabraReservada;

                case -47:
                    return TipoToken.PalabraReservada;

                case -48:
                    return TipoToken.PalabraReservada;

                case -49:
                    return TipoToken.PalabraReservada;

                case -50:
                    return TipoToken.PalabraReservada;

                case -51:
                    return TipoToken.PalabraReservada;

                case -52:
                    return TipoToken.PalabraReservada;

                case -53:
                    return TipoToken.PalabraReservada;

                case -54:
                    return TipoToken.PalabraReservada;

                case -55:
                    return TipoToken.PalabraReservada;

                default:
                    return TipoToken.Nada;
            }
        }

        private Error ManejoErrores(int estado)
        {
            string mensajeError;

            switch (estado)
            {
                case -500:
                    mensajeError = "Identificador no válido.";
                    break;

                case -501:
                    mensajeError = "Número Entero no válido.";
                    break;

                case -502:
                    mensajeError = "Número decimal no válido.";
                    break;

                case -503:
                    mensajeError = "Formato de cadena inválida: se esperaba una \'.";
                    break;

                case -504:
                    mensajeError = "Símbolo no válido.";
                    break;

                default:
                    mensajeError = "Error inesperado.";
                    break;
            }

            return new Error()
            {
                Codigo = estado,
                MensajeError = mensajeError,
                Tipo = tipoError.Lexico,
                Linea = linea
            };

        }

        public List<Token> EjecutarLexico()
        {
            int estado = 0;
            int columna = 0;
            bool entrar = false;
            char caracterActual;
            string lexema = string.Empty;
            linea = 1;

            for (int i = 0; i < codigoFuente.ToCharArray().Length; i++)
            {
                entrar = false;
                caracterActual = SiguienteCaracter(i);

                if (caracterActual.Equals('\n'))
                {
                    linea++;
                }

                lexema += caracterActual;
                columna = RegresarColumna(caracterActual, null, new KeyPressEventArgs(Convert.ToChar(Keys.Enter)));
                estado = matrizTransicion[estado, columna];

                if (estado < 0 && estado > -499)
                {

                    if (lexema.Length > 1)
                    {
                        lexema = lexema + " ";
                        lexema = lexema.Remove(lexema.Length - 1);
                    }

                    /*if (lexema.Length  == 2)
                    {
                        lexema = lexema.Remove(lexema.Length - 1);
                    }*/

                    Token nuevoToken = new Token() 
                    {
                        ValorToken = estado,
                        Lexema = lexema,
                        Linea = linea
                    };

                    if (estado == -1)
                    {
                        nuevoToken.ValorToken = PalabraReservada(lexema.Remove(lexema.Length - 1));
                        nuevoToken.Lexema = lexema.Remove(lexema.Length - 1);
                        nuevoToken.Lexema = lexema.Remove(lexema.Length - 1);
                        entrar = true;
                    }
                    else if (estado == -2 || estado == -6 || estado == -7 || estado == -8 || estado == -9 || estado == -10 || estado == -11 /*|| estado == -12 || estado == -13 || estado == -14 || estado == -15*/)
                    {
                        entrar = true;
                        nuevoToken.Lexema = lexema.Remove(lexema.Length - 1);
                    }
                    else if (estado == -3)
                    {
                        entrar = true;
                        nuevoToken.Lexema = lexema.Remove(lexema.Length - 1);
                    }
                    /*else if (estado <= -12 && estado >= -20)
                    {
                        //entrar = true;
                        //nuevoToken.Lexema = lexema.Remove(lexema.Length - 1);
                    }
                    /*else if (estado <= -16 && estado >= -21)
                    {
                        entrar = true;
                        nuevoToken.Lexema = lexema.Remove(lexema.Length - 1);
                    }*/

                    nuevoToken.TipoToken = Tipo(nuevoToken.ValorToken);
                    listaToken.Add(nuevoToken);

                    if (entrar == true)
                    {
                        i--;
                    }

                    estado = 0;
                    columna = 0;
                    lexema = string.Empty;
                }
                else if (estado <= -499)
                {
                    listaErrorLexico.Add(ManejoErrores(estado));
                    estado = 0;
                    columna = 0;
                    lexema = string.Empty;
                }
                else if (estado == 0)
                {
                    columna = 0;
                    lexema = string.Empty;
                }
            }
            return listaToken;
        }
    }
}