using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class News
    {
        [Key]
        [DisplayName("ID")]
        public int NewsId { get; set; }

        [DisplayName("Author")]
        public string NewsAuthor { get; set; }

        [DisplayName("Content")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Last Updated")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }

        [DisplayName("Picture URL")]
        public string PicUrl { get; set; }

        [NotMapped]
        [DisplayName("Picture")]
        public IFormFile PictureFile { get; set; }

    }
}