namespace PlanillaAsistencia.ControlesPersonalizados
{
    partial class TripleGrillaAsistencias
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

        #region Component Designer generated code

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
            this.tabDatosDelDia = new System.Windows.Forms.TabControl();
            this.tabTurnoManana = new System.Windows.Forms.TabPage();
            this.dgvTurnoManana = new System.Windows.Forms.DataGridView();
            this.tabTurnoTarde = new System.Windows.Forms.TabPage();
            this.dgvTurnoTarde = new System.Windows.Forms.DataGridView();
            this.tabTurnoNoche = new System.Windows.Forms.TabPage();
            this.dgvTurnoNoche = new System.Windows.Forms.DataGridView();
            this.tabDatosDelDia.SuspendLayout();
            this.tabTurnoManana.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnoManana)).BeginInit();
            this.tabTurnoTarde.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnoTarde)).BeginInit();
            this.tabTurnoNoche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnoNoche)).BeginInit();
            this.SuspendLayout();
            // 
            // tabDatosDelDia
            // 
            this.tabDatosDelDia.CausesValidation = false;
            this.tabDatosDelDia.Controls.Add(this.tabTurnoManana);
            this.tabDatosDelDia.Controls.Add(this.tabTurnoTarde);
            this.tabDatosDelDia.Controls.Add(this.tabTurnoNoche);
            this.tabDatosDelDia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDatosDelDia.Location = new System.Drawing.Point(0, 0);
            this.tabDatosDelDia.Name = "tabDatosDelDia";
            this.tabDatosDelDia.SelectedIndex = 0;
            this.tabDatosDelDia.Size = new System.Drawing.Size(780, 247);
            this.tabDatosDelDia.TabIndex = 10;
            this.tabDatosDelDia.SelectedIndexChanged += new System.EventHandler(this.tabDatosDelDia_SelectedIndexChanged);
            // 
            // tabTurnoManana
            // 
            this.tabTurnoManana.Controls.Add(this.dgvTurnoManana);
            this.tabTurnoManana.Location = new System.Drawing.Point(4, 22);
            this.tabTurnoManana.Name = "tabTurnoManana";
            this.tabTurnoManana.Size = new System.Drawing.Size(772, 221);
            this.tabTurnoManana.TabIndex = 0;
            this.tabTurnoManana.Text = "Mañana";
            this.tabTurnoManana.UseVisualStyleBackColor = true;
            // 
            // dgvTurnoManana
            // 
            this.dgvTurnoManana.AllowUserToAddRows = false;
            this.dgvTurnoManana.AllowUserToDeleteRows = false;
            this.dgvTurnoManana.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTurnoManana.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTurnoManana.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTurnoManana.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTurnoManana.Location = new System.Drawing.Point(0, 0);
            this.dgvTurnoManana.MultiSelect = false;
            this.dgvTurnoManana.Name = "dgvTurnoManana";
            this.dgvTurnoManana.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTurnoManana.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTurnoManana.RowHeadersVisible = false;
            this.dgvTurnoManana.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvTurnoManana.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnoManana.Size = new System.Drawing.Size(772, 221);
            this.dgvTurnoManana.TabIndex = 1;
            this.dgvTurnoManana.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // tabTurnoTarde
            // 
            this.tabTurnoTarde.Controls.Add(this.dgvTurnoTarde);
            this.tabTurnoTarde.Location = new System.Drawing.Point(4, 22);
            this.tabTurnoTarde.Name = "tabTurnoTarde";
            this.tabTurnoTarde.Size = new System.Drawing.Size(772, 221);
            this.tabTurnoTarde.TabIndex = 1;
            this.tabTurnoTarde.Text = "Tarde";
            this.tabTurnoTarde.UseVisualStyleBackColor = true;
            // 
            // dgvTurnoTarde
            // 
            this.dgvTurnoTarde.AllowUserToAddRows = false;
            this.dgvTurnoTarde.AllowUserToDeleteRows = false;
            this.dgvTurnoTarde.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTurnoTarde.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTurnoTarde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTurnoTarde.Location = new System.Drawing.Point(0, 0);
            this.dgvTurnoTarde.MultiSelect = false;
            this.dgvTurnoTarde.Name = "dgvTurnoTarde";
            this.dgvTurnoTarde.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTurnoTarde.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTurnoTarde.RowHeadersVisible = false;
            this.dgvTurnoTarde.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvTurnoTarde.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnoTarde.Size = new System.Drawing.Size(772, 221);
            this.dgvTurnoTarde.TabIndex = 2;
            this.dgvTurnoTarde.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // tabTurnoNoche
            // 
            this.tabTurnoNoche.Controls.Add(this.dgvTurnoNoche);
            this.tabTurnoNoche.Location = new System.Drawing.Point(4, 22);
            this.tabTurnoNoche.Name = "tabTurnoNoche";
            this.tabTurnoNoche.Size = new System.Drawing.Size(772, 221);
            this.tabTurnoNoche.TabIndex = 2;
            this.tabTurnoNoche.Text = "Noche";
            this.tabTurnoNoche.UseVisualStyleBackColor = true;
            // 
            // dgvTurnoNoche
            // 
            this.dgvTurnoNoche.AllowUserToAddRows = false;
            this.dgvTurnoNoche.AllowUserToDeleteRows = false;
            this.dgvTurnoNoche.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvTurnoNoche.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTurnoNoche.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTurnoNoche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTurnoNoche.Location = new System.Drawing.Point(0, 0);
            this.dgvTurnoNoche.MultiSelect = false;
            this.dgvTurnoNoche.Name = "dgvTurnoNoche";
            this.dgvTurnoNoche.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTurnoNoche.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTurnoNoche.RowHeadersVisible = false;
            this.dgvTurnoNoche.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvTurnoNoche.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnoNoche.Size = new System.Drawing.Size(772, 221);
            this.dgvTurnoNoche.TabIndex = 2;
            this.dgvTurnoNoche.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgvTurnoNoche.CellStyleChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTurnoNoche_CellStyleChanged);
            // 
            // TripleGrillaAsistencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabDatosDelDia);
            this.Name = "TripleGrillaAsistencias";
            this.Size = new System.Drawing.Size(780, 247);
            this.tabDatosDelDia.ResumeLayout(false);
            this.tabTurnoManana.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnoManana)).EndInit();
            this.tabTurnoTarde.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnoTarde)).EndInit();
            this.tabTurnoNoche.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnoNoche)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabDatosDelDia;
        private System.Windows.Forms.TabPage tabTurnoManana;
        private System.Windows.Forms.DataGridView dgvTurnoManana;
        private System.Windows.Forms.TabPage tabTurnoTarde;
        private System.Windows.Forms.DataGridView dgvTurnoTarde;
        private System.Windows.Forms.TabPage tabTurnoNoche;
        private System.Windows.Forms.DataGridView dgvTurnoNoche;
    }
}
