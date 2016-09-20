namespace PlanillaAsistencia.Pantallas.VistaGlobalAsistencias
{
    partial class ConsultaAsistencias
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
            this.grpBoxFiltrosCursos = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkUsarDocente = new System.Windows.Forms.CheckBox();
            this.chkUsarAsignatura = new System.Windows.Forms.CheckBox();
            this.cmbFiltroDocente = new System.Windows.Forms.ComboBox();
            this.lblFiltroTurno = new System.Windows.Forms.Label();
            this.cmbFiltroAsignatura = new System.Windows.Forms.ComboBox();
            this.lblFiltroAsignatura = new System.Windows.Forms.Label();
            this.grpBoxFechasFiltroCursos = new System.Windows.Forms.GroupBox();
            this.chkUsarFechaHasta = new System.Windows.Forms.CheckBox();
            this.chkUsarFechaDesde = new System.Windows.Forms.CheckBox();
            this.dtpFiltroFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFiltroFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.lblFiltroFechaHasta = new System.Windows.Forms.Label();
            this.lblFiltroFechaDesde = new System.Windows.Forms.Label();
            this.tripleGrillaVistaGlobal = new PlanillaAsistencia.ControlesPersonalizados.TripleGrillaAsistencias();
            this.grpBoxFiltrosCursos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpBoxFechasFiltroCursos.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxFiltrosCursos
            // 
            this.grpBoxFiltrosCursos.Controls.Add(this.btnBuscar);
            this.grpBoxFiltrosCursos.Controls.Add(this.groupBox2);
            this.grpBoxFiltrosCursos.Controls.Add(this.grpBoxFechasFiltroCursos);
            this.grpBoxFiltrosCursos.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxFiltrosCursos.Location = new System.Drawing.Point(5, 2);
            this.grpBoxFiltrosCursos.Name = "grpBoxFiltrosCursos";
            this.grpBoxFiltrosCursos.Size = new System.Drawing.Size(788, 154);
            this.grpBoxFiltrosCursos.TabIndex = 3;
            this.grpBoxFiltrosCursos.TabStop = false;
            this.grpBoxFiltrosCursos.Text = "FILTROS";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = global::PlanillaAsistencia.Properties.Resources.search;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.Location = new System.Drawing.Point(705, 60);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(77, 73);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkUsarDocente);
            this.groupBox2.Controls.Add(this.chkUsarAsignatura);
            this.groupBox2.Controls.Add(this.cmbFiltroDocente);
            this.groupBox2.Controls.Add(this.lblFiltroTurno);
            this.groupBox2.Controls.Add(this.cmbFiltroAsignatura);
            this.groupBox2.Controls.Add(this.lblFiltroAsignatura);
            this.groupBox2.Location = new System.Drawing.Point(7, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(692, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // chkUsarDocente
            // 
            this.chkUsarDocente.AutoSize = true;
            this.chkUsarDocente.Location = new System.Drawing.Point(647, 25);
            this.chkUsarDocente.Name = "chkUsarDocente";
            this.chkUsarDocente.Size = new System.Drawing.Size(15, 14);
            this.chkUsarDocente.TabIndex = 6;
            this.chkUsarDocente.UseVisualStyleBackColor = true;
            this.chkUsarDocente.CheckedChanged += new System.EventHandler(this.chkUsarDocente_CheckedChanged);
            // 
            // chkUsarAsignatura
            // 
            this.chkUsarAsignatura.AutoSize = true;
            this.chkUsarAsignatura.Location = new System.Drawing.Point(317, 25);
            this.chkUsarAsignatura.Name = "chkUsarAsignatura";
            this.chkUsarAsignatura.Size = new System.Drawing.Size(15, 14);
            this.chkUsarAsignatura.TabIndex = 5;
            this.chkUsarAsignatura.UseVisualStyleBackColor = true;
            this.chkUsarAsignatura.CheckedChanged += new System.EventHandler(this.chkUsarAsignatura_CheckedChanged);
            // 
            // cmbFiltroDocente
            // 
            this.cmbFiltroDocente.FormattingEnabled = true;
            this.cmbFiltroDocente.Location = new System.Drawing.Point(441, 15);
            this.cmbFiltroDocente.Name = "cmbFiltroDocente";
            this.cmbFiltroDocente.Size = new System.Drawing.Size(200, 24);
            this.cmbFiltroDocente.TabIndex = 3;
            this.cmbFiltroDocente.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroDocente_SelectedIndexChanged);
            // 
            // lblFiltroTurno
            // 
            this.lblFiltroTurno.AutoSize = true;
            this.lblFiltroTurno.Location = new System.Drawing.Point(366, 23);
            this.lblFiltroTurno.Name = "lblFiltroTurno";
            this.lblFiltroTurno.Size = new System.Drawing.Size(68, 16);
            this.lblFiltroTurno.TabIndex = 2;
            this.lblFiltroTurno.Text = "Docente";
            // 
            // cmbFiltroAsignatura
            // 
            this.cmbFiltroAsignatura.FormattingEnabled = true;
            this.cmbFiltroAsignatura.Location = new System.Drawing.Point(100, 17);
            this.cmbFiltroAsignatura.Name = "cmbFiltroAsignatura";
            this.cmbFiltroAsignatura.Size = new System.Drawing.Size(208, 24);
            this.cmbFiltroAsignatura.TabIndex = 1;
            this.cmbFiltroAsignatura.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroAsignatura_SelectedIndexChanged);
            // 
            // lblFiltroAsignatura
            // 
            this.lblFiltroAsignatura.AutoSize = true;
            this.lblFiltroAsignatura.Location = new System.Drawing.Point(6, 23);
            this.lblFiltroAsignatura.Name = "lblFiltroAsignatura";
            this.lblFiltroAsignatura.Size = new System.Drawing.Size(87, 16);
            this.lblFiltroAsignatura.TabIndex = 0;
            this.lblFiltroAsignatura.Text = "Asignatura";
            // 
            // grpBoxFechasFiltroCursos
            // 
            this.grpBoxFechasFiltroCursos.Controls.Add(this.chkUsarFechaHasta);
            this.grpBoxFechasFiltroCursos.Controls.Add(this.chkUsarFechaDesde);
            this.grpBoxFechasFiltroCursos.Controls.Add(this.dtpFiltroFechaHasta);
            this.grpBoxFechasFiltroCursos.Controls.Add(this.dtpFiltroFechaDesde);
            this.grpBoxFechasFiltroCursos.Controls.Add(this.lblFiltroFechaHasta);
            this.grpBoxFechasFiltroCursos.Controls.Add(this.lblFiltroFechaDesde);
            this.grpBoxFechasFiltroCursos.Location = new System.Drawing.Point(7, 23);
            this.grpBoxFechasFiltroCursos.Name = "grpBoxFechasFiltroCursos";
            this.grpBoxFechasFiltroCursos.Size = new System.Drawing.Size(692, 49);
            this.grpBoxFechasFiltroCursos.TabIndex = 2;
            this.grpBoxFechasFiltroCursos.TabStop = false;
            this.grpBoxFechasFiltroCursos.Text = "Fechas";
            // 
            // chkUsarFechaHasta
            // 
            this.chkUsarFechaHasta.AutoSize = true;
            this.chkUsarFechaHasta.Location = new System.Drawing.Point(647, 25);
            this.chkUsarFechaHasta.Name = "chkUsarFechaHasta";
            this.chkUsarFechaHasta.Size = new System.Drawing.Size(15, 14);
            this.chkUsarFechaHasta.TabIndex = 5;
            this.chkUsarFechaHasta.UseVisualStyleBackColor = true;
            this.chkUsarFechaHasta.CheckedChanged += new System.EventHandler(this.chkUsarFechaHasta_CheckedChanged);
            // 
            // chkUsarFechaDesde
            // 
            this.chkUsarFechaDesde.AutoSize = true;
            this.chkUsarFechaDesde.Location = new System.Drawing.Point(317, 25);
            this.chkUsarFechaDesde.Name = "chkUsarFechaDesde";
            this.chkUsarFechaDesde.Size = new System.Drawing.Size(15, 14);
            this.chkUsarFechaDesde.TabIndex = 4;
            this.chkUsarFechaDesde.UseVisualStyleBackColor = true;
            this.chkUsarFechaDesde.CheckedChanged += new System.EventHandler(this.chkUsarFechaDesde_CheckedChanged);
            // 
            // dtpFiltroFechaHasta
            // 
            this.dtpFiltroFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroFechaHasta.Location = new System.Drawing.Point(441, 16);
            this.dtpFiltroFechaHasta.Name = "dtpFiltroFechaHasta";
            this.dtpFiltroFechaHasta.Size = new System.Drawing.Size(200, 23);
            this.dtpFiltroFechaHasta.TabIndex = 3;
            this.dtpFiltroFechaHasta.CloseUp += new System.EventHandler(this.dtpFiltroFechaHasta_CloseUp);
            // 
            // dtpFiltroFechaDesde
            // 
            this.dtpFiltroFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroFechaDesde.Location = new System.Drawing.Point(100, 18);
            this.dtpFiltroFechaDesde.Name = "dtpFiltroFechaDesde";
            this.dtpFiltroFechaDesde.Size = new System.Drawing.Size(208, 23);
            this.dtpFiltroFechaDesde.TabIndex = 2;
            this.dtpFiltroFechaDesde.CloseUp += new System.EventHandler(this.dtpFiltroFechaDesde_CloseUp);
            // 
            // lblFiltroFechaHasta
            // 
            this.lblFiltroFechaHasta.AutoSize = true;
            this.lblFiltroFechaHasta.Location = new System.Drawing.Point(366, 23);
            this.lblFiltroFechaHasta.Name = "lblFiltroFechaHasta";
            this.lblFiltroFechaHasta.Size = new System.Drawing.Size(51, 16);
            this.lblFiltroFechaHasta.TabIndex = 1;
            this.lblFiltroFechaHasta.Text = "Hasta";
            // 
            // lblFiltroFechaDesde
            // 
            this.lblFiltroFechaDesde.AutoSize = true;
            this.lblFiltroFechaDesde.Location = new System.Drawing.Point(7, 23);
            this.lblFiltroFechaDesde.Name = "lblFiltroFechaDesde";
            this.lblFiltroFechaDesde.Size = new System.Drawing.Size(53, 16);
            this.lblFiltroFechaDesde.TabIndex = 0;
            this.lblFiltroFechaDesde.Text = "Desde";
            // 
            // tripleGrillaVistaGlobal
            // 
            this.tripleGrillaVistaGlobal.Location = new System.Drawing.Point(0, 162);
            this.tripleGrillaVistaGlobal.ModoPresentacion = 1;
            this.tripleGrillaVistaGlobal.Name = "tripleGrillaVistaGlobal";
            this.tripleGrillaVistaGlobal.Size = new System.Drawing.Size(797, 326);
            this.tripleGrillaVistaGlobal.TabIndex = 4;
            // 
            // ConsultaAsistencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.tripleGrillaVistaGlobal);
            this.Controls.Add(this.grpBoxFiltrosCursos);
            this.Name = "ConsultaAsistencias";
            this.Size = new System.Drawing.Size(797, 488);
            this.grpBoxFiltrosCursos.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpBoxFechasFiltroCursos.ResumeLayout(false);
            this.grpBoxFechasFiltroCursos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ControlesPersonalizados.TripleGrillaAsistencias tripleGrillaVistaGlobal;
        private System.Windows.Forms.GroupBox grpBoxFiltrosCursos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbFiltroDocente;
        private System.Windows.Forms.Label lblFiltroTurno;
        private System.Windows.Forms.ComboBox cmbFiltroAsignatura;
        private System.Windows.Forms.Label lblFiltroAsignatura;
        private System.Windows.Forms.GroupBox grpBoxFechasFiltroCursos;
        private System.Windows.Forms.DateTimePicker dtpFiltroFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroFechaDesde;
        private System.Windows.Forms.Label lblFiltroFechaHasta;
        private System.Windows.Forms.Label lblFiltroFechaDesde;
        private System.Windows.Forms.CheckBox chkUsarFechaHasta;
        private System.Windows.Forms.CheckBox chkUsarFechaDesde;
        private System.Windows.Forms.CheckBox chkUsarDocente;
        private System.Windows.Forms.CheckBox chkUsarAsignatura;
    }
}
