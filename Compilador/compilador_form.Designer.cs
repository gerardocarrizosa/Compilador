
namespace Compilador
{
    partial class compilador_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(compilador_form));
            this.menu_mStrip = new System.Windows.Forms.MenuStrip();
            this.compiladorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvLexico = new System.Windows.Forms.DataGridView();
            this.lexico_Lbl = new System.Windows.Forms.Label();
            this.dgvErrores = new System.Windows.Forms.DataGridView();
            this.errores_Lbl = new System.Windows.Forms.Label();
            this.dgvSemantico = new System.Windows.Forms.DataGridView();
            this.semantico_Lbl = new System.Windows.Forms.Label();
            this.txtCodigoFuente = new System.Windows.Forms.TextBox();
            this.menu_mStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLexico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemantico)).BeginInit();
            this.SuspendLayout();
            // 
            // menu_mStrip
            // 
            this.menu_mStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compiladorToolStripMenuItem});
            this.menu_mStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menu_mStrip.Location = new System.Drawing.Point(0, 0);
            this.menu_mStrip.Name = "menu_mStrip";
            this.menu_mStrip.Size = new System.Drawing.Size(1135, 24);
            this.menu_mStrip.TabIndex = 0;
            this.menu_mStrip.Text = "menuStrip1";
            // 
            // compiladorToolStripMenuItem
            // 
            this.compiladorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ejecutarToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cerrarToolStripMenuItem});
            this.compiladorToolStripMenuItem.Name = "compiladorToolStripMenuItem";
            this.compiladorToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.compiladorToolStripMenuItem.Text = "Compilador";
            // 
            // ejecutarToolStripMenuItem
            // 
            this.ejecutarToolStripMenuItem.Name = "ejecutarToolStripMenuItem";
            this.ejecutarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.ejecutarToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.ejecutarToolStripMenuItem.Text = "Ejecutar";
            this.ejecutarToolStripMenuItem.Click += new System.EventHandler(this.ejecutarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // dgvLexico
            // 
            this.dgvLexico.AllowUserToAddRows = false;
            this.dgvLexico.AllowUserToDeleteRows = false;
            this.dgvLexico.AllowUserToResizeRows = false;
            this.dgvLexico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLexico.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvLexico.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLexico.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLexico.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLexico.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLexico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLexico.Location = new System.Drawing.Point(659, 60);
            this.dgvLexico.Name = "dgvLexico";
            this.dgvLexico.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLexico.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLexico.RowHeadersVisible = false;
            this.dgvLexico.Size = new System.Drawing.Size(464, 157);
            this.dgvLexico.TabIndex = 2;
            // 
            // lexico_Lbl
            // 
            this.lexico_Lbl.AutoSize = true;
            this.lexico_Lbl.BackColor = System.Drawing.Color.Transparent;
            this.lexico_Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lexico_Lbl.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lexico_Lbl.Location = new System.Drawing.Point(654, 32);
            this.lexico_Lbl.Name = "lexico_Lbl";
            this.lexico_Lbl.Size = new System.Drawing.Size(75, 25);
            this.lexico_Lbl.TabIndex = 3;
            this.lexico_Lbl.Text = "Léxico";
            // 
            // dgvErrores
            // 
            this.dgvErrores.AllowUserToAddRows = false;
            this.dgvErrores.AllowUserToDeleteRows = false;
            this.dgvErrores.AllowUserToOrderColumns = true;
            this.dgvErrores.AllowUserToResizeRows = false;
            this.dgvErrores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvErrores.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvErrores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvErrores.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvErrores.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvErrores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrores.Location = new System.Drawing.Point(659, 436);
            this.dgvErrores.Name = "dgvErrores";
            this.dgvErrores.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvErrores.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvErrores.RowHeadersVisible = false;
            this.dgvErrores.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvErrores.Size = new System.Drawing.Size(464, 157);
            this.dgvErrores.TabIndex = 6;
            this.dgvErrores.TabStop = false;
            // 
            // errores_Lbl
            // 
            this.errores_Lbl.AutoSize = true;
            this.errores_Lbl.BackColor = System.Drawing.Color.Transparent;
            this.errores_Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.errores_Lbl.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errores_Lbl.Location = new System.Drawing.Point(654, 408);
            this.errores_Lbl.Name = "errores_Lbl";
            this.errores_Lbl.Size = new System.Drawing.Size(82, 25);
            this.errores_Lbl.TabIndex = 7;
            this.errores_Lbl.Text = "Errores";
            // 
            // dgvSemantico
            // 
            this.dgvSemantico.AllowUserToAddRows = false;
            this.dgvSemantico.AllowUserToDeleteRows = false;
            this.dgvSemantico.AllowUserToResizeRows = false;
            this.dgvSemantico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSemantico.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvSemantico.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSemantico.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSemantico.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSemantico.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSemantico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSemantico.Location = new System.Drawing.Point(659, 248);
            this.dgvSemantico.Name = "dgvSemantico";
            this.dgvSemantico.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSemantico.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSemantico.RowHeadersVisible = false;
            this.dgvSemantico.Size = new System.Drawing.Size(464, 157);
            this.dgvSemantico.TabIndex = 8;
            // 
            // semantico_Lbl
            // 
            this.semantico_Lbl.AutoSize = true;
            this.semantico_Lbl.BackColor = System.Drawing.Color.Transparent;
            this.semantico_Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.semantico_Lbl.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.semantico_Lbl.Location = new System.Drawing.Point(654, 220);
            this.semantico_Lbl.Name = "semantico_Lbl";
            this.semantico_Lbl.Size = new System.Drawing.Size(113, 25);
            this.semantico_Lbl.TabIndex = 9;
            this.semantico_Lbl.Text = "Semántico";
            // 
            // txtCodigoFuente
            // 
            this.txtCodigoFuente.BackColor = System.Drawing.SystemColors.Control;
            this.txtCodigoFuente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCodigoFuente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoFuente.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoFuente.Location = new System.Drawing.Point(12, 32);
            this.txtCodigoFuente.Multiline = true;
            this.txtCodigoFuente.Name = "txtCodigoFuente";
            this.txtCodigoFuente.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCodigoFuente.Size = new System.Drawing.Size(636, 561);
            this.txtCodigoFuente.TabIndex = 10;
            this.txtCodigoFuente.Text = resources.GetString("txtCodigoFuente.Text");
            // 
            // compilador_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1135, 603);
            this.Controls.Add(this.txtCodigoFuente);
            this.Controls.Add(this.semantico_Lbl);
            this.Controls.Add(this.dgvSemantico);
            this.Controls.Add(this.errores_Lbl);
            this.Controls.Add(this.dgvErrores);
            this.Controls.Add(this.lexico_Lbl);
            this.Controls.Add(this.dgvLexico);
            this.Controls.Add(this.menu_mStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu_mStrip;
            this.MaximizeBox = false;
            this.Name = "compilador_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lenguajes y Autómatas II";
            this.Load += new System.EventHandler(this.compilador_form_Load);
            this.menu_mStrip.ResumeLayout(false);
            this.menu_mStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLexico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemantico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu_mStrip;
        private System.Windows.Forms.ToolStripMenuItem compiladorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejecutarToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvLexico;
        private System.Windows.Forms.Label lexico_Lbl;
        private System.Windows.Forms.DataGridView dgvErrores;
        private System.Windows.Forms.Label errores_Lbl;
        private System.Windows.Forms.DataGridView dgvSemantico;
        private System.Windows.Forms.Label semantico_Lbl;
        private System.Windows.Forms.TextBox txtCodigoFuente;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
    }
}

