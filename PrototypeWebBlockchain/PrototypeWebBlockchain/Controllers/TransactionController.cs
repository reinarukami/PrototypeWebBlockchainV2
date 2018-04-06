using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Npgsql;
using PrototypeWebBlockchain.Repository;
using PrototypeWebBlockchain.Models;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;
using Newtonsoft.Json;
using PrototypeWebBlockchain.Functions.Filters;
using System.Diagnostics;

namespace PrototypeWebBlockchain.Controllers
{
    [AuthorizeUser]
    public class TransactionController : Controller
    {
        private readonly TransactionRepository transactionRepository;
        private readonly ImageUpload imageupload;
       
        
        public TransactionController()
        {
            transactionRepository = new TransactionRepository();
            imageupload = new ImageUpload();
        }

        // GET: Home

        public ActionResult UploadImage()
        {

            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(Image image)
        {
            if (HttpContext.Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("image", "Image is null , Please set Image");
                return View();
            }

            image.image = HttpContext.Request.Files[0];



            if (image.image != null)
            {
                // Validate the uploaded image(optional)
                var imageId = transactionRepository.nextid().FirstOrDefault().ToString();
                var imagepath = ConfigurationManager.AppSettings["FileImagePath"];
                var imageName = imageId + '_' + Session["ID"].ToString() + '_' + image.image.FileName.ToString();
               
                // Get the complete file path
                var fileSavePath =  Path.Combine(imagepath + imageName);

                // Save the uploaded file to "UploadedFiles" folder
                image.image.SaveAs(fileSavePath);
                image.hash = imageupload.ConvertSavedFileToSha(fileSavePath);
              
                var transaction = new Transaction() {
                id = Int32.Parse(imageId),
                member_id = Int32.Parse(Session["ID"].ToString()),
                filename = imageName,
                filepath = imagepath,
                date = DateTime.Now.ToString()
                };

                transactionRepository.Add(transaction);

                HttpContext.Response.Clear();
                
                //Declare needed resources 
                string script = "<script src='/node_modules/web3/dist/web3.min.js'></script><script src='/Scripts/WebMainConfig.js'></script>" +

                //Declare the Account
                "<script>web3.eth.defaultAccount = web3.eth.accounts[0];" +

                //Declare the Contract Abi
                "var ContractAbi = web3.eth.contract([{'constant':false,'inputs':[{'name':'_id','type':'uint256'},{'name':'_fileid','type':'uint256'},{'name':'_fileHash','type':'string'},{'name':'_date','type':'string'}],'name':'AddFiles','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'anonymous':false,'inputs':[{'indexed':false,'name':'id','type':'uint256'},{'indexed':false,'name':'fileid','type':'uint256'},{'indexed':false,'name':'fileHash','type':'string'},{'indexed':false,'name':'date','type':'string'}],'name':'FileUploadEvent','type':'event'}]);" +

                //Declare the contract Address
                "var ImageContract = ContractAbi.at('0x538882ec49974f8815fee55ad7b40d6dd4b6b75d'); var fileid = web3.eth.blockNumber + 1;" +

                //Declare the transaction
                "ImageContract.AddFiles(" +Session["ID"] + "," + imageId + ",'" + image.hash + "','" + DateTime.Now + "', { gas: 999999 });" +

                "window.location.href = '/Transaction/Transactionlist'; </script>";

                return Content(script);
            }
            return View();

         }

        public ActionResult Transactionlist(Identifier identifier)
        {
            identifier.hash = Session["Hash"].ToString();
            identifier.id = Session["ID"].ToString();
            return View(identifier);
        }

        [HttpPost]
        public JsonResult ValidateImages(string data)
        {
            var _imageFiles = JsonConvert.DeserializeObject<List<ImageJson>>(data);
            var _imageSaved = transactionRepository.FindByID(Int32.Parse(Session["ID"].ToString()));
            var transaction = new List<TransactionJson>();

            var nfilepath = ConfigurationManager.AppSettings["FileImagePath"];
         
            string file;

            foreach (var item  in _imageFiles)
            {
    
                var image = _imageSaved.Where(r => r.id == item.fileid).FirstOrDefault();

                try
                {
                    file = Path.Combine(nfilepath, image.filename);
                    string shavalue = imageupload.ConvertSavedFileToSha(file);
                    if (shavalue == item.filehash)
                    {
                        item.filehash = image.filename;
                        transaction.Add(new TransactionJson()
                        {
                            id = item.fileid,
                            filename = image.filename,
                            status = "Status/check.png",
                            date = item.date
                        });
                    }
                    else
                    {
                        transaction.Add(new TransactionJson()
                        {
                            id = item.fileid,
                            filename = image.filename,
                            status = "Status/cross.png",
                            date = item.date
                        });

                    }
                }

                catch
                {
                    transaction.Add(new TransactionJson()
                    {
                        id = item.fileid,
                        filename = image.filename,
                        status = "Status/cross.png",
                        date = item.date
                    });
                }       
            }

            return new JsonResult() { Data = new { JTransaction = transaction } };
        }

    }
}