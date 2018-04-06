using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PrototypeWebBlockchain.Repository;
using PrototypeWebBlockchain.Models;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;

namespace PrototypeWebBlockchain.Controllers
{
    public class HomeController : Controller
    {
        private readonly MemberRepository memberRepository;

        public HomeController()
        {
            memberRepository = new MemberRepository();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration(Member member)
        {
            var list = memberRepository.FindAll();
            return View(list);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                memberRepository.Add(member);
                return RedirectToAction("Registration");
            }
            return View(member);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var obj = memberRepository.FindByID(id.Value);

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                memberRepository.Update(member);
                return RedirectToAction("Registration");
            }
            return View(member);
        }

        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                memberRepository.Remove(id);
                return RedirectToAction("Registration");
            }
            return RedirectToAction("Registration");
        }


    }
}