using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Utilidades
{
    public class Escalador
    {
        private Control parentControl;
        private float originalWidth;
        private float originalHeight;
        private List<OriginalDimension> originalDimensions;

        public Escalador(Control control)
        {
            this.parentControl = control;
            originalWidth = control.Width;
            originalHeight = control.Height;

            originalDimensions = new List<OriginalDimension>();
            saveOriginalSizes(parentControl);
        }

        private void saveOriginalSizes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                OriginalDimension dimension = new OriginalDimension(control);
                originalDimensions.Add(dimension);
                saveOriginalSizes(control);
            }
        }

        private void resizeAllChildren(Control parent)
        {
            float widthRatio = parent.Width / this.originalWidth;
            float heigthRatio = parent.Height / this.originalHeight;

            foreach (Control control in parent.Controls)
            {
                //control.Left = (int) (control.Left * widthRatio);
                control.Width = (int)(control.Width * widthRatio);
                resizeAllChildren(control);
            }
        }

        public void resize()
        {
            float widthRatio = parentControl.Width / this.originalWidth;
            float heightRatio = parentControl.Height / this.originalHeight;

            foreach (OriginalDimension dimension in originalDimensions)
            {
                Control ctrl = dimension.Control;
                ctrl.Left = (int)(dimension.oLeft * widthRatio);
                ctrl.Width = (int)(dimension.oWidth * widthRatio);
                ctrl.Top = (int)(dimension.oTop * heightRatio);
                ctrl.Height = (int)(dimension.oHeight * heightRatio);

                float menor = widthRatio;
                if(heightRatio < menor) menor = heightRatio;
                ctrl.Font = new Font(dimension.oFont.FontFamily, 
                    dimension.oFont.Size * menor, dimension.oFont.Style);
            }
        }

        private class OriginalDimension
        {
            private Control control;

            public Control Control
            {
                get { return control; }
                set { control = value; }
            }

            private Font font;

            public Font oFont
            {
                get { return font; }
                set { font = value; }
            }

            public OriginalDimension(Control control)
            {
                this.control = control;
                oLeft = control.Left;
                oTop = control.Top;
                oWidth = control.Width;
                oHeight = control.Height;

                Font ctrlFont = Control.Font;
                Font font = new Font(ctrlFont.FontFamily, ctrlFont.SizeInPoints, ctrlFont.Style);
                oFont = font;
            }

            private float left;

            public float oLeft
            {
                get { return left; }
                set { left = value; }
            }
            private float top;

            public float oTop
            {
                get { return top; }
                set { top = value; }
            }
            private float width;

            public float oWidth
            {
                get { return width; }
                set { width = value; }
            }
            private float height;

            public float oHeight
            {
                get { return height; }
                set { height = value; }
            }
        }
    }
}
