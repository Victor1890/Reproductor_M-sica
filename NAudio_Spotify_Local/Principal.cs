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
using NAudio.Wave.SampleProviders;


namespace NAudio_Spotify_Local
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            MemoryManager.MemoryManager.ReleaseMemory();
        }
        private Play_Items items = new Play_Items();


        //Variables
        private static Random rnd = new Random();
        private IWavePlayer _waveOutDevice;
        private AudioFileReader _audioFileReader;
        private List<string> _songFiles = new List<string>();
        private int _songIndex;
        private Action<float> _setVolumeDelegate;
        private string _lastPath;
        //private const string _supportedExtentions = "*.wav;*.aiff;*.mp3;*.aac";


        //Cosas
        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            System.Diagnostics.Process.Start("https://github.com/Victor1890/Reproductor_M-sica");
        }

        public void play_Items1_onAction(object sender, EventArgs e)
        {
            //Cambiar el botón del Play a Pause
            //play_Items1.isPlaying = false;
            //play_Items2.isPlaying = false;
            //play_Items3.isPlaying = false;

            ((Play_Items)sender).isPlaying = true;
            var song = (string)((Play_Items)sender).Tag;

            //Play Song
            thumbnail.Image = ((Play_Items)sender).Thumbnail;
            thumbnail2.Image = ((Play_Items)sender).Thumbnail;
            
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

        private void btLocal_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = false;

            try
            {
                folderBrowser.SelectedPath = _lastPath;
            }
            catch (ArgumentNullException ex)
            {
                ex.ToString();
                folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            }

            if (folderBrowser.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                var paths = Directory.GetFiles(folderBrowser.SelectedPath, "*.mp3", SearchOption.AllDirectories);

                foreach (string path in paths)
                {
                    if (!_songFiles.Contains(path))
                    {
                        AddSongsToLists(path);
                    }
                }
            }
        }

        private void AddSongsToLists(string song)
        {
            string name;
            TagLib.File file;

            file = TagLib.File.Create(song);

            try
            {
                name = string.Format("{0} - {1}", file.Tag.Performers[0], file.Tag.Title);
            }
            catch (IndexOutOfRangeException ex)
            {
                ex.ToString();
                name = Path.GetFileNameWithoutExtension(song);
            }

            _songFiles.Add(song);

            ListSong.Items.Add(name);
        }

        private void btPlay_Click(object sender, EventArgs e)
        {
            if (!_songFiles.Any())
            {
                btLocal_Click(sender, e);
                return;
            }

            if (_waveOutDevice != null)
            {
                if (_waveOutDevice.PlaybackState == PlaybackState.Playing)
                {
                    _waveOutDevice.Pause();
                    btPlay.Image = Properties.Resources.Play;
                    Pic_effects.Visible = false;
                    return;
                }
                else if (_waveOutDevice.PlaybackState == PlaybackState.Paused)
                {
                    _waveOutDevice.Play();
                    btPlay.Image = Properties.Resources.Pause;
                    Pic_effects.Visible = true;
                    return;
                }

                _songIndex = ListSong.SelectedIndex;

                PlaySong(_songIndex);
            }
        }
        //Cosas

        private void PlaySong(int songIndex)
        {
            //Create Wave
            _waveOutDevice = new WaveOut();
            ISampleProvider sampleProvider;

            try
            {
                sampleProvider = CreateInputStream(_songFiles.ElementAt(songIndex));
            }
            catch (Exception ex)
            {
                ex.ToString();
                sampleProvider = CreateInputStream(_songFiles.First());
                ListSong.SelectedIndex = 0;
            }

            //Length of the song
            l_time_final.Text = string.Format("{0:00}:{1:00}", (int)_audioFileReader.TotalTime.TotalMinutes, _audioFileReader.TotalTime.Seconds);

            //Set name and description
            string path;

            try
            {
                path = _songFiles.ElementAt(songIndex);
            }
            catch (Exception ex)
            {
                ex.ToString();
                path = _songFiles.First();
            }

            TagLib.File file = TagLib.File.Create(path);

            l_Artist_1.Text = file.Tag.FirstAlbumArtist;
            l_Song_2.Text = file.Tag.Title;
            l_Song_1.Text = file.Tag.Title;
            l_Song2.Text = file.Tag.Title;
            l_Album2.Text = file.Tag.FirstAlbumArtist;
            l_Album.Text = file.Tag.FirstAlbumArtist;

            lBtrate.Text = file.Properties.AudioBitrate + "kbps";
            lSamplerate.Text = file.Properties.AudioSampleRate + "kHz";
            lFormat.Text = Path.GetExtension(path).ToUpper();
            
            items.Artist = file.Tag.FirstAlbumArtist;
            items.Song = file.Tag.Title;

            
            //Set artwork
            TagLib.IPicture pic;
            MemoryStream stream;
            Bitmap image;

            if (file.Tag.Pictures.Length > 0)
            {
                pic = file.Tag.Pictures[0];

                stream = new MemoryStream(pic.Data.Data);
                image = new Bitmap(stream);
                //new Bitmap(stream);

                thumbnail.Image = image;
                thumbnail2.Image = image;
                items.Thumbnail = image;
            }
            else
            {
                thumbnail.Image = Properties.Resources.default_1;
                thumbnail2.Image = Properties.Resources.default_1;
            }


            if (l_Song_1.Text == "" || l_Artist_1.Text == "" || l_Song_2.Text == "" || l_Album.Text == "")
            {
                l_Album.Text = "Desconocido";
                l_Artist_1.Text = "Desconocido";
                l_Song_2.Text = "Desconocido";
                l_Song_1.Text = "Desconocido";
            }
            //ListSong.Controls.Add(items);

            //Visible Date
            l_Album.Visible = true;
            l_Album2.Visible = true;
            lBtrate.Visible = true;
            lSamplerate.Visible = true;
            lFormat.Visible = true;
            l_Artist_1.Visible = true;
            l_Song_1.Visible = true;
            l_Song_2.Visible = true;
            l_Song2.Visible = true;

            //Play song :P
            _waveOutDevice.Init(sampleProvider);
            _setVolumeDelegate(bunifuSlider2.Value);
            _waveOutDevice.Play();
        }

        private ISampleProvider CreateInputStream(string fileName)
        {
            try
            {
                _audioFileReader = new AudioFileReader(fileName);
            }
            catch (FileNotFoundException ex)
            {
                ex.ToString();
                _audioFileReader = new AudioFileReader(_songFiles.First());
            }

            var sampleChannel = new SampleChannel(_audioFileReader, true);
            _setVolumeDelegate = vol => sampleChannel.Volume = vol;
            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);

            return postVolumeMeter;
        }
                
        private void ListSong_DoubleClick(object sender, MouseEventArgs e)
        {
            if (!_songFiles.Any())
            {
                Pic_effects.Visible = false;
            }
            else
            {
                if (Pic_effects.Visible == false)
                {
                    btPlay.Image = Properties.Resources.Play;
                    Pic_effects.Visible = true;
                }
                else
                {
                    //btPlay.Image = Properties.Resources.Pause;
                    Pic_effects.Visible = true;
                }

                int index = ListSong.IndexFromPoint(e.Location);
                _songIndex = ListSong.SelectedIndex;
                

                if (index != ListBox.NoMatches)
                {
                    //lblTitle.Text = ListSong.SelectedItem.ToString();

                    if (_waveOutDevice == null)
                    {
                        PlaySong(_songIndex);
                    }
                    else if (_waveOutDevice != null)
                    {
                        _waveOutDevice.Stop();
                        PlaySong(_songIndex);
                    }
                }
            }
        }

        private void bthuffle_Click(object sender, EventArgs e)
        {
            if (true)
            {
                int rndIndex = rnd.Next(0, ListSong.Items.Count);

                if (rndIndex == ListSong.SelectedIndex)
                {
                    rndIndex = rnd.Next(0, ListSong.Items.Count);
                }

                ListSong.SelectedIndex = rndIndex;
                PlaySong(rndIndex);
            }

            //if (_waveOutDevice != null)
            //{
            //    if (_waveOutDevice.PlaybackState == PlaybackState.Playing)
            //    {
            //        if (Pic_effects.Visible == true)
            //        {
            //            btPlay.Image = Properties.Resources.Play;
            //            Pic_effects.Visible = false;
            //        }
            //        _waveOutDevice.Stop();
            //        //ClearAll();
            //        ListSong.SelectedIndex = -1;
            //    }
            //}
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            PrevNextSong('+');
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            PrevNextSong('-');
        }

        private void PrevNextSong(char sym)
        {
            if (_waveOutDevice == null)
            {
                return;
            }

            _waveOutDevice.Stop();


            //Shuffle
            Shuffle();

            //Change songs
            int songIndex;

            if (sym == '+')
            {
                songIndex = ListSong.SelectedIndex + 1;
            }
            else if (sym == '-')
            {
                songIndex = ListSong.SelectedIndex - 1;
            }
            else return;

            try
            {
                ListSong.SelectedIndex = songIndex;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ex.ToString();
                if (sym == '+')
                {

                    PlaySong(_songFiles.IndexOf("0"));
                    ListSong.SelectedIndex = 0;
                }
                else if (sym == '-')
                {
                    PlaySong(_songFiles.Count - 1);
                    ListSong.SelectedIndex = ListSong.Items.Count - 1;
                }
                return;
            }
            PlaySong(songIndex);
        }

        //Shuffle
        private void Shuffle()
        {
            if (cShuffle.Checked == true)
            {
                int rndIndex = rnd.Next(0, ListSong.Items.Count + 1);

                if (rndIndex == ListSong.SelectedIndex)
                {
                    rndIndex = rnd.Next(0, ListSong.Items.Count);
                }

                ListSong.SelectedIndex = rndIndex;
                PlaySong(rndIndex);
            }
        }

        //Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_waveOutDevice != null && _audioFileReader != null)
            {
                TimeSpan currentTime = (_waveOutDevice.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : _audioFileReader.CurrentTime;
                bunifuSlider1.Value = Math.Min(bunifuSlider1.MaximumValue, (int)(100 * currentTime.TotalSeconds / _audioFileReader.TotalTime.TotalSeconds));
                l_time_inial.Text = string.Format("{0:00}:{1:00}", (int)currentTime.TotalMinutes, currentTime.Seconds);

                if (_waveOutDevice.PlaybackState == PlaybackState.Stopped || _waveOutDevice.PlaybackState == PlaybackState.Paused)
                {
                    if (Pic_effects.Visible == true)
                    {
                        Pic_effects.Visible = false;
                    }
                }

                if (_waveOutDevice.PlaybackState == PlaybackState.Stopped)
                {
                    if(panel6.AutoSize == true)
                        panel6.AutoSize = false;
                    else
                        panel6.AutoSize = true;
                }

                if (bunifuSlider1.Value == bunifuSlider1.MaximumValue)
                {
                    PrevNextSong('+');
                }

                panel5.AutoSize = true;
                panel6.AutoSize = true;
            }
            else
            {
                bunifuSlider1.Value = 0;
            }
        }
        
        private void CloseWaveOut()
        {
            if (_waveOutDevice != null)
            {
                _waveOutDevice.Stop();
            }
            if (_waveOutDevice != null)
            {
                _waveOutDevice.Dispose();
                _waveOutDevice = null;
            }
        }

        private void ClearAll()
        {
            l_Album.Visible = false;
            l_Artist_1.Visible = false;
            l_Song_2.Visible = false;
            l_Song_1.Visible = false;
            l_Song2.Visible = false;
            l_Album2.Visible = false;
            lSamplerate.Visible = false;
            lBtrate.Visible = false;
            lFormat.Visible = false;


            l_time_final.Text = "0:00";
            l_time_inial.Text = "0:00";

            thumbnail.Image = null;
            thumbnail2.Image = null;
        }

        private void bunifuSlider2_ValueChanged(object sender, EventArgs e)
        {
            if (_setVolumeDelegate != null)
            {
                _setVolumeDelegate(bunifuSlider2.Value);
            }
        }

        private void bunifuSlider1_ValueChanged(object sender, EventArgs e)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.CurrentTime = TimeSpan.FromSeconds(_audioFileReader.TotalTime.TotalSeconds * bunifuSlider1.Value / 100.0);
            }
        }

        private void txtSeach_TextChanged(object sender, EventArgs e)
        {
            //var registrationsList = ListSong.Items.Cast<String>().ToList();
            //ListSong.BeginUpdate();
            //ListSong.Items.Clear();
            //foreach (string str in registrationsList)
            //{
            //    if (str.Contains(txtSeach.Text))
            //    {
            //        ListSong.Items.Add(str);
            //    }
            //}
            //ListSong.EndUpdate();
        }
    }
}