using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilidades;

namespace Utilidades
{
    public partial class ResizableControl : UserControl
    {
        private Escalador escalador;

        public ResizableControl()
        {
            InitializeComponent();
        }

        protected void inicializarEscalador()
        {
            escalador = new Escalador(this);
        }

        private void ResizableControl_SizeChanged(object sender, EventArgs e)
        {
            if (escalador != null)
            {
                escalador.resize();
                Update();
            }
        }
    }
}
