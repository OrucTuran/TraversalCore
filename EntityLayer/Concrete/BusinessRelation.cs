using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class BusinessRelation//hoca about2 olarak aldi
    {
        [Key]
        public int BusinessRelationID { get; set; }
        public string Image { get; set; }
        public string TitleA { get; set; }
        public string TitleB { get; set; }
        public string Description { get; set; }
    }
}
