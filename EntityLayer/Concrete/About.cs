using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }
        public string ImageA { get; set; }
        public string TitleA { get; set; }
        public string DescriptionA { get; set; }
        public string ImageB { get; set; }
        public string TitleB { get; set; }
        public string DescriptionB { get; set; }
        public bool Status { get; set; }
    }
}
