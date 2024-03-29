﻿using System;
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
        public List<string> pilaPendientes = new List<string>();
        List<Token> expresionInfijo = new List<Token>();
        List<Token> expresionPostfijo = new List<Token>();
        List<Token> pila = new List<Token>();
        public int tope = -1;
        int N = 50;
        public int lineaPolish;
        int incrementoParaBrincos = 1;
        int iteracionesHaz = 1;

        public ListaPolish(List<Token> listaTokens, int linea)
        {
            listaTokensPolish = listaTokens;
            lineaPolish = linea;
        }
        public List<string> ejecutarPolish(List<Token> listaTokensPolish, int contador)
        {
            bool apuntadorG = false;
            for (int i = contador; i < listaTokensPolish.Count; i++)
            {
                if (listaTokensPolish[i].ValorToken == /*imprimir*/-53)
                {
                    if (apuntadorG)
                    {
                        listaPolish.Add("@G");
                        listaPolish.Add(listaTokensPolish[i + 2].Lexema);
                        listaPolish.Add(listaTokensPolish[i].Lexema);
                    }
                    else if (pilaPendientes.Count > 0 && pilaPendientes[pilaPendientes.Count - 1].Contains("S2"))
                    {
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        listaPolish.Add(string.Format("A{0}", incrementoParaBrincos - 1));
                        listaPolish.Add(listaTokensPolish[i + 2].Lexema);
                        listaPolish.Add(listaTokensPolish[i].Lexema);
                    }
                    else
                    {
                        listaPolish.Add(listaTokensPolish[i + 2].Lexema);
                        listaPolish.Add(listaTokensPolish[i].Lexema);
                    }
                }
                if (listaTokensPolish[i].ValorToken == /*=*/-31)
                {
                    if (apuntadorG)
                    {
                        listaPolish.Add("@G");
                        RellenarListaPolishInfijoAPostfijo(listaTokensPolish, i);
                    }
                    else if (pilaPendientes.Count > 0 && pilaPendientes[pilaPendientes.Count - 1].Contains("S2"))
                    {
                        listaPolish.Add(string.Format("A{0}", incrementoParaBrincos - 1));
                        RellenarListaPolishInfijoAPostfijo(listaTokensPolish, i);
                    }
                    else
                    {
                        RellenarListaPolishInfijoAPostfijo(listaTokensPolish, i);
                    }
                }
                if (listaTokensPolish[i].ValorToken == /*si*/-40)
                {
                    pilaPendientes.Add(string.Format("Si #{0} |FIN-B{0}", incrementoParaBrincos));
                    pilaPendientes.Add(string.Format("Si #{0} |S2-A{0}", incrementoParaBrincos));
                    pilaPendientes.Add(string.Format("Si #{0} |BRI-B{0}", incrementoParaBrincos));
                    pilaPendientes.Add(string.Format("Si #{0} |S1", incrementoParaBrincos));
                    pilaPendientes.Add(string.Format("Si #{0} |BRF-A{0}", incrementoParaBrincos));
                    pilaPendientes.Add("EXP");
                    List<Token> EXP = new List<Token>();
                    int incrementador = i;
                    do
                    {
                        incrementador += 1;
                        EXP.Add(listaTokensPolish[incrementador]);
                    } while (listaTokensPolish[incrementador + 1].Lexema != ":");
                    expresionPostfijo.Clear();
                    expresionPostfijo = InfijoAPosfijo(EXP);
                    for (int c = 0; c < EXP.Count; c++)
                    {
                        if (apuntadorG)
                        {
                            listaPolish.Add("@G");
                            listaPolish.Add(expresionPostfijo[c].Lexema);
                            apuntadorG = false;
                        }
                        else
                        {
                            listaPolish.Add(expresionPostfijo[c].Lexema);
                        }
                    }
                    pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                    listaPolish.Add(pilaPendientes[pilaPendientes.Count - 1].Split('|')[1]);
                    pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                    incrementoParaBrincos += 1;
                }
                if (listaTokensPolish[i].ValorToken == /*haz*/-45)
                {
                    pilaPendientes.Add(string.Format("Haz #{0} |FIN", iteracionesHaz));
                    pilaPendientes.Add(string.Format("Haz #{0} |BRV-G", iteracionesHaz));
                    pilaPendientes.Add(string.Format("Haz #{0} |EXP", iteracionesHaz));
                    pilaPendientes.Add(string.Format("Haz #{0} |S1", iteracionesHaz));
                    apuntadorG = true;
                    iteracionesHaz += 1;
                }
                if (listaTokensPolish[i].Lexema == "}")
                {
                    if (pilaPendientes.Count == 0)
                    {
                        continue;
                    }
                    if (pilaPendientes[pilaPendientes.Count - 1].Contains("FIN"))
                    {
                        if (listaTokensPolish[i + 1].Lexema == "mientras")
                        {
                            listaPolish.Add("@" + pilaPendientes[pilaPendientes.Count - 1].Substring(pilaPendientes[pilaPendientes.Count - 1].Length - 2));
                            pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                            pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                            List<Token> EXP = new List<Token>();
                            int incrementador = i + 1;
                            do
                            {
                                incrementador += 1;
                                EXP.Add(listaTokensPolish[incrementador]);
                            } while (listaTokensPolish[incrementador + 1].Lexema != ":");
                            expresionPostfijo.Clear();
                            expresionPostfijo = InfijoAPosfijo(EXP);
                            for (int c = 0; c < EXP.Count; c++)
                            {
                                listaPolish.Add(expresionPostfijo[c].Lexema);
                            }
                            pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                            listaPolish.Add(pilaPendientes[pilaPendientes.Count - 1].Split('|')[1]);
                            pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                            listaPolish.Add(pilaPendientes[pilaPendientes.Count - 1].Split('|')[1]);
                            pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        }
                        else
                        {
                            listaPolish.Add("@" + pilaPendientes[pilaPendientes.Count - 1].Substring(pilaPendientes[pilaPendientes.Count - 1].Length - 2));
                            pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        }
                        if (pilaPendientes.Count == 0)
                        {
                            break;
                        }
                        continue;
                    }
                    if (pilaPendientes[pilaPendientes.Count - 1].Contains("S1"))
                    {
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        listaPolish.Add(pilaPendientes[pilaPendientes.Count - 1].Split('|')[1]);
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        if (pilaPendientes[pilaPendientes.Count - 1].Contains("S2"))
                        {
                            listaPolish.Add("@" + pilaPendientes[pilaPendientes.Count - 1].Substring(pilaPendientes[pilaPendientes.Count - 1].Length - 2));
                            pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                        }
                        continue;
                    }
                    if (pilaPendientes[pilaPendientes.Count - 1].Contains("S1") || pilaPendientes[pilaPendientes.Count - 1].Contains("FIN") 
                        && listaTokensPolish[i + 1].Lexema == "mientras")
                    {
                        pilaPendientes.RemoveAt(pilaPendientes.Count - 1);
                    }
                }
            }
            return listaPolish;
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
                /*if (expresionInfijo[expresionInfijo.Count - 1].TipoToken.ToString() == "PalabraReservada"
                    || expresionInfijo[expresionInfijo.Count - 1].TipoToken.ToString() == "Cadena")
                //|| expresionInfijo[expresionInfijo.Count - 1].TipoToken.ToString() == "NumeroEntero"
                //|| expresionInfijo[expresionInfijo.Count - 1].TipoToken.ToString() == "NumeroDecimal" 
                //&& listaTokensPolish[incrementadorPuntero + 1].TipoToken.ToString() != "OperadorAritmetico")
                {
                    expresionPostfijo = InfijoAPosfijo(expresionInfijo);
                    for (int i = 0; i < expresionPostfijo.Count; i++)
                    {
                        listaPolish.Add(expresionPostfijo[i].Lexema);
                    }
                    break;
                }*/
            } while (listaTokensPolish[incrementadorPuntero + 1].ValorToken != -28);
            List<Token> izquierda = new List<Token>();
            List<Token> derecha = new List<Token>();
            int x = 0;
            do
            {
                izquierda.Add(expresionInfijo[x]);
                x++;
            } while (expresionInfijo[x].ValorToken != -31);
            x++;
            do
            {
                derecha.Add(expresionInfijo[x]);
                x++;
            } while (x <= expresionInfijo.Count - 1);
            if (derecha.Count == 1)
            {
                expresionPostfijo = InfijoAPosfijo(expresionInfijo);
                for (int i = 0; i < expresionPostfijo.Count; i++)
                {
                    listaPolish.Add(expresionPostfijo[i].Lexema);
                }
            }
            else
            {
                List<Token> derechaPostfijo = new List<Token>();
                List<Token> izquierdaPostfijo = new List<Token>();
                expresionPostfijo.Add(expresionInfijo[0]);
                derechaPostfijo = InfijoAPosfijo(derecha);
                expresionPostfijo.Add(expresionInfijo[1]);
                for (int i = 0; i < expresionPostfijo.Count; i++)
                {
                    listaPolish.Add(expresionPostfijo[i].Lexema);
                }
            }
            
        }

        public List<Token> InfijoAPosfijo(List<Token> listaOperacionInfijo)
        {
            //expresionPostfijo.Clear();
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
        /*if (listaTokensPolish[i].ValorToken == -43)
                {

                }*/
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
