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
    public class ArtPiece
    {
        [DisplayName("ID")]
        public int ArtPieceId { get; set; }
        
        [Required]
        [DisplayName("Author")]
        [StringLength(70, ErrorMessage = "Author`s name cannot exceed 70 characters.")]
        [RegularExpression(@"^([^0-9]*)$$",//regex for names 
            ErrorMessage ="Authors name cannot include any numbers")]

        public string Author { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "Title cannot exceed 70 characters.")]
        [DisplayName("Title")]
        public string Info { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }
        public ArtPiece()
        {
            this.ModifiedDate = DateTime.Now;
        }
        
        [DisplayName("Note")]
        [StringLength(250, ErrorMessage ="Note cannot exceed 250 characters.")]
        public string AuthorsNote { get; set; }

        [Required]
        [DisplayName("Type of Art")]
        [StringLength(30, MinimumLength = 3 ,ErrorMessage ="Type of art must be between 3 and 30 characters")]
        public string TypeOfArt { get; set; }

        [DisplayName("Style")]
        [StringLength(40)]
        [RegularExpression(@"^([^0-9]*)$",ErrorMessage = "Style cannot contain numbers")]//regex accepts everything but numbers
        public string Style { get; set; }

        [DisplayName("Is reserved")]
        public bool Reserved { get; set; }

        [DisplayName("Picture URL")]
        public string PicUrl { get; set; }

        [DisplayName("Horizontal")]
        public bool Horizontal { get; set; }

        [NotMapped]
        [DisplayName("Upload picture of your piece of art")]
        //[Required]
        public IFormFile PictureFile { get; set; }

        [Required]
        [DisplayName("Price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}