using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emitirOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap captura = null;

        private void CapturarForm()
        {
            button1.Visible = false;
            button2.Visible = false;
            var formBorderStyleAnterior = this.FormBorderStyle;

            try
            {
                this.FormBorderStyle = FormBorderStyle.None;

                WindowHelper.Rect rect;
                WindowHelper.DwmGetWindowAttribute(this.Handle, (int)WindowHelper.Dwmwindowattribute.DwmwaExtendedFrameBounds,
                    out rect, System.Runtime.InteropServices.Marshal.SizeOf(typeof(WindowHelper.Rect)));
                var rectangle = rect.ToRectangle();

                captura = new Bitmap(rectangle.Width, rectangle.Height);
                var graphics = Graphics.FromImage(captura);
                graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, rectangle.Size);
            }
            finally
            {
                this.FormBorderStyle = formBorderStyleAnterior;
            }
        }

        public static class WindowHelper
        {
            [Serializable, System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
            public struct Rect
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;

                public System.Drawing.Rectangle ToRectangle()
                {
                    return System.Drawing.Rectangle.FromLTRB(Left, Top, Right, Bottom);
                }
            }

            [System.Runtime.InteropServices.DllImport(@"dwmapi.dll")]
            public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rect pvAttribute, int cbAttribute);

            public enum Dwmwindowattribute
            {
                DwmwaExtendedFrameBounds = 9
            }
        }
        
        private void label14_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            CapturarForm();
            printDocument1.Print();
            Application.Exit();
        }
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(captura, 20, 20);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
