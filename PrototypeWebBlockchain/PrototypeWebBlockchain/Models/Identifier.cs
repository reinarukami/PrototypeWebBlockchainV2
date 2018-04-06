using ASPCoreSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PrototypeWebBlockchain.Models
{
    public class Identifier 
    {
        public string id { get; set; }
        public string hash { get; set; }
    }
}