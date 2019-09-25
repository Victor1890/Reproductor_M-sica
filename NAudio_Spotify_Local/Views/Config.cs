using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAudio_Spotify_Local.Views
{
    public partial class Config: UserControl
    {
        public Config()
        {
            InitializeComponent();
        }
        ColorDialog color = new ColorDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            if (color.Color == color.Color)
            {
                color.Reset();
            }
            
            if (color.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default["MyColor"] = color.Color;
                Properties.Settings.Default.Save();
            }

            this.BackColor = Properties.Settings.Default.MyColor;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked)
            {
                Properties.Settings.Default["MyColor"] = Color.FromArgb(17,17,17);
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default["MyColor"] = Color.White;
                Properties.Settings.Default.Save();
            }
        }
    }
}
