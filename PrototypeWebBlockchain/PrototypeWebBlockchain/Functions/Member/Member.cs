using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using Npgsql;
using PrototypeWebBlockchain.Models;
using System.IO;
using System.Security.Cryptography;
using System;
using System.Web;
using System.Text;

namespace PrototypeWebBlockchain.Repository
{
    public class MemberFunction 
    {
        public string CovertToSha(string value)
        {
            var sha = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = sha.ComputeHash(Encoding.ASCII.GetBytes(value));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

    }
}