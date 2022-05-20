using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    class ListaPolish
    {
        public List<Token> listaTokensPolish;
        public List<string> listaPolish = new List<string>();
        List<Token> expresionInfijo = new List<Token>();
        List<Token> expresionPostfijo = new List<Token>();
        List<Token> pila = new List<Token>();
        public int tope = -1;
        int N = 50;
        public int lineaPolish;
        public int nivel;
        public List<string> pilaPendientes = new List<string>();

        public ListaPolish(List<Token> listaTokens, int linea)
        {
            listaTokensPolish = listaTokens;
            lineaPolish = linea;
        }

        public void ejecutarPolish(List<Token> listaTokensPolish, int contador)
        {
            bool dentroDelSi = false;
            bool existeSino = false;
            nivel = 0;
            int i;
            for (i = contador; i < listaTokensPolish.Count; i++)
            {
                if (listaTokensPolish[i].ValorToken == /*imprimir*/-53)
                {
                    listaPolish.Add(listaTokensPolish[i + 2].Lexema);
                    listaPolish.Add(listaTokensPolish[i].Lexema);
                }
                if (listaTokensPolish[i].ValorToken == /*=*/-31)
                {
                    RellenarListaPolishInfijoAPostfijo(listaTokensPolish, i);
                }
                if (listaTokensPolish[i].ValorToken == /*si*/-40)
                {
                    pilaPendientes.Add("S2");
                    pilaPendientes.Add("BRI-B");
                    pilaPendientes.Add("S1");
                    pilaPendientes.Add("BRF-A");
                    pilaPendientes.Add("EXP");
                    List<Token> Exp = new List<Token>();
                    int incrementador = i;
                    do
                    {
                        incrementador += 1;
                        Exp.Add(listaTokensPolish[incrementador]);
                    } while (listaTokensPolish[incrementador + 1].Lexema != ":");
                    expresionPostfijo.Clear();
                    expresionPostfijo = InfijoAPosfijo(Exp);
                    for (int c = 0; c < Exp.Count; c++)
                    {
                        listaPolish.Add(expresionPostfijo[c].Lexema);
                    }
                    pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                    listaPolish.Add(pilaPendientes[pilaPendientes.Count - 1]);
                    pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                    dentroDelSi = true;
                }
                if (listaTokensPolish[i].ValorToken == /*para*/-43)
                {

                }
                if (listaTokensPolish[i].ValorToken == /*haz*/-45)
                {
                    pilaPendientes.Add("FIN");
                    pilaPendientes.Add("BRV-G");
                    pilaPendientes.Add("EXP");
                    pilaPendientes.Add("S1");
                }
                if (listaTokensPolish[i].Lexema == "{")
                {
                    nivel += 1;
                }
                if (listaTokensPolish[i].Lexema == "}")
                {
                    if ((dentroDelSi || existeSino) && pilaPendientes.Count > 0)
                    {
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        if (pilaPendientes[pilaPendientes.Count - 1] != "S1" && pilaPendientes[pilaPendientes.Count - 1] != "S2")
                        {
                            listaPolish.Add(pilaPendientes[pilaPendientes.Count - 1]);
                            pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        }
                        
                    } else
                    {
                        nivel -= 1;
                    }
                }
                if (listaTokensPolish[i].Lexema == "sino")
                {
                    existeSino = true;
                }
                if (listaTokensPolish[i].Lexema == "mientras")
                {
                    pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                    List<Token> EXP = new List<Token>();
                    int incrementador = i + 1;
                    do
                    {
                        incrementador += 1;
                        EXP.Add(listaTokensPolish[incrementador]);
                    } while (listaTokensPolish[incrementador + 1].Lexema != ")");
                    expresionPostfijo.Clear();
                    expresionPostfijo = InfijoAPosfijo(EXP);
                    for (int c = 0; c < EXP.Count; c++)
                    {
                        listaPolish.Add(expresionPostfijo[c].Lexema);
                    }
                    if (pilaPendientes[pilaPendientes.Count - 1] != "EXP")
                    {
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                    } else
                    {
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        listaPolish.Add(pilaPendientes[pilaPendientes.Count - 1]);
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        listaPolish.Add(pilaPendientes[pilaPendientes.Count - 1]);
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                    }
                }
            }
        }

        public void RellenarListaPolishInfijoAPostfijo(List<Token> listaTokensPolish, int puntero)
        {
            expresionInfijo.Clear();
            expresionPostfijo.Clear();
            int incrementadorPuntero = puntero;
            int decrementadorPuntero = puntero;
            do
            {
                decrementadorPuntero -= 1;
                expresionInfijo.Add(listaTokensPolish[decrementadorPuntero]);
            } while (listaTokensPolish[decrementadorPuntero].TipoToken.ToString() == "PalabraReservada");
            expresionInfijo.Add(listaTokensPolish[puntero]);
            do
            {
                incrementadorPuntero += 1;
                expresionInfijo.Add(listaTokensPolish[incrementadorPuntero]);
                if (expresionInfijo[expresionInfijo.Count - 1].TipoToken.ToString() == "PalabraReservada")
                {
                    expresionPostfijo = InfijoAPosfijo(expresionInfijo);
                    for (int i = 0; i < expresionPostfijo.Count; i++)
                    {
                        listaPolish.Add(expresionPostfijo[i].Lexema);
                    }
                    break;
                }
            } while (listaTokensPolish[incrementadorPuntero + 1].ValorToken != -28);
        }

        public List<Token> InfijoAPosfijo(List<Token> listaOperacionInfijo)
        {
            expresionPostfijo.Clear();
            for (int i = 0; i < listaOperacionInfijo.Count; i++)
            {
                if (listaOperacionInfijo[i].TipoToken.ToString() == "Identificador"
                    || listaOperacionInfijo[i].TipoToken.ToString() == "NumeroEntero"
                    || listaOperacionInfijo[i].TipoToken.ToString() == "NumeroDecimal"
                    || listaOperacionInfijo[i].TipoToken.ToString() == "Cadena"
                    || listaOperacionInfijo[i].ValorToken == -55/*leer*/
                    || listaOperacionInfijo[i].ValorToken == -47/*verdadero*/
                    || listaOperacionInfijo[i].ValorToken == -48/*falso*/ )
                {
                    expresionPostfijo.Add(listaOperacionInfijo[i]);
                }
                else if (listaOperacionInfijo[i].Lexema != ")")
                {
                    if (tope == -1)
                    {
                        MeterALaPila(listaOperacionInfijo[i]);
                    }
                    else
                    {
                        if (Infijo(i, listaOperacionInfijo) <= PrioridadPila(pila))
                        {
                            expresionPostfijo.Add(SacarDeLaPila());
                            MeterALaPila(listaOperacionInfijo[i]);
                            if (pila.Count >= 2 && PrioridadPila(pila) <= PrioridadPilaCheck(pila))
                            {
                                expresionPostfijo.Add(SacarDeLaPila());
                            }
                        }
                        else if (Infijo(i, listaOperacionInfijo) > PrioridadPila(pila))
                        {
                            MeterALaPila(listaOperacionInfijo[i]);
                        }
                    }
                }
                else if (listaOperacionInfijo[i].Lexema == ")")
                {
                    while (pila[tope].Lexema != "(")
                    {
                        expresionPostfijo.Add(SacarDeLaPila());
                    }
                    if (pila[tope].Lexema == "(")
                    {
                        SacarDeLaPila();
                    }
                    else if (tope != -1)
                    {
                        expresionPostfijo.Add(SacarDeLaPila());
                    }
                }
            }
            while (tope > -1)
            {
                expresionPostfijo.Add(SacarDeLaPila());
            }
            // Descomentar para mostrar MessageBox con la expresion en postfijo
            /*string stringPostfijo = null;
            for (int i = 0; i < expresionPostfijo.Count; i++)
            {
                stringPostfijo += expresionPostfijo[i].Lexema;
            }
            MessageBox.Show($"{stringPostfijo}");*/
            return expresionPostfijo;
        }

        #region Funciones Infijo a Postfijo
        public void MeterALaPila(Token dato)
        {
            if (PilaLlena() != true)
            {
                tope = tope + 1;
                pila.Add(dato);
            }
        }

        public Token SacarDeLaPila()
        {
            if (PilaVacia() != true)
            {
                Token aux = pila[tope];
                pila.Remove(pila[tope]);
                tope = tope - 1;
                return aux;
            }
            return null;
        }

        public Token SacarDeLaPilaCheck()
        {
            if (PilaVacia() != true)
            {
                Token aux = pila[tope - 1];
                pila.Remove(pila[tope - 1]);
                tope = tope - 1;
                return aux;
            }
            return null;
        }

        public int Infijo(int i, List<Token> token)
        {
            int prioridadDeOperadores = 0;
            if (token[i].Lexema == "(" || token[i].Lexema == "[")
            {
                prioridadDeOperadores = 5;
                return prioridadDeOperadores;
            }
            else if (token[i].Lexema == "^")
            {
                prioridadDeOperadores = 4;
                return prioridadDeOperadores;
            }
            else if (token[i].Lexema == "*" || token[i].Lexema == "/")
            {
                prioridadDeOperadores = 2;
                return prioridadDeOperadores;
            }
            else if (token[i].Lexema == "+" || token[i].Lexema == "-")
            {
                prioridadDeOperadores = 1;
                return prioridadDeOperadores;
            }
            return prioridadDeOperadores;
        }

        public int PrioridadPila(List<Token> pila)
        {
            int prioridadPila = 0;
            if (pila[tope].Lexema == "^")
            {
                prioridadPila = 3;
                return prioridadPila;
            }
            else if (pila[tope].Lexema == "*" || pila[tope].Lexema == "/")
            {
                prioridadPila = 2;
                return prioridadPila;
            }
            else if (pila[tope].Lexema == "+" || pila[tope].Lexema == "-")
            {
                prioridadPila = 1;
                return prioridadPila;
            }
            else if (pila[tope].Lexema == "(" || pila[tope].Lexema == "[")
            {
                prioridadPila = 0;
                return prioridadPila;
            }
            return prioridadPila;
        }
        public int PrioridadPilaCheck(List<Token> pila)
        {
            int prioridadPila = 0;
            if (pila[tope - 1].Lexema == "^")
            {
                prioridadPila = 3;
                return prioridadPila;
            }
            else if (pila[tope - 1].Lexema == "*" || pila[tope - 1].Lexema == "/")
            {
                prioridadPila = 2;
                return prioridadPila;
            }
            else if (pila[tope - 1].Lexema == "+" || pila[tope - 1].Lexema == "-")
            {
                prioridadPila = 1;
                return prioridadPila;
            }
            else if (pila[tope - 1].Lexema == "(" || pila[tope - 1].Lexema == "[")
            {
                prioridadPila = 0;
                return prioridadPila;
            }
            return prioridadPila;
        }

        public bool PilaLlena()
        {
            if (tope == (N - 1))
                return true;
            else
                return false;
        }

        public bool PilaVacia()
        {
            if (tope == -1)
                return true;
            else
                return false;
        }
        #endregion
    }
}
