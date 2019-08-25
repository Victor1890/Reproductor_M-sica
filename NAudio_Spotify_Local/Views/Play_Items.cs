using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAudio_Spotify_Local
{
    public partial class Play_Items : UserControl
    {
        public Play_Items()
        {
            InitializeComponent();
        }

        //Variables
        private bool _playing = false;


        
        /// <summary>
        /// Esto es para poner la Imagen de la Música
        /// </summary>
        public Image Thumbnail
        {
            get
            {
                return thumbnail.Image;
            }
            set
            {
                thumbnail.Image = value;
            }
        }


        /// <summary>
        /// Esto es para saber cuando está en reprodución o no
        /// </summary>
        public bool isPlaying
        {
            get
            {
                return _playing;
            }
            set
            {
                _playing = value;
                if (_playing)
                {
                    actionBtn.Image = Properties.Resources.pause2;
                }
                else
                {
                    actionBtn.Image = Properties.Resources.play2;
                }
            }
        }

        /// <summary>
        /// Esto devuelve el nombre de la música
        /// </summary>
        public string Song
        {
            get
            {
                return lNameSong.Text;
            }
            set
            {
                lNameSong.Text = value;
            }
        }

        /// <summary>
        /// Obtiene el nombre del Artista de la Canción
        /// </summary>
        public string Artist
        {
            get
            {
                return lArtist.Text;
            }
            set
            {
                lArtist.Text = value;
            }
        }

        /// <summary>
        /// Esto obtendrá la Duracción de la Música
        /// </summary>
        public string Duration
        {
            get
            {
                return lDuration.Text;
            }
            set
            {
                lDuration.Text = value;
            }
        }

        public event EventHandler onAction = null;

        private void actionBtn_Click(object sender, EventArgs e)
        {
            isPlaying = !isPlaying;

            if (onAction != null)
            {
                onAction.Invoke(this, e);
            }
        }
    }
}