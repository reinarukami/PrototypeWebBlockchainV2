using ASPCoreSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PrototypeWebBlockchain.Models
{
    public class Transaction : BaseEntity
    {
        [Key]
        public int? id { get; set; }
        [Required]
        public int? member_id { get; set; }
        [Required]
        public string filename { get; set; }
        [Required]
        public string filepath { get; set; }
        [Required]
        public string date { get; set; }
    }
}