using System.Web.Mvc;
using PrototypeWebBlockchain.Repository;
using PrototypeWebBlockchain.Models;
using System.Web.SessionState;

namespace PrototypeWebBlockchain.Controllers
{
    public class MemberController : Controller
    {
        private readonly MemberRepository memberRepository;
        private readonly MemberFunction memberFunction;

        public MemberController()
        {
            memberRepository = new MemberRepository();
            memberFunction = new MemberFunction();
        }

        public ActionResult Index(Member member)
        {
            var list = memberRepository.FindAll();
            return View(list);
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
                return RedirectToAction("Login");
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {

            if (ModelState.IsValid)
            {
                var member = memberRepository.Login(login.email, login.password);
                if(member != null)
                {
                    var test = memberFunction.CovertToSha(member.lname + member.fname);
                    Session["Hash"] = test;
                    Session["ID"] = member.id;

                    return RedirectToRoute(new { Controller = "Transaction", Action = "Transactionlist" });
                }

                ModelState.AddModelError("LoginError", "Username and Password do not match , Please try Again");
            }

            return View(login);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToRoute(new { Controller = "Member", Action = "Login" });
        }

    }
}