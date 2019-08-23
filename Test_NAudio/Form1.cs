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
using NAudio.Wave;

namespace Test_NAudio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BlockAlignReductionStream stream;
        DirectSoundOut soundOut;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "MP3 Archivos (*.mp3)|*.mp3;";
            if (open.ShowDialog() != DialogResult.OK) return;

            TagLib.File file = TagLib.File.Create(open.FileName);
            var mStream = new MemoryStream();
            var firstPicture = file.Tag.Pictures.FirstOrDefault();
            if (firstPicture != null)
            {
                byte[] pData = firstPicture.Data.Data;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                var bm = new Bitmap(mStream, false);
                mStream.Dispose();
                pictureBox1.Image = bm;
            }
            else
            {
                // set "no cover" image
            }

            DisponeWal();

            soundOut = new DirectSoundOut();
            WaveStream wave = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(open.FileName));
            stream = new BlockAlignReductionStream(wave);
            
            soundOut.Init(stream);
            
            soundOut.Play();

            //var time = stream.CurrentTime = TimeSpan.FromSeconds(5.0);
            //for (int i = 0; i < time.TotalMinutes; i++)
            //{
            //    label1.Text = time.ToString();
            //}
        }

        private void DisponeWal()
        {
            if(soundOut != null)
            {
                if (soundOut.PlaybackState == PlaybackState.Playing)
                    soundOut.Stop();
                soundOut.Dispose();
                soundOut = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(soundOut != null)
            {
                if (soundOut.PlaybackState == PlaybackState.Playing)
                    soundOut.Pause();
                else if (soundOut.PlaybackState == PlaybackState.Paused)
                    soundOut.Play();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisponeWal();
        }
    }
}
