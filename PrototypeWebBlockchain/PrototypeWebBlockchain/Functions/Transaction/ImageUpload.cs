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

namespace PrototypeWebBlockchain.Repository
{
    public class ImageUpload 
    {

        public string ConvertSavedFileToSha(string fileSavePath)
        {

            using (SHA256 sha256 = SHA256.Create())
            {
                using (FileStream input = File.Open(fileSavePath, FileMode.Open))
                {
                    var shaValue = BitConverter.ToString(sha256.ComputeHash(input)).Replace("-", "");

                    return shaValue;
                }
            }
        }

        public void FiletoHash(string savepath , string savehash)
        {
            File.Move(savepath, savehash);
        }

    }
}