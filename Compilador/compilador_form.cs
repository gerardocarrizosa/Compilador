using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Compilador
{
    public partial class compilador_form : Form
    {
        public compilador_form()
        {
            InitializeComponent();
        }

        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lexico = new Lexico(txtCodigoFuente.Text);
            lexico.EjecutarLexico();

            var objSintactico = new Sintactico(lexico.listaToken);
            objSintactico.EjecutarSintactico(objSintactico.listaTokens);

            var semantico = new Semantico(objSintactico.listaTokens, lexico.linea);
            List<Variables> variables = new List<Variables>();
            variables = semantico.ejecutarSemantico(semantico.listaTokensSemantico);

            List<Error> listaErroresLexico = lexico.listaErrorLexico;
            List<Error> listaErroresSintactico = objSintactico.listaErrorSintactico;
            List<Error> listaErrores = listaErroresLexico.Union(listaErroresSintactico).ToList(); //Lista de Errores Lexicos y Sintacticos
            List<Error> listaErroresSemantico = semantico.listaErrorSemantico; //Lista de Errores Totales
            List<string> CodigoEnPolish = new List<string>();

            var lista = new BindingList<Token>(lexico.listaToken);
            var listaSemantico = new BindingList<Variables>(semantico.variables.ToList());

            dgvLexico.DataSource = null;
            dgvLexico.DataSource = lista;

            if (listaErrores.Count != 0) {
                dgvErrores.DataSource = null;
                dgvErrores.DataSource = listaErrores;
                dgvSemantico.DataSource = null;
            }
            if (listaErroresSemantico.Count != 0) {
                dgvErrores.DataSource = null;
                dgvErrores.DataSource = listaErroresSemantico;
            }
            if (listaErrores.Count == 0 && listaErroresSemantico.Count == 0) {
                MessageBox.Show("Código compilado correctamente");
                dgvErrores.DataSource = null;
                dgvSemantico.DataSource = null;
                dgvSemantico.DataSource = listaSemantico;

                var polish = new ListaPolish(semantico.listaTokensSemantico, lexico.linea);
                CodigoEnPolish = polish.ejecutarPolish(polish.listaTokensPolish, 11);
                string stringPolish = "";
                for (int i = 0; i < CodigoEnPolish.Count; i++)
                {
                    stringPolish += CodigoEnPolish[i];
                    stringPolish += " | ";
                }
                MessageBox.Show(stringPolish,"Polish");

                /*------Crear Archivo ASM------------------------------------------------------------------------------------*/

                Stack<string> pilaVariables = new Stack<string>();

                string path = "C:/Users/gerga/ITH//ASM/compi.asm";

                string stringEnsamblador = "\nINCLUDE MACROS.MAC\n" +
                                           "DOSSEG\n" +
                                           ".MODEL SMALL\n" +
                                           ".STACK 100h\n" +
                                           ".DATA";

                for (int i = 0; i < variables.Count; i++)
                {
                    if (variables[i].Tipo == "Cadena")
                    {
                        string strAux = null;
                        for (int j = 0; j < CodigoEnPolish.Count; j++)
                        {
                            if (CodigoEnPolish[j] == "= " && CodigoEnPolish[j - 2] == variables[i].Lexema)
                            {
                                strAux = CodigoEnPolish[j - 1];
                                break;
                            }
                        }
                        stringEnsamblador += "\n\t\t\t" + variables[i].Lexema + " db " + strAux + ", 13, 10,'$'";
                    }
                    else
                    {
                        stringEnsamblador += "\n\t\t\t" + variables[i].Lexema + " dw ?";
                    }
                }
                stringEnsamblador += "\n\t\t\t;/variables";
                stringEnsamblador += "\n.CODE\n" +
                                     ".386\n" +
                                     "BEGIN:\n" +
                                     "\t\t\tMOV     AX, @DATA\n" +
                                     "\t\t\tMOV     DS, AX\n" +
                                     "CALL  COMPI\n" +
                                     "\t\t\tMOV AX, 4C00H\n" +
                                     "\t\t\tINT 21H\n" +
                                     "COMPI PROC\n";
                int contadorOperaciones = 1;
                for (int i = 0; i < CodigoEnPolish.Count; i++)
                {
                    if (CodigoEnPolish[i] == "+")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tSUMAR " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw " + "?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == "-")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tRESTA " + operando1 + " " + operando2 + " " + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == "*")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tMULTI " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == "/")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tDIVIDE " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i].StartsWith("'") && CodigoEnPolish[i].EndsWith("'"))
                    {
                        pilaVariables.Push(CodigoEnPolish[i].Substring(1, CodigoEnPolish[i].Length - 2)); // es string
                    }
                    else if (CodigoEnPolish[i] == "= ")
                    {
                        if (stringEnsamblador.Contains(CodigoEnPolish[i - 1] + ", 13, 10,'$'"))
                        {
                            pilaVariables.Pop();
                            pilaVariables.Pop();
                            continue;
                        }
                        else
                        {
                            var operando2 = pilaVariables.Pop();
                            var operando1 = pilaVariables.Pop();

                            stringEnsamblador += "\t\t\tI_ASIGNAR " + operando1 + "," + operando2 + "\n";
                        }

                    }
                    else if (CodigoEnPolish[i] == ">")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tI_MAYOR " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == ">=")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tI_MAYORIGUAL " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == "<")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tI_MENOR " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == "<=")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tI_MENORIGUAL " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == "==")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tI_IGUAL " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == "!=")
                    {
                        var operando2 = pilaVariables.Pop();
                        var operando1 = pilaVariables.Pop();

                        stringEnsamblador += "\t\t\tI_DIFERENTES " + operando1 + "," + operando2 + "," + "resultado" + contadorOperaciones + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i] == "imprimir")
                    {
                        string operando1 = pilaVariables.Pop();
                        if (stringEnsamblador.Contains(operando1))
                        {
                            stringEnsamblador += "\t\t\tWRITE " + operando1 + "\n";
                        }
                        else
                        {
                            string resultado = "resultado" + contadorOperaciones;
                            pilaVariables.Push(resultado);
                            stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " db '" + operando1 + "','$'\n\t\t\t;/variables");
                            stringEnsamblador += "\t\t\tWRITE " + resultado + "\n";
                            contadorOperaciones++;
                        }
                    }
                    else if (CodigoEnPolish[i] == "leer")
                    {
                        stringEnsamblador += "\t\t\treadinteger@ " + "VALOR1@" + "\n";
                        string resultado = "resultado" + contadorOperaciones;
                        pilaVariables.Push(resultado);
                        stringEnsamblador = stringEnsamblador.Replace(";/variables", resultado + " dw ?" + "\n\t\t\t;/variables");
                        contadorOperaciones++;
                    }
                    else if (CodigoEnPolish[i].Contains("BRF"))
                    {
                        string Apuntador = CodigoEnPolish[i].Remove(0, 4);
                        int temp = contadorOperaciones;

                        stringEnsamblador += "JF resultado" + (temp - 1) + "," + Apuntador + "\n";
                    }
                    else if (CodigoEnPolish[i].Contains("BRI"))
                    {
                        string Apuntador = CodigoEnPolish[i].Remove(0, 4);

                        stringEnsamblador += "JMP " + Apuntador + "\n";
                    }
                    else if (CodigoEnPolish[i].Contains("@"))
                    {
                        string Apuntador = CodigoEnPolish[i].Remove(0, 1);

                        stringEnsamblador += Apuntador + ":\n";
                    }
                    else if (CodigoEnPolish[i] == "BRV-G")
                    {
                        string Apuntador = CodigoEnPolish[i].Remove(0, 4);
                        int temp = contadorOperaciones;

                        stringEnsamblador += "JF resultado" + (temp - 1) + "," + Apuntador + "\n";
                    }
                    else if (CodigoEnPolish[i] == "FIN")
                    {
                        continue;
                    }
                    else if (CodigoEnPolish[i] == "falso")
                    {
                        var operando1 = pilaVariables.Pop();

                    }
                    else if (CodigoEnPolish[i] == "verdadero")
                    {
                        continue;
                    }
                    else
                    {
                        pilaVariables.Push(CodigoEnPolish[i]); // es variable
                    }
                }

                stringEnsamblador += "\t\t\tret\n" +
                                     "COMPI ENDP\n" +
                                     "END BEGIN\n";
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    using (FileStream fs = File.Create(path))
                    {
                        Byte[] text = new UTF8Encoding(true).GetBytes(stringEnsamblador);
                        fs.Write(text, 0, text.Length);
                        MessageBox.Show("Archivo .asm generado!", "Aviso");
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }
        }

        private void compilador_form_Load(object sender, EventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
