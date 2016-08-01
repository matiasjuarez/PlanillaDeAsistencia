namespace PlanillaAsistencia.ControlesPersonalizados
{
    partial class LabelConEfectos
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
            this.lblTexto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTexto
            // 
            this.lblTexto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(0, 0);
            this.lblTexto.MaximumSize = this.Size;
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(57, 13);
            this.lblTexto.TabIndex = 0;
            this.lblTexto.Text = "MENSAJE";
            // 
            // LabelConEfectos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lblTexto);
            this.Name = "LabelConEfectos";
            this.Size = new System.Drawing.Size(73, 13);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTexto;
    }
}
