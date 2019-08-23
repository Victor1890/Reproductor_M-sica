using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NAudio.Wave;
using System.Windows.Forms;
using System.Drawing;

namespace NAudio_Spotify_Local.Classes
{
    public class Music
    {
        //Variables
        private Classes.Datos datos = new Classes.Datos();
        private FolderBrowserDialog folder;

        private string[] _ArchivoMp3;
        private string[] _rutasArchivoMp3;
        private string[] _rutas;
        

        public void Thumbnail()
        {
            TagLib.File file = TagLib.File.Create("_rutas");
            var mStream = new MemoryStream();
            var firstPicture = file.Tag.Pictures.FirstOrDefault();

            if (firstPicture != null)
            {
                byte[] pData = firstPicture.Data.Data;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                var bm = new Bitmap(mStream, false);
                mStream.Dispose();
            }
            else
            {
                var imagen = Properties.Resources.default_1;
            }
        }

        public string[] ForderDirectory()
        {
            folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                _ArchivoMp3 = Directory.GetFiles(folder.SelectedPath, "*.mp3");

                foreach (var Names in _ArchivoMp3)
                {
                    Play_Items items = new Play_Items();
                    TagLib.File file = TagLib.File.Create(Names);
                    var title = file.Tag.Title;
                    var artist = file.Tag.FirstAlbumArtist;
                    var album = file.Tag.Album;
                    var duration = file.Properties.Duration;

                    datos.Title = new string[] { title };
                    datos.Artist = new string[] { artist };
                    datos.Album = new string[] { album };
                    datos.Duration = new TimeSpan[] { duration };

                    items.Song = string.Join("", datos.Title);
                    items.Artist = string.Join("",datos.Artist);
                    items.Duration = datos.Duration.ToString();
                }
            }
            return _rutasArchivoMp3;
        }
    }
}