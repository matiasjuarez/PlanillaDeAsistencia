using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utilidades
{
    public class CargadorCombo
    {
        public static void cargar<T>(ComboBox combo, List<T> data, string displayMember, string valueMember)
        {
            BindingSource source = new BindingSource();
            source.DataSource = data;

            combo.DataSource = source;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
        }
    }
}
