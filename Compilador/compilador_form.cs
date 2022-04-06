using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            semantico.ejecutarSemantico(semantico.listaTokensSemantico);

            List<Error> listaErroresLexico = lexico.listaErrorLexico;
            List<Error> listaErroresSintactico = objSintactico.listaErrorSintactico;
            List<Error> listaErrores = listaErroresLexico.Union(listaErroresSintactico).ToList(); //Lista de Errores Lexicos y Sintacticos
            List<Error> listaErroresSemantico = semantico.listaErrorSemantico; //Lista de Errores

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
            }

            //dgvSemantico.DataSource = null;
            //dgvSemantico.DataSource = listaSemantico;
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
