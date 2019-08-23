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


namespace NAudio_Spotify_Local
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }
        Classes.Music Music = new Classes.Music();
        

        //Variables
        private string[] _rutasM;




        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btInstagram_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/victorrosario.1890/");
        }

        private void btGitHub_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Victor1890");
        }

        public void play_Items1_onAction(object sender, EventArgs e)
        {
            //Cambiar el botón del Play a Pause
            play_Items1.isPlaying = false;
            //play_Items2.isPlaying = false;
            //play_Items3.isPlaying = false;

            ((Play_Items)sender).isPlaying = true;
            string song = (string)((Play_Items)sender).Tag;

            //Play Song
            thumbnail.Image = ((Play_Items)sender).Thumbnail;
            thumbnail2.Image = ((Play_Items)sender).Thumbnail;

            //Name, Artist Song
            l_Song_1.Text = ((Play_Items)sender).Song;
            l_Artist_1.Text = ((Play_Items)sender).Artist;
            l_Artist_1.Text = ((Play_Items)sender).Artist;

            //Effecto de audio
            if (Pic_effects.Visible == false)
            {
                btPlay.Image = Properties.Resources.Pause;
                Pic_effects.Visible = true;
            }
            else
            {
                btPlay.Image = Properties.Resources.Play;
                Pic_effects.Visible = false;
            }
        }

        private void btPlay_Click(object sender, EventArgs e)
        {
            if (Pic_effects.Visible == false)
            {
                btPlay.Image = Properties.Resources.Pause;
                Pic_effects.Visible = true;
            }
            else
            {
                btPlay.Image = Properties.Resources.Play;
                Pic_effects.Visible = false;
            }
        }

        private void btLocal_Click(object sender, EventArgs e)
        {
            Music.ForderDirectory();
            //FolderBrowserDialog folder = new FolderBrowserDialog();
            //if (folder.ShowDialog() == DialogResult.OK)
            //{
            //    _rutasM = Directory.GetFiles(folder.SelectedPath,"*.mp3");
            //}

            if (_rutasM == null)
            {
                vScrollBar1.Visible = true;
                for (int i = 0; i < 1; i++)
                {
                    var items = new Play_Items();
                    items.Dock = DockStyle.Top;
                    items.Tag += "none";
                    items.onAction += play_Items1_onAction;
                    panel5.Controls.Add(items);
                }
            }
        }

        private void alert(string mng)
        {
            MessageBox.Show(mng);
        }

        private void btMute_Hight_Click(object sender, EventArgs e)
        {
            if (bunifuSlider2.Value < 0)
            {
                btMute_Hight.Image = Properties.Resources.mute;
            }
            else if (bunifuSlider2.Value > 1 || bunifuSlider2.Value < 79)
            {
                //btMute_Hight.Image = Properties.Resources
            }

        }
    }
}