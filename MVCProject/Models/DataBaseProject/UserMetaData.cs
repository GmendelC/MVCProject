using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models.DataBaseProject
{
    [MetadataType(typeof(UserMetaData))]
     public partial class User
    {
    }

    internal class UserMetaData
    {
        [Required]
        public int Id;
        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}