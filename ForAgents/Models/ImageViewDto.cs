using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class ImageView
    {
        [Required]
        [Display(Name ="Upload File")]
        public IFormFile IDDocumentFileAttach { get; set; }
    }
}
