using Lowest_Checker.Microsoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lowest_Checker.stage
{
    public partial class LoadProxiesStage : Form
    {
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool dragging = false;
        public LoadProxiesStage()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // 无边框
            this.BackColor = Color.Black; // 背景颜色
            this.Padding = new Padding(1); // 设置边框厚度
            label1.MouseDown += new MouseEventHandler(DragLabel_MouseDown);
            label1.MouseMove += new MouseEventHandler(DragLabel_MouseMove);
            label1.MouseUp += new MouseEventHandler(DragLabel_MouseUp);
        }
        private void DragLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void DragLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void DragLabel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            LoadProxies.readProxies();
            this.Close();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            LoadProxies.readApis();
            this.Close();
        }
    }
}
