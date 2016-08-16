namespace PlanillaAsistencia.Pantallas.EditorAsistencias
{
    partial class EditorAsistencias
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.grpBoxGeneral = new System.Windows.Forms.GroupBox();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.cmbEstadoAsistencia = new System.Windows.Forms.ComboBox();
            this.lblCantidadAlumnos = new System.Windows.Forms.Label();
            this.lblAsistencia = new System.Windows.Forms.Label();
            this.numUpDownAlumnos = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbAsignatura = new System.Windows.Forms.ComboBox();
            this.grpBoxDocente = new System.Windows.Forms.GroupBox();
            this.cmbDocente = new System.Windows.Forms.ComboBox();
            this.grpBoxHorarios = new System.Windows.Forms.GroupBox();
            this.grpBoxHoraReal = new System.Windows.Forms.GroupBox();
            this.mktxtHoraSalidaReal = new System.Windows.Forms.MaskedTextBox();
            this.mktxtHoraEntradaReal = new System.Windows.Forms.MaskedTextBox();
            this.lblHoraSalidaReal = new System.Windows.Forms.Label();
            this.lblHoraEntradaReal = new System.Windows.Forms.Label();
            this.grpBoxHoraEsperada = new System.Windows.Forms.GroupBox();
            this.mktxtHoraSalidaEsperada = new System.Windows.Forms.MaskedTextBox();
            this.mktxtHoraEntradaEsperada = new System.Windows.Forms.MaskedTextBox();
            this.lblHoraSalidaEsperada = new System.Windows.Forms.Label();
            this.lblHoraEntradaEsperada = new System.Windows.Forms.Label();
            this.datePickerCargaAsistencia = new System.Windows.Forms.DateTimePicker();
            this.lbleMensajes = new PlanillaAsistencia.ControlesPersonalizados.LabelConEfectos();
            this.tripleGrillaAsistencias = new PlanillaAsistencia.ControlesPersonalizados.TripleGrillaAsistencias();
            this.grpBoxGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownAlumnos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpBoxDocente.SuspendLayout();
            this.grpBoxHorarios.SuspendLayout();
            this.grpBoxHoraReal.SuspendLayout();
            this.grpBoxHoraEsperada.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackgroundImage = global::PlanillaAsistencia.Properties.Resources.save;
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Location = new System.Drawing.Point(684, 145);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(69, 59);
            this.btnGuardar.TabIndex = 27;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // grpBoxGeneral
            // 
            this.grpBoxGeneral.Controls.Add(this.txtObservaciones);
            this.grpBoxGeneral.Controls.Add(this.lblObservaciones);
            this.grpBoxGeneral.Controls.Add(this.cmbEstadoAsistencia);
            this.grpBoxGeneral.Controls.Add(this.lblCantidadAlumnos);
            this.grpBoxGeneral.Controls.Add(this.lblAsistencia);
            this.grpBoxGeneral.Controls.Add(this.numUpDownAlumnos);
            this.grpBoxGeneral.Location = new System.Drawing.Point(319, 104);
            this.grpBoxGeneral.Name = "grpBoxGeneral";
            this.grpBoxGeneral.Size = new System.Drawing.Size(334, 127);
            this.grpBoxGeneral.TabIndex = 26;
            this.grpBoxGeneral.TabStop = false;
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
            // numUpDownAlumnos
            // 
            this.numUpDownAlumnos.Location = new System.Drawing.Point(185, 20);
            this.numUpDownAlumnos.Name = "numUpDownAlumnos";
            this.numUpDownAlumnos.Size = new System.Drawing.Size(37, 20);
            this.numUpDownAlumnos.TabIndex = 13;
            this.numUpDownAlumnos.ValueChanged += new System.EventHandler(this.numUpDownAlumnos_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbAsignatura);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(405, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 55);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Asignatura";
            // 
            // cmbAsignatura
            // 
            this.cmbAsignatura.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAsignatura.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAsignatura.Enabled = false;
            this.cmbAsignatura.FormattingEnabled = true;
            this.cmbAsignatura.Location = new System.Drawing.Point(6, 22);
            this.cmbAsignatura.Name = "cmbAsignatura";
            this.cmbAsignatura.Size = new System.Drawing.Size(352, 24);
            this.cmbAsignatura.TabIndex = 2;
            this.cmbAsignatura.SelectionChangeCommitted += new System.EventHandler(this.cmbAsignatura_SelectionChangeCommitted);
            // 
            // grpBoxDocente
            // 
            this.grpBoxDocente.Controls.Add(this.cmbDocente);
            this.grpBoxDocente.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxDocente.Location = new System.Drawing.Point(3, 43);
            this.grpBoxDocente.Name = "grpBoxDocente";
            this.grpBoxDocente.Size = new System.Drawing.Size(364, 55);
            this.grpBoxDocente.TabIndex = 24;
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
            // grpBoxHorarios
            // 
            this.grpBoxHorarios.Controls.Add(this.grpBoxHoraReal);
            this.grpBoxHorarios.Controls.Add(this.grpBoxHoraEsperada);
            this.grpBoxHorarios.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxHorarios.Location = new System.Drawing.Point(3, 104);
            this.grpBoxHorarios.Name = "grpBoxHorarios";
            this.grpBoxHorarios.Size = new System.Drawing.Size(310, 127);
            this.grpBoxHorarios.TabIndex = 23;
            this.grpBoxHorarios.TabStop = false;
            this.grpBoxHorarios.Text = "Horarios";
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
            // datePickerCargaAsistencia
            // 
            this.datePickerCargaAsistencia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerCargaAsistencia.Location = new System.Drawing.Point(3, 3);
            this.datePickerCargaAsistencia.Name = "datePickerCargaAsistencia";
            this.datePickerCargaAsistencia.Size = new System.Drawing.Size(200, 20);
            this.datePickerCargaAsistencia.TabIndex = 22;
            this.datePickerCargaAsistencia.Value = new System.DateTime(2016, 3, 17, 0, 0, 0, 0);
            this.datePickerCargaAsistencia.CloseUp += new System.EventHandler(this.datePickerCargaAsistencia_CloseUp);
            // 
            // lbleMensajes
            // 
            this.lbleMensajes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbleMensajes.ColorPorDefecto = System.Drawing.Color.Black;
            this.lbleMensajes.DuracionMensajes = 3000;
            this.lbleMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbleMensajes.Location = new System.Drawing.Point(221, 3);
            this.lbleMensajes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbleMensajes.Name = "lbleMensajes";
            this.lbleMensajes.Size = new System.Drawing.Size(562, 34);
            this.lbleMensajes.TabIndex = 29;
            // 
            // tripleGrillaAsistencias
            // 
            this.tripleGrillaAsistencias.Location = new System.Drawing.Point(3, 237);
            this.tripleGrillaAsistencias.Name = "tripleGrillaAsistencias";
            this.tripleGrillaAsistencias.Size = new System.Drawing.Size(780, 247);
            this.tripleGrillaAsistencias.TabIndex = 28;
            // 
            // ModificacionAsistencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.lbleMensajes);
            this.Controls.Add(this.tripleGrillaAsistencias);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.grpBoxGeneral);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBoxDocente);
            this.Controls.Add(this.grpBoxHorarios);
            this.Controls.Add(this.datePickerCargaAsistencia);
            this.Name = "ModificacionAsistencias";
            this.Size = new System.Drawing.Size(788, 488);
            this.grpBoxGeneral.ResumeLayout(false);
            this.grpBoxGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownAlumnos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.grpBoxDocente.ResumeLayout(false);
            this.grpBoxHorarios.ResumeLayout(false);
            this.grpBoxHoraReal.ResumeLayout(false);
            this.grpBoxHoraReal.PerformLayout();
            this.grpBoxHoraEsperada.ResumeLayout(false);
            this.grpBoxHoraEsperada.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ControlesPersonalizados.TripleGrillaAsistencias tripleGrillaAsistencias;
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
        private ControlesPersonalizados.LabelConEfectos lbleMensajes;
    }
}
