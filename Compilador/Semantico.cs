using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using System.Windows.Forms;

namespace Compilador
{
    public class Semantico
    {
        string tempTipo = null;
        //public LinkedList<Variables> variables = new LinkedList<Variables>();
        public List<Variables> variables = new List<Variables>();
        public List<Token> listaTokensSemantico;
        public List<Error> listaErrorSemantico;
        int lineaSemantico;
        List<Variables> expresionInfijo = new List<Variables>();
        List<Variables> pila = new List<Variables>();
        List<Variables> expresionPostfijo = new List<Variables>();
        List<Variables> expresionIzquierda = new List<Variables>();
        List<Variables> expresionDerecha = new List<Variables>();
        List<Variables> expresionIzquierdaEnPostfijo = new List<Variables>();
        List<Variables> expresionDerechaEnPostfijo = new List<Variables>();
        List<Variables> condicionalEnInfijo = new List<Variables>();
        List<Variables> condicionalEnPostfijo = new List<Variables>();
        List<Variables> expresionCondicionalEnInfijo = new List<Variables>();
        string tipoDeCondicionalCompleta = null;
        public int tope = -1;
        int N = 50;

        public Semantico(List<Token> listaTokensSintactico, int linea)
        {
            listaTokensSemantico = listaTokensSintactico;
            lineaSemantico = linea;
            listaErrorSemantico = new List<Error>();
        }

        public List<Variables> ejecutarSemantico(List<Token> listaTokensSemantico)
        {
            int i;
            for (i = 0; i < listaTokensSemantico.Count; i++)
            {
                if (listaTokensSemantico[i].ValorToken == -36)
                {
                    tempTipo = "NumeroEntero";
                    AgregarVariable(variables, i);
                }
                if (listaTokensSemantico[i].ValorToken == -37)
                {
                    tempTipo = "NumeroDecimal";
                    AgregarVariable(variables, i);
                }
                if (listaTokensSemantico[i].ValorToken == -35)
                {
                    tempTipo = "Cadena";
                    AgregarVariable(variables, i);
                }
                if (listaTokensSemantico[i].ValorToken == -38)
                {
                    tempTipo = "Booleano";
                    AgregarVariable(variables, i);
                }
                if (listaTokensSemantico[i].ValorToken == -27)
                {
                    AgregarVariable(variables, i);
                }
                if (listaTokensSemantico[i].ValorToken == -31)
                {
                    AsignacionDeVariable(i);
                }
                if (listaTokensSemantico[i].ValorToken == -40)
                {
                    ValidacionDeTiposRelacional(variables, i);
                }
            }
            return variables;
        }

        public void AsignacionDeVariable(int puntero)
        {
            expresionPostfijo.Clear();
            expresionInfijo.Clear();
            List<Variables> listaDeVariablesAuxiliar = variables.ToList();
            int llenadorDePila = puntero;
            string tipoDeOperacionIngresada;
            for (int nodo = 0; nodo < variables.Count; nodo++)
            {
                if (listaTokensSemantico[puntero - 1].Lexema == listaDeVariablesAuxiliar[nodo].Lexema)
                {
                    expresionInfijo.Clear();
                    for (llenadorDePila = puntero; llenadorDePila <= listaTokensSemantico.Count - 1; llenadorDePila++)
                    {
                        if (llenadorDePila == listaTokensSemantico.Count - 1)
                        {
                            NuevoError(-605, listaTokensSemantico[puntero - 1].Lexema);
                            break;
                        }
                        else if (listaTokensSemantico[llenadorDePila + 1].ValorToken == -28)
                        { //Aqui se agregó que leer lo dejara pasar.
                            break;
                        }
                        else
                        {
                            expresionInfijo.Add(new Variables { Tipo = listaTokensSemantico[llenadorDePila + 1].TipoToken.ToString(), Lexema = listaTokensSemantico[llenadorDePila + 1].Lexema });
                        }
                    }
                    if (expresionInfijo[0].Lexema == "leer")
                    {
                        break;
                    }
                    if (expresionInfijo.Count == 1)
                    {
                        if (listaDeVariablesAuxiliar[nodo].Tipo == listaTokensSemantico[puntero + 1].TipoToken.ToString() ||
                            listaTokensSemantico[puntero + 1].TipoToken.ToString() == "Identificador" || listaTokensSemantico[puntero + 1].Lexema.ToString() == "falso" ||
                            listaTokensSemantico[puntero + 1].Lexema.ToString() == "verdadero")
                        {
                            break;
                        }
                        else
                        {
                            NuevoError(-702, listaTokensSemantico[puntero - 1].Lexema); //Incompatibilidad de tipos.
                            break;
                        }
                    }
                    else
                    {
                        if (listaErrorSemantico.Count == 0)
                        {
                            expresionPostfijo = InfijoAPosfijo(expresionInfijo); // Infijo a Postfijo para validar el tipo de dato.
                            tipoDeOperacionIngresada = ValidacionDeTipos(expresionPostfijo);
                            if (listaDeVariablesAuxiliar[nodo].Tipo != tipoDeOperacionIngresada)
                            {
                                NuevoError(-702, listaTokensSemantico[puntero - 1].Lexema); //Incompatibilidad de tipos.
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (nodo == listaDeVariablesAuxiliar.Count - 1)
                    {
                        NuevoError(-701, listaTokensSemantico[puntero - 1].Lexema); //Variable no decalrada.
                    }
                }
            }
        }

        public void AgregarVariable(List<Variables> listaDeVariables, int puntero)
        {
            List<Variables> listaDeVariablesAuxiliar = listaDeVariables.ToList();
            int nodos = listaDeVariablesAuxiliar.Count;
            int posicionListaAuxiliar = 0;

            if (listaDeVariables.Count != 0)
            {
                for (int contador = 1; contador <= nodos; contador++)
                {
                    if (listaTokensSemantico[puntero + 1].Lexema != listaDeVariablesAuxiliar[posicionListaAuxiliar].Lexema)
                    {
                        if (contador == nodos)
                        {
                            listaDeVariables.Add(new Variables { Tipo = tempTipo, Lexema = listaTokensSemantico[puntero + 1].Lexema });
                        }
                        else
                        {
                            listaDeVariablesAuxiliar.RemoveAt(0);
                        }
                    }
                    else
                    {
                        NuevoError(-700, listaTokensSemantico[puntero + 1].Lexema); //Variable ya declarada
                    }
                }
            }
            else
            {
                listaDeVariables.Add(new Variables { Tipo = tempTipo, Lexema = listaTokensSemantico[puntero + 1].Lexema });
            }
        }

        public Error ManejoDeErrores(int codigo, string variable)
        {
            string mensajeError = "";
            switch (codigo)
            {
                case -700:
                    mensajeError = $"Variable \"{variable}\" ya declarada.";
                    break;
                case -701:
                    mensajeError = $"Variable \"{variable}\" no declarada.";
                    break;
                case -702:
                    mensajeError = $"El tipo de la variable \"{variable}\" no coincide.";
                    break;
                case -605:
                    mensajeError = $"Error de declaración de variable \"{variable}\".";
                    break;
                case -703:
                    mensajeError = "Condicional no retorna valor Lógico.";
                    break;
                default:
                    break;
            }
            return new Error() { Codigo = codigo, MensajeError = mensajeError, Tipo = tipoError.Semantico, Linea = lineaSemantico - 1 };
        }

        public void NuevoError(int codigo, string variable)
        {
            var nuevoError = ManejoDeErrores(codigo, variable);
            listaErrorSemantico.Clear();
            listaErrorSemantico.Add(nuevoError);
        }

        public List<Variables> InfijoAPosfijo(List<Variables> listaOperacionInfijo)
        {
            expresionPostfijo.Clear();
            for (int i = 0; i < listaOperacionInfijo.Count; i++)
            {
                if (listaOperacionInfijo[i].Tipo == "Identificador"
                    || listaOperacionInfijo[i].Tipo == "NumeroEntero" || listaOperacionInfijo[i].Tipo == "NumeroDecimal" || listaOperacionInfijo[i].Tipo == "Cadena")
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
            /*string stringPostfijo = null;
            for (int i = 0; i < expresionPostfijo.Count; i++)
            {
                stringPostfijo += expresionPostfijo[i].Lexema;
            }
            MessageBox.Show($"{stringPostfijo}");*/
            return expresionPostfijo;
        }

        public string ValidacionDeTipos(List<Variables> expresionPostfijoParaValidar)
        {
            List<string> pilaParaValidar = new List<string>();
            var sistemaDeTipos = new SistemaDeTipos();
            int operando1, operando2;
            for (int i = 0; i < expresionPostfijoParaValidar.Count; i++)
            {
                if (expresionPostfijoParaValidar[i].Tipo == "NumeroEntero" || expresionPostfijoParaValidar[i].Tipo == "NumeroDecimal" ||
                    expresionPostfijoParaValidar[i].Tipo == "Identificador" || expresionPostfijoParaValidar[i].Tipo == "Cadena" || expresionPostfijoParaValidar[i].Tipo == "Booleano")
                {
                    pilaParaValidar.Add(expresionPostfijoParaValidar[i].Tipo);
                }
                else
                {
                    if (expresionPostfijoParaValidar[i].Lexema == "+")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposSuma(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "-")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposResta(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "*")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposMultiplicacion(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "/")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposDivision(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == ">")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposMayorQue(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "<")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposMenorQue(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "<=")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposMenorOIgualQue(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == ">=")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposMayorOIgualQue(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "==")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposIgualA(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "!=")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposDiferenteA(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "&&")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposAND(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "||")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        operando2 = Operador2ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposOR(operando1, operando2));
                    }
                    if (expresionPostfijoParaValidar[i].Lexema == "!!")
                    {
                        operando1 = Operador1ParaSistemaDeTipos(pilaParaValidar);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.RemoveAt(pilaParaValidar.Count - 1);
                        pilaParaValidar.Add(sistemaDeTipos.SistemaDeTiposNOT(operando1));
                    }
                }
            }
            if (pilaParaValidar.Count == 1)
            {
                return pilaParaValidar[0];
            }
            else
            {
                return null;
            }
        }

        public int Operador1ParaSistemaDeTipos(List<string> pilaParaValidar)
        {
            int operando1 = 0;
            int i = pilaParaValidar.Count - 1;
            if (pilaParaValidar[i - 1] == "NumeroEntero")
            {
                operando1 = 0;
            }
            if (pilaParaValidar[i - 1] == "NumeroDecimal")
            {
                operando1 = 1;
            }
            if (pilaParaValidar[i - 1] == "Cadena")
            {
                operando1 = 2;
            }
            if (pilaParaValidar[i - 1] == "Identificador")
            {
                operando1 = 2;
            }
            if (pilaParaValidar[i - 1] == "Booleano")
            {
                operando1 = 3;
            }
            return operando1;
        }

        public int Operador2ParaSistemaDeTipos(List<string> pilaParaValidar)
        {
            int operando2 = 0;
            int i = pilaParaValidar.Count - 1;
            if (pilaParaValidar[i] == "NumeroEntero")
            {
                operando2 = 0;
            }
            if (pilaParaValidar[i] == "NumeroDecimal")
            {
                operando2 = 1;
            }
            if (pilaParaValidar[i] == "Cadena")
            {
                operando2 = 2;
            }
            if (pilaParaValidar[i] == "Identificador")
            {
                operando2 = 2;
            }
            if (pilaParaValidar[i] == "Booleano")
            {
                operando2 = 3;
            }
            return operando2;
        }

        public void ValidacionDeTiposRelacional(List<Variables> listaDeVariables, int puntero)
        {
            List<Variables> listaDeVariablesAuxiliar = listaDeVariables.ToList();
            int nodos = listaDeVariablesAuxiliar.Count;
            expresionDerecha.Clear();
            expresionDerechaEnPostfijo.Clear();
            expresionIzquierda.Clear();
            expresionIzquierdaEnPostfijo.Clear();
            expresionCondicionalEnInfijo.Clear();
            condicionalEnInfijo.Clear();
            int punteroDeCondicional = puntero;
            string tipoDeExpresionIzquierdaEnPostfijo = null, tipoDeExpresionDerechaEnPostfijo = null;
            int operador = 0;
            bool esCondicionalLogico = false;
            do
            {
                expresionCondicionalEnInfijo.Add(new Variables
                {
                    Tipo = listaTokensSemantico[punteroDeCondicional + 1].TipoToken.ToString(),
                    Lexema = listaTokensSemantico[punteroDeCondicional + 1].Lexema
                });
                punteroDeCondicional++;
            } while (listaTokensSemantico[punteroDeCondicional + 1].ValorToken != -29);
            for (int contadorExpCond = 0; contadorExpCond < expresionCondicionalEnInfijo.Count; contadorExpCond++)
            {
                if (expresionCondicionalEnInfijo[contadorExpCond].Lexema == "&&" || expresionCondicionalEnInfijo[contadorExpCond].Lexema == "||" ||
                    expresionCondicionalEnInfijo[contadorExpCond].Lexema == "!!")
                {
                    operador = contadorExpCond;
                    esCondicionalLogico = true;
                    for (int i = contadorExpCond; i > 0; i--)
                    {
                        if (expresionCondicionalEnInfijo[i - 1].Tipo == "Identificador")
                        {
                            for (int contador = 0; contador < nodos; contador++)
                            {
                                if (expresionCondicionalEnInfijo[i - 1].Lexema == listaDeVariablesAuxiliar[contador].Lexema)
                                {
                                    expresionIzquierda.Add(new Variables { Tipo = listaDeVariablesAuxiliar[contador].Tipo, Lexema = listaDeVariablesAuxiliar[contador].Lexema });
                                    break;
                                }
                                if (contador + 1 == nodos)
                                {
                                    NuevoError(-605, expresionCondicionalEnInfijo[i - 1].Lexema); // Variable no declarada
                                }
                            }
                        }
                        else
                        {
                            expresionIzquierda.Add(expresionCondicionalEnInfijo[i - 1]);
                        }
                    }
                    expresionIzquierda.Reverse();
                    for (int i = contadorExpCond; i < expresionCondicionalEnInfijo.Count - 1; i++)
                    {
                        if (expresionCondicionalEnInfijo[i + 1].Tipo == "Identificador")
                        {
                            for (int contador = 0; contador < nodos; contador++)
                            {
                                if (expresionCondicionalEnInfijo[i + 1].Lexema == listaDeVariablesAuxiliar[contador].Lexema)
                                {
                                    expresionDerecha.Add(new Variables { Tipo = listaDeVariablesAuxiliar[contador].Tipo, Lexema = listaDeVariablesAuxiliar[contador].Lexema });
                                    break;
                                }
                                if (contador + 1 == nodos)
                                {
                                    NuevoError(-605, expresionCondicionalEnInfijo[i + 1].Lexema); // Variable no declarada
                                    break;
                                }
                            }
                        }
                        else
                        {
                            expresionDerecha.Add(expresionCondicionalEnInfijo[i + 1]);
                        }
                    }
                }
            }
            if (!esCondicionalLogico)
            {
                for (int contadorExpCond = 0; contadorExpCond < expresionCondicionalEnInfijo.Count - 1; contadorExpCond++)
                {
                    if (expresionCondicionalEnInfijo[contadorExpCond].Lexema == ">" || expresionCondicionalEnInfijo[contadorExpCond].Lexema == "<" || expresionCondicionalEnInfijo[contadorExpCond].Lexema == "<="
                        || expresionCondicionalEnInfijo[contadorExpCond].Lexema == ">=" || expresionCondicionalEnInfijo[contadorExpCond].Lexema == "==" || expresionCondicionalEnInfijo[contadorExpCond].Lexema == "!=")
                    {
                        operador = contadorExpCond;
                        for (int i = contadorExpCond; i > 0; i--)
                        {
                            expresionIzquierda.Add(expresionCondicionalEnInfijo[i - 1]);
                        }
                        expresionIzquierda.Reverse();
                        for (int i = contadorExpCond; i < expresionCondicionalEnInfijo.Count - 1; i++)
                        {
                            expresionDerecha.Add(expresionCondicionalEnInfijo[i + 1]);
                        }
                    }
                }
            }
            if (listaErrorSemantico.Count == 0)
            {
                if (expresionIzquierda.Count == 1)
                {
                    if (expresionIzquierda[0].Tipo == "Identificador")
                    {
                        for (int contador = 0; contador < nodos; contador++)
                        {
                            if (expresionIzquierda[0].Lexema == listaDeVariablesAuxiliar[contador].Lexema)
                            {
                                condicionalEnInfijo.Add(listaDeVariablesAuxiliar[contador]);
                                condicionalEnInfijo.Add(new Variables { Tipo = expresionCondicionalEnInfijo[operador].Tipo.ToString(), Lexema = expresionCondicionalEnInfijo[operador].Lexema });
                                break;
                            }
                            if (contador + 1 == nodos)
                            {
                                NuevoError(-701, listaTokensSemantico[puntero - 1].Lexema); //Variable no declarada
                            }
                        }
                    }
                    else
                    {
                        condicionalEnInfijo.Add(expresionIzquierda[0]);
                        condicionalEnInfijo.Add(new Variables { Tipo = expresionCondicionalEnInfijo[operador/*punteroDeCondicional - 1*/].Tipo.ToString(), Lexema = expresionCondicionalEnInfijo[operador/*punteroDeCondicional - 1*/].Lexema });
                    }
                }
                else
                {
                    if (listaErrorSemantico.Count == 0)
                    {
                        expresionIzquierdaEnPostfijo = InfijoAPosfijo(expresionIzquierda);
                        tipoDeExpresionIzquierdaEnPostfijo = ValidacionDeTipos(expresionIzquierdaEnPostfijo);
                        if (tipoDeExpresionIzquierdaEnPostfijo == null)
                        {
                            NuevoError(-703, "si");
                        }
                    }
                }
                if (expresionDerecha.Count == 1)
                {
                    if (expresionDerecha[0].Tipo == "Identificador")
                    {
                        for (int contador = 0; contador < nodos; contador++)
                        {
                            if (expresionDerecha[0].Lexema == listaDeVariablesAuxiliar[contador].Lexema)
                            {
                                condicionalEnInfijo.Add(listaDeVariablesAuxiliar[contador]);
                                break;
                            }
                            if (contador + 1 == nodos)
                            {
                                NuevoError(-701, expresionCondicionalEnInfijo[contador - 1].Lexema); //Variable no declarada
                            }
                        }
                    }
                    else
                    {
                        condicionalEnInfijo.Add(expresionDerecha[0]);
                    }
                }
                else
                {
                    if (listaErrorSemantico.Count == 0)
                    {
                        expresionDerechaEnPostfijo = InfijoAPosfijo(expresionDerecha);
                        tipoDeExpresionDerechaEnPostfijo = ValidacionDeTipos(expresionDerechaEnPostfijo);
                        if (tipoDeExpresionDerechaEnPostfijo == null)
                        {
                            NuevoError(-703, "si"); // Condicional no es logico
                        }
                    }
                }
                if (listaErrorSemantico.Count == 0)
                {
                    if (condicionalEnInfijo.Count == 0)
                    {
                        condicionalEnInfijo.Add(new Variables { Tipo = tipoDeExpresionIzquierdaEnPostfijo });
                        condicionalEnInfijo.Add(new Variables { Tipo = tipoDeExpresionDerechaEnPostfijo });
                        condicionalEnInfijo.Add(new Variables { Tipo = expresionCondicionalEnInfijo[operador].Tipo, Lexema = expresionCondicionalEnInfijo[operador].Lexema });
                    }
                    condicionalEnPostfijo = InfijoAPosfijo(condicionalEnInfijo);
                    tipoDeCondicionalCompleta = ValidacionDeTipos(condicionalEnPostfijo);
                    if (tipoDeCondicionalCompleta != "Booleano")
                    {
                        NuevoError(-703, "si");
                    }
                }
            }
        }

        #region FuncionesParaInfijoAPostfijo
        public void MeterALaPila(Variables dato)
        {
            if (PilaLlena() != true)
            {
                tope = tope + 1;
                pila.Add(dato);
            }
        }

        public Variables SacarDeLaPila()
        {
            if (PilaVacia() != true)
            {
                Variables aux = pila[tope];
                pila.Remove(pila[tope]);
                tope = tope - 1;
                return aux;
            }
            return null;
        }

        public Variables SacarDeLaPilaCheck()
        {
            if (PilaVacia() != true)
            {
                Variables aux = pila[tope - 1];
                pila.Remove(pila[tope - 1]);
                tope = tope - 1;
                return aux;
            }
            return null;
        }

        public int Infijo(int i, List<Variables> token)
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

        public int PrioridadPila(List<Variables> pila)
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
        public int PrioridadPilaCheck(List<Variables> pila)
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
        #endregion FuncionesParaInfijoAPostfijo

    }
}
