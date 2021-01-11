﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class ArtPiece
    {
        [DisplayName("ID")]
        public int ArtPieceId { get; set; }

        [DisplayName("Author")]
        public string Author { get; set; }

        [DisplayName("Information")]
        public string Info { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Last Updated")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }

        [DisplayName("Note")]
        public string AuthorsNote { get; set; }

        [DisplayName("Type of Art")]
        public string TypeOfArt { get; set; }

        [DisplayName("Style")]
        public string Style { get; set; }

        [DisplayName("Is reserved")]
        public bool Reserved { get; set; }

        [DisplayName("Picture URL")]
        public string PicUrl { get; set; }

        public bool Horizontal { get; set; }

        [NotMapped]
        [DisplayName("Upload picture of your piece of art")]
        public IFormFile PictureFile { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }
    }
}