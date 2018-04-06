using ASPCoreSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PrototypeWebBlockchain.Models
{
    public class Member : BaseEntity
    {
        [Key]
        public int? id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string fname { get; set; }
        [Required]
        public string lname { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public string contact { get; set; }
        [Required]
        public string address { get; set; }

    }
}