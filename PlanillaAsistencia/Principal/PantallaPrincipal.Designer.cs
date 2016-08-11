namespace PlanillaAsistencia.Principal
{
    partial class PantallaPrincipal
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.sESIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iNICIARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cERRARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 24);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(799, 517);
            this.tabs.TabIndex = 0;
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sESIONToolStripMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(799, 24);
            this.menuPrincipal.TabIndex = 2;
            this.menuPrincipal.Text = "menuStrip2";
            // 
            // sESIONToolStripMenuItem
            // 
            this.sESIONToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iNICIARToolStripMenuItem,
            this.cERRARToolStripMenuItem});
            this.sESIONToolStripMenuItem.Name = "sESIONToolStripMenuItem";
            this.sESIONToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.sESIONToolStripMenuItem.Text = "SESION";
            // 
            // iNICIARToolStripMenuItem
            // 
            this.iNICIARToolStripMenuItem.Name = "iNICIARToolStripMenuItem";
            this.iNICIARToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.iNICIARToolStripMenuItem.Text = "INICIAR";
            // 
            // cERRARToolStripMenuItem
            // 
            this.cERRARToolStripMenuItem.Name = "cERRARToolStripMenuItem";
            this.cERRARToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cERRARToolStripMenuItem.Text = "CERRAR";
            // 
            // PantallaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 541);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.menuPrincipal);
            this.Name = "PantallaPrincipal";
            this.Text = "PantallaPrincipal";
            this.Resize += new System.EventHandler(this.PantallaPrincipal_Resize);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem sESIONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iNICIARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cERRARToolStripMenuItem;
    }
}