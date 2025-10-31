using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lowest_Checker.GUI
{
    public partial class ConsoleStage : Form
    {
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool dragging = false;
        public ConsoleStage()
        {
            InitializeComponent();
            RedirectConsoleOutput();
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
        private void RedirectConsoleOutput()
        {
            // 创建一个 StringWriter 对象，用于捕获控制台输出
            StringWriter writer = new StringWriter();
            // 将控制台的标准输出重定向到 StringWriter
            Console.SetOut(writer);

            // 创建一个定时器，定期检查 StringWriter 中是否有新的输出
            Timer timer = new Timer();
            timer.Interval = 200; // 每隔100毫秒检查一次
            timer.Tick += (sender, e) =>
            {
                // 检查 StringWriter 中是否有新的输出
                string newText = writer.ToString();
                if (!string.IsNullOrEmpty(newText))
                {
                    // 将新的输出追加到 RichTextBox 中
                    this.richTextBox1.AppendText(newText);
                    // 清空 StringWriter，以便捕获下一次输出
                    writer.GetStringBuilder().Clear();
                }
            };
            timer.Start();
        }
    }
}
