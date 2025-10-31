using Lowest_Checker.CheckVersion;
using Lowest_Checker.GUI;
using Lowest_Checker.LoadConfig;
using Lowest_Checker.stage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lowest_Checker
{
    public partial class MainStage : Form
    {
        Panel mPanel;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool dragging = false;
        private stage.MicrosoftStage ms;
        public MainStage()
        {
            InitializeComponent();
            
            this.FormBorderStyle = FormBorderStyle.None; // 无边框
            this.BackColor = Color.Black ; // 背景颜色
            this.Padding = new Padding(1); // 设置边框厚度
            label1.MouseDown += new MouseEventHandler(DragLabel_MouseDown);
            label1.MouseMove += new MouseEventHandler(DragLabel_MouseMove);
            label1.MouseUp += new MouseEventHandler(DragLabel_MouseUp);
            ms = new stage.MicrosoftStage();
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

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void changeColor(string s)
        {
            if(s == "ms")
            {
                this.label4.BackColor = Color.FromArgb(51, 90, 204);
                this.label7.BackColor = SystemColors.ControlDarkDark;
            }
            else if(s == "home")
            {
                this.label7.BackColor = Color.FromArgb(51, 90, 204);
                this.label4.BackColor = SystemColors.ControlDarkDark;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ReadMicrosoftConfig.read();
            changeColor("ms");
            mPanel = ms.GetPanel();
            mPanel.Visible = false;
            this.Controls.Add(mPanel);
            mPanel.Location = panel2.Location;
            mPanel.BringToFront();
            mPanel.Visible = true;
            this.panel2.Visible = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            changeColor("home");
            this.panel2.Visible=true;
            this.Controls.Remove(mPanel);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ConsoleStage c = new ConsoleStage();
            c.Show();
        }

        private void MainStage_Load(object sender, EventArgs e)
        {
            if (!CheckUpdate.check())
            {
                UpdateStage updateStage = new UpdateStage();
                updateStage.ShowDialog();
            }
        }
    }
}
