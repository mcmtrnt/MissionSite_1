using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MissionSite.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [EmailAddress(ErrorMessage = "Please enter your email address")]
        [DisplayName("Email")]
        public string UserEmail { get; set; }

        [StringLength(30, ErrorMessage = "Your First Name must be less than 30 characters")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [StringLength(30, ErrorMessage = "Your Last Name must be less than 30 characters")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }


    }
}