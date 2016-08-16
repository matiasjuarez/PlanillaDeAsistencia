namespace PlanillaAsistencia.Pantallas.ABMCEncargados
{
    partial class ABMCEncargados
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
            this.mkFechaNacimiento = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // listEncargados
            // 
            this.listEncargados.FormattingEnabled = true;
            this.listEncargados.Location = new System.Drawing.Point(3, 3);
            this.listEncargados.Name = "listEncargados";
            this.listEncargados.Size = new System.Drawing.Size(165, 446);
            this.listEncargados.TabIndex = 0;
            this.listEncargados.SelectedIndexChanged += new System.EventHandler(this.listEncargados_SelectedIndexChanged);
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
            this.label4.Location = new System.Drawing.Point(177, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "LEGAJO";
            // 
            // txtLegajo
            // 
            this.txtLegajo.Location = new System.Drawing.Point(231, 228);
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
            this.groupBox2.Controls.Add(this.mkFechaNacimiento);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEliminarFoto);
            this.groupBox3.Controls.Add(this.btnTomarFoto);
            this.groupBox3.Controls.Add(this.btnSeleccionarFoto);
            this.groupBox3.Controls.Add(this.pbFoto);
            this.groupBox3.Location = new System.Drawing.Point(474, 118);
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
            this.pbFoto.Image = global::PlanillaAsistencia.Properties.Resources.mystery;
            this.pbFoto.InitialImage = global::PlanillaAsistencia.Properties.Resources.mystery;
            this.pbFoto.Location = new System.Drawing.Point(6, 19);
            this.pbFoto.Name = "pbFoto";
            this.pbFoto.Size = new System.Drawing.Size(264, 196);
            this.pbFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFoto.TabIndex = 0;
            this.pbFoto.TabStop = false;
            // 
            // btnBajaEncargado
            // 
            this.btnBajaEncargado.Location = new System.Drawing.Point(177, 19);
            this.btnBajaEncargado.Name = "btnBajaEncargado";
            this.btnBajaEncargado.Size = new System.Drawing.Size(75, 23);
            this.btnBajaEncargado.TabIndex = 16;
            this.btnBajaEncargado.Text = "Baja";
            this.btnBajaEncargado.UseVisualStyleBackColor = true;
            // 
            // btnModificarEncargado
            // 
            this.btnModificarEncargado.Location = new System.Drawing.Point(96, 19);
            this.btnModificarEncargado.Name = "btnModificarEncargado";
            this.btnModificarEncargado.Size = new System.Drawing.Size(75, 23);
            this.btnModificarEncargado.TabIndex = 15;
            this.btnModificarEncargado.Text = "Modificar";
            this.btnModificarEncargado.UseVisualStyleBackColor = true;
            this.btnModificarEncargado.Click += new System.EventHandler(this.btnModificarEncargado_Click);
            // 
            // btnNuevoEncargado
            // 
            this.btnNuevoEncargado.Location = new System.Drawing.Point(15, 19);
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
            this.groupBox4.Location = new System.Drawing.Point(180, 400);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(564, 49);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(483, 19);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 18;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.Location = new System.Drawing.Point(381, 19);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarCambios.TabIndex = 17;
            this.btnGuardarCambios.Text = "Guardar";
            this.btnGuardarCambios.UseVisualStyleBackColor = true;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // mkFechaNacimiento
            // 
            this.mkFechaNacimiento.Location = new System.Drawing.Point(374, 56);
            this.mkFechaNacimiento.Mask = "00/00/0000";
            this.mkFechaNacimiento.Name = "mkFechaNacimiento";
            this.mkFechaNacimiento.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mkFechaNacimiento.Size = new System.Drawing.Size(166, 20);
            this.mkFechaNacimiento.TabIndex = 8;
            this.mkFechaNacimiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mkFechaNacimiento.ValidatingType = typeof(System.DateTime);
            this.mkFechaNacimiento.TextChanged += new System.EventHandler(this.mkFechaNacimiento_TextChanged);
            // 
            // ABMCEncargados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLegajo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listEncargados);
            this.Name = "ABMCEncargados";
            this.Size = new System.Drawing.Size(758, 461);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).EndInit();
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.MaskedTextBox mkFechaNacimiento;
    }
}
