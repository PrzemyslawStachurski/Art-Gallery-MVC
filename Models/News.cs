using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class News
    {

        [DisplayName("ID")]
        public int NewsId { get; set; }

        [DisplayName("Author")]
        public string NewsAuthor { get; set; }

        [DisplayName("Author")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Last Updated")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }


    }
}
