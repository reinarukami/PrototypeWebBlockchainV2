using ASPCoreSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PrototypeWebBlockchain.Models
{
    public class TransactionJson
    {
        public int id { get; set; }
        public string filename { get; set; }
        public string status { get; set; }
        public string date { get; set; }
    }
}