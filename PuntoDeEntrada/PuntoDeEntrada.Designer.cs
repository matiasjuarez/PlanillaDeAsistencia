﻿namespace PuntoDeEntrada
{
    partial class PuntoDeEntrada
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uSUARIOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sESIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDMINISTRARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLANILLAASISTENCIAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oBJETOSPERDIDOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSUARIOSToolStripMenuItem,
            this.pLANILLAASISTENCIAToolStripMenuItem,
            this.oBJETOSPERDIDOSToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(711, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // uSUARIOSToolStripMenuItem
            // 
            this.uSUARIOSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sESIONToolStripMenuItem,
            this.aDMINISTRARToolStripMenuItem});
            this.uSUARIOSToolStripMenuItem.Name = "uSUARIOSToolStripMenuItem";
            this.uSUARIOSToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.uSUARIOSToolStripMenuItem.Text = "USUARIOS";
            // 
            // sESIONToolStripMenuItem
            // 
            this.sESIONToolStripMenuItem.Name = "sESIONToolStripMenuItem";
            this.sESIONToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sESIONToolStripMenuItem.Text = "SESION";
            this.sESIONToolStripMenuItem.Click += new System.EventHandler(this.sESIONToolStripMenuItem_Click);
            // 
            // aDMINISTRARToolStripMenuItem
            // 
            this.aDMINISTRARToolStripMenuItem.Name = "aDMINISTRARToolStripMenuItem";
            this.aDMINISTRARToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aDMINISTRARToolStripMenuItem.Text = "ADMINISTRAR";
            this.aDMINISTRARToolStripMenuItem.Click += new System.EventHandler(this.aDMINISTRARToolStripMenuItem_Click);
            // 
            // pLANILLAASISTENCIAToolStripMenuItem
            // 
            this.pLANILLAASISTENCIAToolStripMenuItem.Name = "pLANILLAASISTENCIAToolStripMenuItem";
            this.pLANILLAASISTENCIAToolStripMenuItem.Size = new System.Drawing.Size(139, 20);
            this.pLANILLAASISTENCIAToolStripMenuItem.Text = "PLANILLA ASISTENCIA";
            this.pLANILLAASISTENCIAToolStripMenuItem.Click += new System.EventHandler(this.pLANILLAASISTENCIAToolStripMenuItem_Click);
            // 
            // oBJETOSPERDIDOSToolStripMenuItem
            // 
            this.oBJETOSPERDIDOSToolStripMenuItem.Name = "oBJETOSPERDIDOSToolStripMenuItem";
            this.oBJETOSPERDIDOSToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.oBJETOSPERDIDOSToolStripMenuItem.Text = "OBJETOS PERDIDOS";
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 24);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(711, 237);
            this.panelPrincipal.TabIndex = 1;
            // 
            // PuntoDeEntrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 261);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PuntoDeEntrada";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uSUARIOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sESIONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDMINISTRARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pLANILLAASISTENCIAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oBJETOSPERDIDOSToolStripMenuItem;
        private System.Windows.Forms.Panel panelPrincipal;
    }
}
