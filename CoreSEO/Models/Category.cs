using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSEO.Models
{
    public class Category
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Playlist> Playlists { get; set; }

    }
}
