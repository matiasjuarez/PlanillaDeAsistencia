namespace PlanillaAsistencia
{
    partial class planillaAsistencia
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabFiltroCursos = new System.Windows.Forms.TabPage();
            this.vistaGlobal1 = new PlanillaAsistencia.VistaGlobal();
            this.tabAsistencia = new System.Windows.Forms.TabPage();
            this.datePickerCargaAsistencia = new System.Windows.Forms.DateTimePicker();
            this.grpBoxHorarios = new System.Windows.Forms.GroupBox();
            this.grpBoxHoraEsperada = new System.Windows.Forms.GroupBox();
            this.lblHoraEntradaEsperada = new System.Windows.Forms.Label();
            this.lblHoraSalidaEsperada = new System.Windows.Forms.Label();
            this.mktxtHoraEntradaEsperada = new System.Windows.Forms.MaskedTextBox();
            this.mktxtHoraSalidaEsperada = new System.Windows.Forms.MaskedTextBox();
            this.grpBoxHoraReal = new System.Windows.Forms.GroupBox();
            this.lblHoraEntradaReal = new System.Windows.Forms.Label();
            this.lblHoraSalidaReal = new System.Windows.Forms.Label();
            this.mktxtHoraEntradaReal = new System.Windows.Forms.MaskedTextBox();
            this.mktxtHoraSalidaReal = new System.Windows.Forms.MaskedTextBox();
            this.grpBoxDocente = new System.Windows.Forms.GroupBox();
            this.cmbDocente = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbAsignatura = new System.Windows.Forms.ComboBox();
            this.grpBoxGeneral = new System.Windows.Forms.GroupBox();
            this.numUpDownAlumnos = new System.Windows.Forms.NumericUpDown();
            this.lblAsistencia = new System.Windows.Forms.Label();
            this.lblCantidadAlumnos = new System.Windows.Forms.Label();
            this.cmbEstadoAsistencia = new System.Windows.Forms.ComboBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lbleMensajes = new PlanillaAsistencia.ControlesPersonalizados.LabelConEfectos();
            this.tripleGrillaAsistencias = new PlanillaAsistencia.ControlesPersonalizados.TripleGrillaAsistencias();
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();
            this.tabFiltroCursos.SuspendLayout();
            this.tabAsistencia.SuspendLayout();
            this.grpBoxHorarios.SuspendLayout();
            this.grpBoxHoraEsperada.SuspendLayout();
            this.grpBoxHoraReal.SuspendLayout();
            this.grpBoxDocente.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpBoxGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownAlumnos)).BeginInit();
            this.tabControlPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFiltroCursos
            // 
            this.tabFiltroCursos.Controls.Add(this.vistaGlobal1);
            this.tabFiltroCursos.Location = new System.Drawing.Point(4, 22);
            this.tabFiltroCursos.Name = "tabFiltroCursos";
            this.tabFiltroCursos.Size = new System.Drawing.Size(787, 487);
            this.tabFiltroCursos.TabIndex = 2;
            this.tabFiltroCursos.Text = "Vista global de cursos";
            this.tabFiltroCursos.UseVisualStyleBackColor = true;
            // 
            // vistaGlobal1
            // 
            this.vistaGlobal1.ControladorVistaGlobal = null;
            this.vistaGlobal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vistaGlobal1.Location = new System.Drawing.Point(0, 0);
            this.vistaGlobal1.Name = "vistaGlobal1";
            this.vistaGlobal1.Size = new System.Drawing.Size(787, 487);
            this.vistaGlobal1.TabIndex = 0;
            // 
            // tabAsistencia
            // 
            this.tabAsistencia.Controls.Add(this.tripleGrillaAsistencias);
            this.tabAsistencia.Controls.Add(this.lbleMensajes);
            this.tabAsistencia.Controls.Add(this.btnGuardar);
            this.tabAsistencia.Controls.Add(this.grpBoxGeneral);
            this.tabAsistencia.Controls.Add(this.groupBox1);
            this.tabAsistencia.Controls.Add(this.grpBoxDocente);
            this.tabAsistencia.Controls.Add(this.grpBoxHorarios);
            this.tabAsistencia.Controls.Add(this.datePickerCargaAsistencia);
            this.tabAsistencia.Location = new System.Drawing.Point(4, 22);
            this.tabAsistencia.Name = "tabAsistencia";
            this.tabAsistencia.Padding = new System.Windows.Forms.Padding(3);
            this.tabAsistencia.Size = new System.Drawing.Size(787, 487);
            this.tabAsistencia.TabIndex = 0;
            this.tabAsistencia.Text = "Asistencia";
            this.tabAsistencia.UseVisualStyleBackColor = true;
            // 
            // datePickerCargaAsistencia
            // 
            this.datePickerCargaAsistencia.Location = new System.Drawing.Point(15, 6);
            this.datePickerCargaAsistencia.Name = "datePickerCargaAsistencia";
            this.datePickerCargaAsistencia.Size = new System.Drawing.Size(200, 20);
            this.datePickerCargaAsistencia.TabIndex = 8;
            this.datePickerCargaAsistencia.Value = new System.DateTime(2016, 3, 17, 0, 0, 0, 0);
            this.datePickerCargaAsistencia.CloseUp += new System.EventHandler(this.datePickerCargaAsistencia_CloseUp);
            // 
            // grpBoxHorarios
            // 
            this.grpBoxHorarios.Controls.Add(this.grpBoxHoraReal);
            this.grpBoxHorarios.Controls.Add(this.grpBoxHoraEsperada);
            this.grpBoxHorarios.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxHorarios.Location = new System.Drawing.Point(15, 108);
            this.grpBoxHorarios.Name = "grpBoxHorarios";
            this.grpBoxHorarios.Size = new System.Drawing.Size(310, 127);
            this.grpBoxHorarios.TabIndex = 10;
            this.grpBoxHorarios.TabStop = false;
            this.grpBoxHorarios.Text = "Horarios";
            // 
            // grpBoxHoraEsperada
            // 
            this.grpBoxHoraEsperada.Controls.Add(this.mktxtHoraSalidaEsperada);
            this.grpBoxHoraEsperada.Controls.Add(this.mktxtHoraEntradaEsperada);
            this.grpBoxHoraEsperada.Controls.Add(this.lblHoraSalidaEsperada);
            this.grpBoxHoraEsperada.Controls.Add(this.lblHoraEntradaEsperada);
            this.grpBoxHoraEsperada.Location = new System.Drawing.Point(7, 23);
            this.grpBoxHoraEsperada.Name = "grpBoxHoraEsperada";
            this.grpBoxHoraEsperada.Size = new System.Drawing.Size(145, 100);
            this.grpBoxHoraEsperada.TabIndex = 0;
            this.grpBoxHoraEsperada.TabStop = false;
            this.grpBoxHoraEsperada.Text = "Esperado";
            // 
            // lblHoraEntradaEsperada
            // 
            this.lblHoraEntradaEsperada.AutoSize = true;
            this.lblHoraEntradaEsperada.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraEntradaEsperada.Location = new System.Drawing.Point(6, 30);
            this.lblHoraEntradaEsperada.Name = "lblHoraEntradaEsperada";
            this.lblHoraEntradaEsperada.Size = new System.Drawing.Size(65, 16);
            this.lblHoraEntradaEsperada.TabIndex = 0;
            this.lblHoraEntradaEsperada.Text = "Entrada";
            // 
            // lblHoraSalidaEsperada
            // 
            this.lblHoraSalidaEsperada.AutoSize = true;
            this.lblHoraSalidaEsperada.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraSalidaEsperada.Location = new System.Drawing.Point(6, 67);
            this.lblHoraSalidaEsperada.Name = "lblHoraSalidaEsperada";
            this.lblHoraSalidaEsperada.Size = new System.Drawing.Size(52, 16);
            this.lblHoraSalidaEsperada.TabIndex = 3;
            this.lblHoraSalidaEsperada.Text = "Salida";
            // 
            // mktxtHoraEntradaEsperada
            // 
            this.mktxtHoraEntradaEsperada.Location = new System.Drawing.Point(87, 25);
            this.mktxtHoraEntradaEsperada.Mask = "00:00";
            this.mktxtHoraEntradaEsperada.Name = "mktxtHoraEntradaEsperada";
            this.mktxtHoraEntradaEsperada.ReadOnly = true;
            this.mktxtHoraEntradaEsperada.Size = new System.Drawing.Size(50, 23);
            this.mktxtHoraEntradaEsperada.TabIndex = 4;
            this.mktxtHoraEntradaEsperada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mktxtHoraEntradaEsperada.ValidatingType = typeof(System.DateTime);
            // 
            // mktxtHoraSalidaEsperada
            // 
            this.mktxtHoraSalidaEsperada.Location = new System.Drawing.Point(86, 60);
            this.mktxtHoraSalidaEsperada.Mask = "00:00";
            this.mktxtHoraSalidaEsperada.Name = "mktxtHoraSalidaEsperada";
            this.mktxtHoraSalidaEsperada.ReadOnly = true;
            this.mktxtHoraSalidaEsperada.Size = new System.Drawing.Size(50, 23);
            this.mktxtHoraSalidaEsperada.TabIndex = 5;
            this.mktxtHoraSalidaEsperada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mktxtHoraSalidaEsperada.ValidatingType = typeof(System.DateTime);
            // 
            // grpBoxHoraReal
            // 
            this.grpBoxHoraReal.Controls.Add(this.mktxtHoraSalidaReal);
            this.grpBoxHoraReal.Controls.Add(this.mktxtHoraEntradaReal);
            this.grpBoxHoraReal.Controls.Add(this.lblHoraSalidaReal);
            this.grpBoxHoraReal.Controls.Add(this.lblHoraEntradaReal);
            this.grpBoxHoraReal.Location = new System.Drawing.Point(158, 23);
            this.grpBoxHoraReal.Name = "grpBoxHoraReal";
            this.grpBoxHoraReal.Size = new System.Drawing.Size(145, 100);
            this.grpBoxHoraReal.TabIndex = 1;
            this.grpBoxHoraReal.TabStop = false;
            this.grpBoxHoraReal.Text = "Real";
            // 
            // lblHoraEntradaReal
            // 
            this.lblHoraEntradaReal.AutoSize = true;
            this.lblHoraEntradaReal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraEntradaReal.Location = new System.Drawing.Point(6, 30);
            this.lblHoraEntradaReal.Name = "lblHoraEntradaReal";
            this.lblHoraEntradaReal.Size = new System.Drawing.Size(65, 16);
            this.lblHoraEntradaReal.TabIndex = 5;
            this.lblHoraEntradaReal.Text = "Entrada";
            // 
            // lblHoraSalidaReal
            // 
            this.lblHoraSalidaReal.AutoSize = true;
            this.lblHoraSalidaReal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraSalidaReal.Location = new System.Drawing.Point(6, 67);
            this.lblHoraSalidaReal.Name = "lblHoraSalidaReal";
            this.lblHoraSalidaReal.Size = new System.Drawing.Size(52, 16);
            this.lblHoraSalidaReal.TabIndex = 7;
            this.lblHoraSalidaReal.Text = "Salida";
            // 
            // mktxtHoraEntradaReal
            // 
            this.mktxtHoraEntradaReal.Location = new System.Drawing.Point(87, 25);
            this.mktxtHoraEntradaReal.Mask = "00:00";
            this.mktxtHoraEntradaReal.Name = "mktxtHoraEntradaReal";
            this.mktxtHoraEntradaReal.Size = new System.Drawing.Size(50, 23);
            this.mktxtHoraEntradaReal.TabIndex = 8;
            this.mktxtHoraEntradaReal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mktxtHoraEntradaReal.ValidatingType = typeof(System.DateTime);
            this.mktxtHoraEntradaReal.TextChanged += new System.EventHandler(this.mktxtHoraEntradaReal_TextChanged);
            // 
            // mktxtHoraSalidaReal
            // 
            this.mktxtHoraSalidaReal.Location = new System.Drawing.Point(86, 60);
            this.mktxtHoraSalidaReal.Mask = "00:00";
            this.mktxtHoraSalidaReal.Name = "mktxtHoraSalidaReal";
            this.mktxtHoraSalidaReal.Size = new System.Drawing.Size(50, 23);
            this.mktxtHoraSalidaReal.TabIndex = 9;
            this.mktxtHoraSalidaReal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mktxtHoraSalidaReal.ValidatingType = typeof(System.DateTime);
            this.mktxtHoraSalidaReal.TextChanged += new System.EventHandler(this.mktxtHoraSalidaReal_TextChanged);
            // 
            // grpBoxDocente
            // 
            this.grpBoxDocente.Controls.Add(this.cmbDocente);
            this.grpBoxDocente.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxDocente.Location = new System.Drawing.Point(15, 47);
            this.grpBoxDocente.Name = "grpBoxDocente";
            this.grpBoxDocente.Size = new System.Drawing.Size(364, 55);
            this.grpBoxDocente.TabIndex = 11;
            this.grpBoxDocente.TabStop = false;
            this.grpBoxDocente.Text = "Docente";
            // 
            // cmbDocente
            // 
            this.cmbDocente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDocente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDocente.FormattingEnabled = true;
            this.cmbDocente.Location = new System.Drawing.Point(6, 22);
            this.cmbDocente.Name = "cmbDocente";
            this.cmbDocente.Size = new System.Drawing.Size(352, 24);
            this.cmbDocente.TabIndex = 0;
            this.cmbDocente.SelectionChangeCommitted += new System.EventHandler(this.cmbDocente_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbAsignatura);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(417, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 55);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Asignatura";
            // 
            // cmbAsignatura
            // 
            this.cmbAsignatura.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAsignatura.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAsignatura.FormattingEnabled = true;
            this.cmbAsignatura.Location = new System.Drawing.Point(6, 22);
            this.cmbAsignatura.Name = "cmbAsignatura";
            this.cmbAsignatura.Size = new System.Drawing.Size(352, 24);
            this.cmbAsignatura.TabIndex = 2;
            this.cmbAsignatura.SelectionChangeCommitted += new System.EventHandler(this.cmbAsignatura_SelectionChangeCommitted);
            this.cmbAsignatura.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmb_MouseClick);
            // 
            // grpBoxGeneral
            // 
            this.grpBoxGeneral.Controls.Add(this.txtObservaciones);
            this.grpBoxGeneral.Controls.Add(this.lblObservaciones);
            this.grpBoxGeneral.Controls.Add(this.cmbEstadoAsistencia);
            this.grpBoxGeneral.Controls.Add(this.lblCantidadAlumnos);
            this.grpBoxGeneral.Controls.Add(this.lblAsistencia);
            this.grpBoxGeneral.Controls.Add(this.numUpDownAlumnos);
            this.grpBoxGeneral.Location = new System.Drawing.Point(331, 108);
            this.grpBoxGeneral.Name = "grpBoxGeneral";
            this.grpBoxGeneral.Size = new System.Drawing.Size(334, 127);
            this.grpBoxGeneral.TabIndex = 16;
            this.grpBoxGeneral.TabStop = false;
            // 
            // numUpDownAlumnos
            // 
            this.numUpDownAlumnos.Location = new System.Drawing.Point(185, 20);
            this.numUpDownAlumnos.Name = "numUpDownAlumnos";
            this.numUpDownAlumnos.Size = new System.Drawing.Size(37, 20);
            this.numUpDownAlumnos.TabIndex = 13;
            this.numUpDownAlumnos.ValueChanged += new System.EventHandler(this.numUpDownAlumnos_ValueChanged);
            // 
            // lblAsistencia
            // 
            this.lblAsistencia.AutoSize = true;
            this.lblAsistencia.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsistencia.Location = new System.Drawing.Point(6, 54);
            this.lblAsistencia.Name = "lblAsistencia";
            this.lblAsistencia.Size = new System.Drawing.Size(58, 16);
            this.lblAsistencia.TabIndex = 15;
            this.lblAsistencia.Text = "Estado";
            // 
            // lblCantidadAlumnos
            // 
            this.lblCantidadAlumnos.AutoSize = true;
            this.lblCantidadAlumnos.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadAlumnos.Location = new System.Drawing.Point(6, 24);
            this.lblCantidadAlumnos.Name = "lblCantidadAlumnos";
            this.lblCantidadAlumnos.Size = new System.Drawing.Size(161, 16);
            this.lblCantidadAlumnos.TabIndex = 14;
            this.lblCantidadAlumnos.Text = "Cantidad de alumnos";
            // 
            // cmbEstadoAsistencia
            // 
            this.cmbEstadoAsistencia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbEstadoAsistencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoAsistencia.FormattingEnabled = true;
            this.cmbEstadoAsistencia.Location = new System.Drawing.Point(185, 50);
            this.cmbEstadoAsistencia.Name = "cmbEstadoAsistencia";
            this.cmbEstadoAsistencia.Size = new System.Drawing.Size(143, 21);
            this.cmbEstadoAsistencia.TabIndex = 16;
            this.cmbEstadoAsistencia.SelectionChangeCommitted += new System.EventHandler(this.cmbEstadoAsistencia_SelectionChangeCommitted);
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObservaciones.Location = new System.Drawing.Point(6, 84);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(116, 16);
            this.lblObservaciones.TabIndex = 17;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(185, 80);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(143, 41);
            this.txtObservaciones.TabIndex = 18;
            this.txtObservaciones.TextChanged += new System.EventHandler(this.txtObservaciones_TextChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Location = new System.Drawing.Point(671, 212);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(108, 23);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lbleMensajes
            // 
            this.lbleMensajes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbleMensajes.ColorPorDefecto = System.Drawing.Color.Black;
            this.lbleMensajes.DuracionMensajes = 3000;
            this.lbleMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbleMensajes.Location = new System.Drawing.Point(235, 6);
            this.lbleMensajes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbleMensajes.Name = "lbleMensajes";
            this.lbleMensajes.Size = new System.Drawing.Size(548, 34);
            this.lbleMensajes.TabIndex = 20;
            // 
            // tripleGrillaAsistencias
            // 
            this.tripleGrillaAsistencias.Location = new System.Drawing.Point(1, 241);
            this.tripleGrillaAsistencias.Name = "tripleGrillaAsistencias";
            this.tripleGrillaAsistencias.Size = new System.Drawing.Size(780, 247);
            this.tripleGrillaAsistencias.TabIndex = 21;
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Controls.Add(this.tabAsistencia);
            this.tabControlPrincipal.Controls.Add(this.tabFiltroCursos);
            this.tabControlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tabControlPrincipal.Name = "tabControlPrincipal";
            this.tabControlPrincipal.SelectedIndex = 0;
            this.tabControlPrincipal.Size = new System.Drawing.Size(795, 513);
            this.tabControlPrincipal.TabIndex = 8;
            // 
            // planillaAsistencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(795, 513);
            this.Controls.Add(this.tabControlPrincipal);
            this.Name = "planillaAsistencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Planilla de asistencia";
            this.Resize += new System.EventHandler(this.planillaAsistencia_Resize);
            this.tabFiltroCursos.ResumeLayout(false);
            this.tabAsistencia.ResumeLayout(false);
            this.grpBoxHorarios.ResumeLayout(false);
            this.grpBoxHoraEsperada.ResumeLayout(false);
            this.grpBoxHoraEsperada.PerformLayout();
            this.grpBoxHoraReal.ResumeLayout(false);
            this.grpBoxHoraReal.PerformLayout();
            this.grpBoxDocente.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grpBoxGeneral.ResumeLayout(false);
            this.grpBoxGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownAlumnos)).EndInit();
            this.tabControlPrincipal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabFiltroCursos;
        private VistaGlobal vistaGlobal1;
        private System.Windows.Forms.TabPage tabAsistencia;
        private ControlesPersonalizados.TripleGrillaAsistencias tripleGrillaAsistencias;
        private ControlesPersonalizados.LabelConEfectos lbleMensajes;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox grpBoxGeneral;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.ComboBox cmbEstadoAsistencia;
        private System.Windows.Forms.Label lblCantidadAlumnos;
        private System.Windows.Forms.Label lblAsistencia;
        private System.Windows.Forms.NumericUpDown numUpDownAlumnos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAsignatura;
        private System.Windows.Forms.GroupBox grpBoxDocente;
        private System.Windows.Forms.ComboBox cmbDocente;
        private System.Windows.Forms.GroupBox grpBoxHorarios;
        private System.Windows.Forms.GroupBox grpBoxHoraReal;
        private System.Windows.Forms.MaskedTextBox mktxtHoraSalidaReal;
        private System.Windows.Forms.MaskedTextBox mktxtHoraEntradaReal;
        private System.Windows.Forms.Label lblHoraSalidaReal;
        private System.Windows.Forms.Label lblHoraEntradaReal;
        private System.Windows.Forms.GroupBox grpBoxHoraEsperada;
        private System.Windows.Forms.MaskedTextBox mktxtHoraSalidaEsperada;
        private System.Windows.Forms.MaskedTextBox mktxtHoraEntradaEsperada;
        private System.Windows.Forms.Label lblHoraSalidaEsperada;
        private System.Windows.Forms.Label lblHoraEntradaEsperada;
        private System.Windows.Forms.DateTimePicker datePickerCargaAsistencia;
        private System.Windows.Forms.TabControl tabControlPrincipal;

    }
}

