using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSEO.Models
{
    /// <summary>
    /// Constructor class example
    /// https://www.youtube.com/watch?v=3zoHLx3MNyA&index=2&list=PLwbi_A3jiptdPOdADYa02v89Fb928ix-y
    /// </summary>
    public class Content
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Desciption { get; set; }
        public string Keywords { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

    }
}
