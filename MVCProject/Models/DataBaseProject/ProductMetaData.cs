using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models.DataBaseProject
{
    [MetadataType(typeof(ProductMetaData))]
     public partial class Product
    {
    }

    internal class ProductMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public System.DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        
        public decimal Price { get; set; }
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Required]
        [MaxLength(4000)]
        [DataType(DataType.MultilineText)]
        public string LogDescription { get; set; }
    }
}