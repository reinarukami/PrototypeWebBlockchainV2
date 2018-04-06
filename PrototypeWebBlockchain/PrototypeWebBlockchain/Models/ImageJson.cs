using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PrototypeWebBlockchain.Models
{
    public class ImageJson
    {
        public int id { get; set; }

        public int fileid { get; set; }

        public string filehash { get; set; }

        public string date { get; set; }
     }
}