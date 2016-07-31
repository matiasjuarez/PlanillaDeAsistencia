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
            this.tabCargaAsistencia = new System.Windows.Forms.TabControl();
            this.tabAsistencia = new System.Windows.Forms.TabPage();
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
            this.tabFiltroCursos = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpBoxFiltrosCursos = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbFiltroTurno = new System.Windows.Forms.ComboBox();
            this.lblFiltroTurno = new System.Windows.Forms.Label();
            this.cmbFiltroAsignatura = new System.Windows.Forms.ComboBox();
            this.lblFiltroAsignatura = new System.Windows.Forms.Label();
            this.grpBoxFechasFiltroCursos = new System.Windows.Forms.GroupBox();
            this.lblFiltroFechaHasta = new System.Windows.Forms.Label();
            this.mktxtFechaHastaFiltro = new System.Windows.Forms.MaskedTextBox();
            this.lblFiltroFechaDesde = new System.Windows.Forms.Label();
            this.mktxtFechaDesdeFiltro = new System.Windows.Forms.MaskedTextBox();
            this.dgvListaCursos = new System.Windows.Forms.DataGridView();
            this.tabABMEncargados = new System.Windows.Forms.TabPage();
            this.grpBoxAbmDocentes = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabABMDocente = new System.Windows.Forms.TabPage();
            this.tabVistaGeneralDocentes = new System.Windows.Forms.TabPage();
            this.lbleMensajes = new PlanillaAsistencia.ControlesPersonalizados.LabelConEfectos();
            this.tripleGrillaAsistencias = new PlanillaAsistencia.ControlesPersonalizados.TripleGrillaAsistencias();
            this.tabCargaAsistencia.SuspendLayout();
            this.tabAsistencia.SuspendLayout();
            this.grpBoxGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownAlumnos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpBoxDocente.SuspendLayout();
            this.grpBoxHorarios.SuspendLayout();
            this.grpBoxHoraReal.SuspendLayout();
            this.grpBoxHoraEsperada.SuspendLayout();
            this.tabFiltroCursos.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpBoxFiltrosCursos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpBoxFechasFiltroCursos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCursos)).BeginInit();
            this.tabABMEncargados.SuspendLayout();
            this.grpBoxAbmDocentes.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCargaAsistencia
            // 
            this.tabCargaAsistencia.Controls.Add(this.tabAsistencia);
            this.tabCargaAsistencia.Controls.Add(this.tabFiltroCursos);
            this.tabCargaAsistencia.Controls.Add(this.tabABMEncargados);
            this.tabCargaAsistencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCargaAsistencia.Location = new System.Drawing.Point(0, 0);
            this.tabCargaAsistencia.Name = "tabCargaAsistencia";
            this.tabCargaAsistencia.SelectedIndex = 0;
            this.tabCargaAsistencia.Size = new System.Drawing.Size(795, 513);
            this.tabCargaAsistencia.TabIndex = 8;
            // 
            // tabAsistencia
            // 
            this.tabAsistencia.Controls.Add(this.lbleMensajes);
            this.tabAsistencia.Controls.Add(this.tripleGrillaAsistencias);
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
            this.cmbAsignatura.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbAsignatura_MouseClick);
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
            this.datePickerCargaAsistencia.Location = new System.Drawing.Point(15, 6);
            this.datePickerCargaAsistencia.Name = "datePickerCargaAsistencia";
            this.datePickerCargaAsistencia.Size = new System.Drawing.Size(200, 20);
            this.datePickerCargaAsistencia.TabIndex = 8;
            this.datePickerCargaAsistencia.Value = new System.DateTime(2016, 3, 17, 0, 0, 0, 0);
            this.datePickerCargaAsistencia.CloseUp += new System.EventHandler(this.datePickerCargaAsistencia_CloseUp);
            // 
            // tabFiltroCursos
            // 
            this.tabFiltroCursos.Controls.Add(this.panel1);
            this.tabFiltroCursos.Location = new System.Drawing.Point(4, 22);
            this.tabFiltroCursos.Name = "tabFiltroCursos";
            this.tabFiltroCursos.Size = new System.Drawing.Size(787, 487);
            this.tabFiltroCursos.TabIndex = 2;
            this.tabFiltroCursos.Text = "Vista global de cursos";
            this.tabFiltroCursos.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpBoxFiltrosCursos);
            this.panel1.Controls.Add(this.dgvListaCursos);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 454);
            this.panel1.TabIndex = 0;
            // 
            // grpBoxFiltrosCursos
            // 
            this.grpBoxFiltrosCursos.Controls.Add(this.groupBox2);
            this.grpBoxFiltrosCursos.Controls.Add(this.grpBoxFechasFiltroCursos);
            this.grpBoxFiltrosCursos.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxFiltrosCursos.Location = new System.Drawing.Point(5, 4);
            this.grpBoxFiltrosCursos.Name = "grpBoxFiltrosCursos";
            this.grpBoxFiltrosCursos.Size = new System.Drawing.Size(767, 139);
            this.grpBoxFiltrosCursos.TabIndex = 1;
            this.grpBoxFiltrosCursos.TabStop = false;
            this.grpBoxFiltrosCursos.Text = "FILTROS";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbFiltroTurno);
            this.groupBox2.Controls.Add(this.lblFiltroTurno);
            this.groupBox2.Controls.Add(this.cmbFiltroAsignatura);
            this.groupBox2.Controls.Add(this.lblFiltroAsignatura);
            this.groupBox2.Location = new System.Drawing.Point(202, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Curso";
            // 
            // cmbFiltroTurno
            // 
            this.cmbFiltroTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTurno.FormattingEnabled = true;
            this.cmbFiltroTurno.Items.AddRange(new object[] {
            "TODOS",
            "Mañana",
            "Tarde",
            "Noche"});
            this.cmbFiltroTurno.Location = new System.Drawing.Point(100, 48);
            this.cmbFiltroTurno.Name = "cmbFiltroTurno";
            this.cmbFiltroTurno.Size = new System.Drawing.Size(208, 24);
            this.cmbFiltroTurno.TabIndex = 3;
            // 
            // lblFiltroTurno
            // 
            this.lblFiltroTurno.AutoSize = true;
            this.lblFiltroTurno.Location = new System.Drawing.Point(9, 56);
            this.lblFiltroTurno.Name = "lblFiltroTurno";
            this.lblFiltroTurno.Size = new System.Drawing.Size(49, 16);
            this.lblFiltroTurno.TabIndex = 2;
            this.lblFiltroTurno.Text = "Turno";
            // 
            // cmbFiltroAsignatura
            // 
            this.cmbFiltroAsignatura.FormattingEnabled = true;
            this.cmbFiltroAsignatura.Location = new System.Drawing.Point(100, 17);
            this.cmbFiltroAsignatura.Name = "cmbFiltroAsignatura";
            this.cmbFiltroAsignatura.Size = new System.Drawing.Size(208, 24);
            this.cmbFiltroAsignatura.TabIndex = 1;
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
            this.grpBoxFechasFiltroCursos.Controls.Add(this.lblFiltroFechaHasta);
            this.grpBoxFechasFiltroCursos.Controls.Add(this.mktxtFechaHastaFiltro);
            this.grpBoxFechasFiltroCursos.Controls.Add(this.lblFiltroFechaDesde);
            this.grpBoxFechasFiltroCursos.Controls.Add(this.mktxtFechaDesdeFiltro);
            this.grpBoxFechasFiltroCursos.Location = new System.Drawing.Point(7, 23);
            this.grpBoxFechasFiltroCursos.Name = "grpBoxFechasFiltroCursos";
            this.grpBoxFechasFiltroCursos.Size = new System.Drawing.Size(174, 79);
            this.grpBoxFechasFiltroCursos.TabIndex = 2;
            this.grpBoxFechasFiltroCursos.TabStop = false;
            this.grpBoxFechasFiltroCursos.Text = "Fechas";
            // 
            // lblFiltroFechaHasta
            // 
            this.lblFiltroFechaHasta.AutoSize = true;
            this.lblFiltroFechaHasta.Location = new System.Drawing.Point(7, 54);
            this.lblFiltroFechaHasta.Name = "lblFiltroFechaHasta";
            this.lblFiltroFechaHasta.Size = new System.Drawing.Size(51, 16);
            this.lblFiltroFechaHasta.TabIndex = 1;
            this.lblFiltroFechaHasta.Text = "Hasta";
            // 
            // mktxtFechaHastaFiltro
            // 
            this.mktxtFechaHastaFiltro.Location = new System.Drawing.Point(66, 47);
            this.mktxtFechaHastaFiltro.Mask = "00/00/0000";
            this.mktxtFechaHastaFiltro.Name = "mktxtFechaHastaFiltro";
            this.mktxtFechaHastaFiltro.Size = new System.Drawing.Size(100, 23);
            this.mktxtFechaHastaFiltro.TabIndex = 1;
            this.mktxtFechaHastaFiltro.ValidatingType = typeof(System.DateTime);
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
            // mktxtFechaDesdeFiltro
            // 
            this.mktxtFechaDesdeFiltro.Location = new System.Drawing.Point(66, 16);
            this.mktxtFechaDesdeFiltro.Mask = "00/00/0000";
            this.mktxtFechaDesdeFiltro.Name = "mktxtFechaDesdeFiltro";
            this.mktxtFechaDesdeFiltro.Size = new System.Drawing.Size(100, 23);
            this.mktxtFechaDesdeFiltro.TabIndex = 0;
            this.mktxtFechaDesdeFiltro.ValidatingType = typeof(System.DateTime);
            // 
            // dgvListaCursos
            // 
            this.dgvListaCursos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaCursos.Location = new System.Drawing.Point(5, 149);
            this.dgvListaCursos.Name = "dgvListaCursos";
            this.dgvListaCursos.Size = new System.Drawing.Size(767, 302);
            this.dgvListaCursos.TabIndex = 0;
            // 
            // tabABMEncargados
            // 
            this.tabABMEncargados.Controls.Add(this.grpBoxAbmDocentes);
            this.tabABMEncargados.Location = new System.Drawing.Point(4, 22);
            this.tabABMEncargados.Name = "tabABMEncargados";
            this.tabABMEncargados.Padding = new System.Windows.Forms.Padding(3);
            this.tabABMEncargados.Size = new System.Drawing.Size(787, 487);
            this.tabABMEncargados.TabIndex = 1;
            this.tabABMEncargados.Text = "ABM Encargados";
            this.tabABMEncargados.UseVisualStyleBackColor = true;
            // 
            // grpBoxAbmDocentes
            // 
            this.grpBoxAbmDocentes.Controls.Add(this.tabControl1);
            this.grpBoxAbmDocentes.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxAbmDocentes.Location = new System.Drawing.Point(8, 7);
            this.grpBoxAbmDocentes.Name = "grpBoxAbmDocentes";
            this.grpBoxAbmDocentes.Size = new System.Drawing.Size(776, 420);
            this.grpBoxAbmDocentes.TabIndex = 0;
            this.grpBoxAbmDocentes.TabStop = false;
            this.grpBoxAbmDocentes.Text = "ENCARGADOS";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabABMDocente);
            this.tabControl1.Controls.Add(this.tabVistaGeneralDocentes);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(770, 398);
            this.tabControl1.TabIndex = 0;
            // 
            // tabABMDocente
            // 
            this.tabABMDocente.Location = new System.Drawing.Point(4, 25);
            this.tabABMDocente.Name = "tabABMDocente";
            this.tabABMDocente.Padding = new System.Windows.Forms.Padding(3);
            this.tabABMDocente.Size = new System.Drawing.Size(762, 369);
            this.tabABMDocente.TabIndex = 0;
            this.tabABMDocente.Text = "Nuevo/Modificación";
            this.tabABMDocente.UseVisualStyleBackColor = true;
            // 
            // tabVistaGeneralDocentes
            // 
            this.tabVistaGeneralDocentes.Location = new System.Drawing.Point(4, 25);
            this.tabVistaGeneralDocentes.Name = "tabVistaGeneralDocentes";
            this.tabVistaGeneralDocentes.Padding = new System.Windows.Forms.Padding(3);
            this.tabVistaGeneralDocentes.Size = new System.Drawing.Size(762, 369);
            this.tabVistaGeneralDocentes.TabIndex = 1;
            this.tabVistaGeneralDocentes.Text = "Vista general";
            this.tabVistaGeneralDocentes.UseVisualStyleBackColor = true;
            // 
            // lbleMensajes
            // 
            this.lbleMensajes.AutoSize = true;
            this.lbleMensajes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lbleMensajes.Location = new System.Drawing.Point(235, 6);
            this.lbleMensajes.MaximumSize = new System.Drawing.Size(73, 0);
            this.lbleMensajes.Name = "lbleMensajes";
            this.lbleMensajes.Size = new System.Drawing.Size(73, 0);
            this.lbleMensajes.TabIndex = 20;
            // 
            // tripleGrillaAsistencias
            // 
            this.tripleGrillaAsistencias.AutoSize = true;
            this.tripleGrillaAsistencias.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tripleGrillaAsistencias.CantidadAlumnos = -1;
            this.tripleGrillaAsistencias.ComienzoClaseEsperado = "";
            this.tripleGrillaAsistencias.ComienzoClaseReal = "";
            this.tripleGrillaAsistencias.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tripleGrillaAsistencias.Encargado = "";
            this.tripleGrillaAsistencias.EstadoAsistencia = "";
            this.tripleGrillaAsistencias.FinClaseEsperado = "";
            this.tripleGrillaAsistencias.FinClaseReal = "";
            this.tripleGrillaAsistencias.Location = new System.Drawing.Point(3, 241);
            this.tripleGrillaAsistencias.Name = "tripleGrillaAsistencias";
            this.tripleGrillaAsistencias.NombreAsignatura = "";
            this.tripleGrillaAsistencias.NombreProfesor = "";
            this.tripleGrillaAsistencias.Observaciones = "";
            this.tripleGrillaAsistencias.Size = new System.Drawing.Size(781, 243);
            this.tripleGrillaAsistencias.TabIndex = 19;
            // 
            // planillaAsistencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(795, 513);
            this.Controls.Add(this.tabCargaAsistencia);
            this.Name = "planillaAsistencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Planilla de asistencia";
            this.Load += new System.EventHandler(this.planillaAsistencia_Load);
            this.Resize += new System.EventHandler(this.planillaAsistencia_Resize);
            this.tabCargaAsistencia.ResumeLayout(false);
            this.tabAsistencia.ResumeLayout(false);
            this.tabAsistencia.PerformLayout();
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
            this.tabFiltroCursos.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpBoxFiltrosCursos.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpBoxFechasFiltroCursos.ResumeLayout(false);
            this.grpBoxFechasFiltroCursos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCursos)).EndInit();
            this.tabABMEncargados.ResumeLayout(false);
            this.grpBoxAbmDocentes.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCargaAsistencia;
        private System.Windows.Forms.TabPage tabAsistencia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAsignatura;
        private System.Windows.Forms.GroupBox grpBoxDocente;
        private System.Windows.Forms.GroupBox grpBoxHorarios;
        private System.Windows.Forms.GroupBox grpBoxHoraReal;
        private System.Windows.Forms.Label lblHoraSalidaReal;
        private System.Windows.Forms.Label lblHoraEntradaReal;
        private System.Windows.Forms.GroupBox grpBoxHoraEsperada;
        private System.Windows.Forms.Label lblHoraSalidaEsperada;
        private System.Windows.Forms.Label lblHoraEntradaEsperada;
        private System.Windows.Forms.DateTimePicker datePickerCargaAsistencia;
        private System.Windows.Forms.TabPage tabABMEncargados;
        private System.Windows.Forms.GroupBox grpBoxAbmDocentes;
        private System.Windows.Forms.TabPage tabFiltroCursos;
        private System.Windows.Forms.NumericUpDown numUpDownAlumnos;
        private System.Windows.Forms.GroupBox grpBoxGeneral;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.ComboBox cmbEstadoAsistencia;
        private System.Windows.Forms.Label lblCantidadAlumnos;
        private System.Windows.Forms.Label lblAsistencia;
        private System.Windows.Forms.MaskedTextBox mktxtHoraSalidaReal;
        private System.Windows.Forms.MaskedTextBox mktxtHoraEntradaReal;
        private System.Windows.Forms.MaskedTextBox mktxtHoraSalidaEsperada;
        private System.Windows.Forms.MaskedTextBox mktxtHoraEntradaEsperada;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabABMDocente;
        private System.Windows.Forms.TabPage tabVistaGeneralDocentes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpBoxFiltrosCursos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbFiltroTurno;
        private System.Windows.Forms.Label lblFiltroTurno;
        private System.Windows.Forms.ComboBox cmbFiltroAsignatura;
        private System.Windows.Forms.Label lblFiltroAsignatura;
        private System.Windows.Forms.GroupBox grpBoxFechasFiltroCursos;
        private System.Windows.Forms.Label lblFiltroFechaHasta;
        private System.Windows.Forms.MaskedTextBox mktxtFechaHastaFiltro;
        private System.Windows.Forms.Label lblFiltroFechaDesde;
        private System.Windows.Forms.MaskedTextBox mktxtFechaDesdeFiltro;
        private System.Windows.Forms.DataGridView dgvListaCursos;
        private System.Windows.Forms.ComboBox cmbDocente;
        private System.Windows.Forms.Button btnGuardar;
        private PlanillaAsistencia.ControlesPersonalizados.TripleGrillaAsistencias tripleGrillaAsistencias;
        private ControlesPersonalizados.LabelConEfectos lbleMensajes;
    }
}

