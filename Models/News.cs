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

        [DisplayName("Title")]
        [StringLength(70, ErrorMessage = "Title cannot exceed 70 characters.")]
        public string Title { get; set; }


        [DisplayName("Subtitle")]
        [StringLength(70, ErrorMessage = "Subitle cannot exceed 70 characters.")]
        public string Subtitle { get; set; }

        [DisplayName("News Author")]
        [StringLength(70, ErrorMessage = "Authors name cannot exceed 70 characters.")]
        [RegularExpression(@"^([^0-9]*)$$",//regex for names 
            ErrorMessage = "Authors name cannot include any numbers ")]
        public string NewsAuthor { get; set; }

        [DisplayName("Content")]
        [StringLength(1200, ErrorMessage = "Content cannot exceed 1200 characters.")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Last Updated")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }
        public News()
        {
            this.LastModified = DateTime.Now;
        }


        [DisplayName("Picture URL")]
        public string PicUrl { get; set; }

        [NotMapped]
        [DisplayName("Upload your picture")]
        public IFormFile PictureFile { get; set; }

    }
}