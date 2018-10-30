using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSEO.Models
{
    public class Playlist
    {

        public int PlaylistID { get; set; }
        public int CategoryID { get; set; }
        public int ContentID { get; set; }


        public Category Category { get; set; }
        public Content Content { get; set; }
    }
}
