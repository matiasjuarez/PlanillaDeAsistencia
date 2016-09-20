namespace AdministracionPersonal.Administracion
{
    partial class PantallaAdministracionPersonal
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
            this.listEncargados = new System.Windows.Forms.ListBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLegajo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMailBBS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMailPersonal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEliminarFoto = new System.Windows.Forms.Button();
            this.btnTomarFoto = new System.Windows.Forms.Button();
            this.btnSeleccionarFoto = new System.Windows.Forms.Button();
            this.pbFoto = new System.Windows.Forms.PictureBox();
            this.btnBajaEncargado = new System.Windows.Forms.Button();
            this.btnModificarEncargado = new System.Windows.Forms.Button();
            this.btnNuevoEncargado = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.panelUsuario = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReiniciarContrasena = new System.Windows.Forms.Button();
            this.btnBloquear = new System.Windows.Forms.Button();
            this.lblHabilitado = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAgregarRol = new System.Windows.Forms.Button();
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panelUsuario.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listEncargados
            // 
            this.listEncargados.FormattingEnabled = true;
            this.listEncargados.Location = new System.Drawing.Point(3, 3);
            this.listEncargados.Name = "listEncargados";
            this.listEncargados.Size = new System.Drawing.Size(165, 212);
            this.listEncargados.TabIndex = 0;
            this.listEncargados.Click += new System.EventHandler(this.listEncargados_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(8, 28);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 13);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "NOMBRE";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(91, 21);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(166, 20);
            this.txtNombre.TabIndex = 2;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(374, 21);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(166, 20);
            this.txtApellido.TabIndex = 4;
            this.txtApellido.TextChanged += new System.EventHandler(this.txtApellido_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(291, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "APELLIDO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "DOCUMENTO";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(91, 58);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(166, 20);
            this.txtDocumento.TabIndex = 6;
            this.txtDocumento.TextChanged += new System.EventHandler(this.txtDocumento_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "NACIMIENTO";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "LEGAJO";
            // 
            // txtLegajo
            // 
            this.txtLegajo.Location = new System.Drawing.Point(576, 118);
            this.txtLegajo.Name = "txtLegajo";
            this.txtLegajo.Size = new System.Drawing.Size(166, 20);
            this.txtLegajo.TabIndex = 10;
            this.txtLegajo.TextChanged += new System.EventHandler(this.txtLegajo_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMailBBS);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtMailPersonal);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtTelefono);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(180, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 102);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CONTACTO";
            // 
            // txtMailBBS
            // 
            this.txtMailBBS.Location = new System.Drawing.Point(107, 71);
            this.txtMailBBS.Name = "txtMailBBS";
            this.txtMailBBS.Size = new System.Drawing.Size(166, 20);
            this.txtMailBBS.TabIndex = 16;
            this.txtMailBBS.TextChanged += new System.EventHandler(this.txtMailBBS_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "MAIL BBS";
            // 
            // txtMailPersonal
            // 
            this.txtMailPersonal.Location = new System.Drawing.Point(107, 45);
            this.txtMailPersonal.Name = "txtMailPersonal";
            this.txtMailPersonal.Size = new System.Drawing.Size(166, 20);
            this.txtMailPersonal.TabIndex = 14;
            this.txtMailPersonal.TextChanged += new System.EventHandler(this.txtMailPersonal_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "MAIL PERSONAL";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(107, 19);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(166, 20);
            this.txtTelefono.TabIndex = 12;
            this.txtTelefono.TextChanged += new System.EventHandler(this.txtTelefono_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "TELEFONO";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpFechaNacimiento);
            this.groupBox2.Controls.Add(this.lblNombre);
            this.groupBox2.Controls.Add(this.txtNombre);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtApellido);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDocumento);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(180, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(570, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATOS PERSONALES";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(371, 57);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(169, 20);
            this.dtpFechaNacimiento.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEliminarFoto);
            this.groupBox3.Controls.Add(this.btnTomarFoto);
            this.groupBox3.Controls.Add(this.btnSeleccionarFoto);
            this.groupBox3.Controls.Add(this.pbFoto);
            this.groupBox3.Location = new System.Drawing.Point(474, 155);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(276, 254);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FOTO";
            // 
            // btnEliminarFoto
            // 
            this.btnEliminarFoto.Location = new System.Drawing.Point(195, 221);
            this.btnEliminarFoto.Name = "btnEliminarFoto";
            this.btnEliminarFoto.Size = new System.Drawing.Size(75, 23);
            this.btnEliminarFoto.TabIndex = 3;
            this.btnEliminarFoto.Text = "Eliminar";
            this.btnEliminarFoto.UseVisualStyleBackColor = true;
            this.btnEliminarFoto.Click += new System.EventHandler(this.btnEliminarFoto_Click);
            // 
            // btnTomarFoto
            // 
            this.btnTomarFoto.Location = new System.Drawing.Point(87, 221);
            this.btnTomarFoto.Name = "btnTomarFoto";
            this.btnTomarFoto.Size = new System.Drawing.Size(75, 23);
            this.btnTomarFoto.TabIndex = 2;
            this.btnTomarFoto.Text = "Camara";
            this.btnTomarFoto.UseVisualStyleBackColor = true;
            this.btnTomarFoto.Click += new System.EventHandler(this.btnTomarFoto_Click);
            // 
            // btnSeleccionarFoto
            // 
            this.btnSeleccionarFoto.Location = new System.Drawing.Point(6, 221);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Size = new System.Drawing.Size(75, 23);
            this.btnSeleccionarFoto.TabIndex = 1;
            this.btnSeleccionarFoto.Text = "Seleccionar";
            this.btnSeleccionarFoto.UseVisualStyleBackColor = true;
            this.btnSeleccionarFoto.Click += new System.EventHandler(this.btnSeleccionarFoto_Click);
            // 
            // pbFoto
            // 
            this.pbFoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbFoto.Image = global::AdministracionPersonal.Properties.Resources.mystery;
            this.pbFoto.InitialImage = global::AdministracionPersonal.Properties.Resources.mystery;
            this.pbFoto.Location = new System.Drawing.Point(6, 19);
            this.pbFoto.Name = "pbFoto";
            this.pbFoto.Size = new System.Drawing.Size(264, 196);
            this.pbFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFoto.TabIndex = 0;
            this.pbFoto.TabStop = false;
            // 
            // btnBajaEncargado
            // 
            this.btnBajaEncargado.Location = new System.Drawing.Point(177, 10);
            this.btnBajaEncargado.Name = "btnBajaEncargado";
            this.btnBajaEncargado.Size = new System.Drawing.Size(75, 23);
            this.btnBajaEncargado.TabIndex = 16;
            this.btnBajaEncargado.Text = "Baja";
            this.btnBajaEncargado.UseVisualStyleBackColor = true;
            this.btnBajaEncargado.Click += new System.EventHandler(this.btnBajaEncargado_Click);
            // 
            // btnModificarEncargado
            // 
            this.btnModificarEncargado.Location = new System.Drawing.Point(96, 10);
            this.btnModificarEncargado.Name = "btnModificarEncargado";
            this.btnModificarEncargado.Size = new System.Drawing.Size(75, 23);
            this.btnModificarEncargado.TabIndex = 15;
            this.btnModificarEncargado.Text = "Modificar";
            this.btnModificarEncargado.UseVisualStyleBackColor = true;
            this.btnModificarEncargado.Click += new System.EventHandler(this.btnModificarEncargado_Click);
            // 
            // btnNuevoEncargado
            // 
            this.btnNuevoEncargado.Location = new System.Drawing.Point(15, 10);
            this.btnNuevoEncargado.Name = "btnNuevoEncargado";
            this.btnNuevoEncargado.Size = new System.Drawing.Size(75, 23);
            this.btnNuevoEncargado.TabIndex = 14;
            this.btnNuevoEncargado.Text = "Alta";
            this.btnNuevoEncargado.UseVisualStyleBackColor = true;
            this.btnNuevoEncargado.Click += new System.EventHandler(this.btnNuevoEncargado_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCancelar);
            this.groupBox4.Controls.Add(this.btnGuardarCambios);
            this.groupBox4.Controls.Add(this.btnModificarEncargado);
            this.groupBox4.Controls.Add(this.btnBajaEncargado);
            this.groupBox4.Controls.Add(this.btnNuevoEncargado);
            this.groupBox4.Location = new System.Drawing.Point(3, 415);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(747, 39);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(666, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 18;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.Location = new System.Drawing.Point(558, 10);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarCambios.TabIndex = 17;
            this.btnGuardarCambios.Text = "Guardar";
            this.btnGuardarCambios.UseVisualStyleBackColor = true;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // panelUsuario
            // 
            this.panelUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUsuario.Controls.Add(this.panel2);
            this.panelUsuario.Controls.Add(this.lblHabilitado);
            this.panelUsuario.Controls.Add(this.label8);
            this.panelUsuario.Controls.Add(this.btnAgregarRol);
            this.panelUsuario.Controls.Add(this.cmbRoles);
            this.panelUsuario.Controls.Add(this.txtNombreUsuario);
            this.panelUsuario.Controls.Add(this.label9);
            this.panelUsuario.Controls.Add(this.label10);
            this.panelUsuario.Location = new System.Drawing.Point(3, 224);
            this.panelUsuario.Name = "panelUsuario";
            this.panelUsuario.Size = new System.Drawing.Size(377, 158);
            this.panelUsuario.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReiniciarContrasena);
            this.panel2.Controls.Add(this.btnBloquear);
            this.panel2.Location = new System.Drawing.Point(3, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(367, 41);
            this.panel2.TabIndex = 19;
            // 
            // btnReiniciarContrasena
            // 
            this.btnReiniciarContrasena.Location = new System.Drawing.Point(3, 3);
            this.btnReiniciarContrasena.Name = "btnReiniciarContrasena";
            this.btnReiniciarContrasena.Size = new System.Drawing.Size(124, 36);
            this.btnReiniciarContrasena.TabIndex = 7;
            this.btnReiniciarContrasena.Text = "Reiniciar password";
            this.btnReiniciarContrasena.UseVisualStyleBackColor = true;
            this.btnReiniciarContrasena.Click += new System.EventHandler(this.btnReiniciarContrasena_Click);
            // 
            // btnBloquear
            // 
            this.btnBloquear.Location = new System.Drawing.Point(248, 3);
            this.btnBloquear.Name = "btnBloquear";
            this.btnBloquear.Size = new System.Drawing.Size(116, 35);
            this.btnBloquear.TabIndex = 6;
            this.btnBloquear.Text = "BLOQUEAR";
            this.btnBloquear.UseVisualStyleBackColor = true;
            this.btnBloquear.Click += new System.EventHandler(this.btnBloquear_Click);
            // 
            // lblHabilitado
            // 
            this.lblHabilitado.AutoSize = true;
            this.lblHabilitado.Location = new System.Drawing.Point(149, 80);
            this.lblHabilitado.Name = "lblHabilitado";
            this.lblHabilitado.Size = new System.Drawing.Size(0, 13);
            this.lblHabilitado.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "HABILITADO";
            // 
            // btnAgregarRol
            // 
            this.btnAgregarRol.Location = new System.Drawing.Point(118, 47);
            this.btnAgregarRol.Name = "btnAgregarRol";
            this.btnAgregarRol.Size = new System.Drawing.Size(28, 22);
            this.btnAgregarRol.TabIndex = 16;
            this.btnAgregarRol.Text = "+";
            this.btnAgregarRol.UseVisualStyleBackColor = true;
            this.btnAgregarRol.Click += new System.EventHandler(this.btnAgregarRol_Click);
            // 
            // cmbRoles
            // 
            this.cmbRoles.FormattingEnabled = true;
            this.cmbRoles.Location = new System.Drawing.Point(152, 47);
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.Size = new System.Drawing.Size(218, 21);
            this.cmbRoles.TabIndex = 15;
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(152, 10);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(218, 20);
            this.txtNombreUsuario.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "ROL";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "USUARIO";
            // 
            // PantallaAdministracionUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.panelUsuario);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLegajo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listEncargados);
            this.Name = "PantallaAdministracionUsuarios";
            this.Size = new System.Drawing.Size(758, 461);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.panelUsuario.ResumeLayout(false);
            this.panelUsuario.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listEncargados;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLegajo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMailBBS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMailPersonal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnEliminarFoto;
        private System.Windows.Forms.Button btnTomarFoto;
        private System.Windows.Forms.Button btnSeleccionarFoto;
        private System.Windows.Forms.PictureBox pbFoto;
        private System.Windows.Forms.Button btnBajaEncargado;
        private System.Windows.Forms.Button btnModificarEncargado;
        private System.Windows.Forms.Button btnNuevoEncargado;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Panel panelUsuario;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReiniciarContrasena;
        private System.Windows.Forms.Button btnBloquear;
        private System.Windows.Forms.Label lblHabilitado;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAgregarRol;
        private System.Windows.Forms.ComboBox cmbRoles;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}
