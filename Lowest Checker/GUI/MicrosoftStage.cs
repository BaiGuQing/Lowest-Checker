using Lowest_Checker.LoadConfig;
using Lowest_Checker.Microsoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lowest_Checker.stage
{

    public partial class MicrosoftStage : UserControl
    {
        private Timer _timer;
        public MicrosoftStage()
        {
            InitializeComponent();
            _timer = new Timer();
            _timer.Interval = 200;
            _timer.Tick += new EventHandler(OnTimedEvent);
            _timer.Start();
            
        }
        private void OnTimedEvent(Object sender, EventArgs e)
        {
            this.label8.Text = Checker.Microsoft_Vailed.ToString();
            this.label9.Text = Checker.Microsoft_Invailed.ToString();
            this.label10.Text = Checker.Microsoft_2FAs.ToString();
            this.label22.Text = Checker.Microsoft_Unknown.ToString();
            this.label20.Text = Checker.Microsoft_Blocked.ToString();
            this.label12.Text = Checker.proxies.Count.ToString();
            this.label11.Text = LoadCombos.Microsoft_Combos.Count.ToString();
            this.label2.Text = Checker.Microsoft_ProxiyErrors.ToString();
            this.label25.Text = Checker.Microsoft_Locked.ToString();
            double d = 0;
            if (Checker.Microsoft_Process > 0)
            {
                d = (Checker.Microsoft_Vailed / Checker.Microsoft_Process) * 100;
            }
            this.label15.Text = d.ToString() + "%";
            this.label16.Text = Checker.Microsoft_Process.ToString();
        }

        public Panel GetPanel()
        {
            return this.panel1;
        }

        private void label18_Click(object sender, EventArgs e)
        {
            LoadProxiesStage loadProxies = new LoadProxiesStage();
            loadProxies.ShowDialog();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            LoadCombos.readCombo();
            this.label11.Text = LoadCombos.Microsoft_Combos.Count.ToString();
        }



        private async void label1_Click(object sender, EventArgs e)
        {
            ReadMicrosoftConfig.read();
            await Checker.checker();
        }
    }
}
