using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudio_Spotify_Local.Classes
{
    class Datos
    {
        public string[] Title { get; set; }

        public string[] Artist { get; set; }

        public string[] Album { get; set; }

        public TimeSpan[] Duration { get; set; }
    }
}
