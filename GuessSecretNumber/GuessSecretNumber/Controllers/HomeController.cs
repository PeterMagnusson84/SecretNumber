using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuessSecretNumber.Models;

namespace GuessSecretNumber.Controllers
{
    public class HomeController : Controller
    {
        private SecretNumber SN
        {
            get
            {
                return Session["game_session"] as SecretNumber ?? (SecretNumber)(Session["game_session"] = new SecretNumber());
            }
        }

        public ActionResult Index()
        {
            SN.Initialize();

            return View("Index", SN);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formdata)
        {
            if (TryUpdateModel(SN, new[] { "Guess" }, formdata))
            {
                var model = new Models.SecretNumberMessages
                {
                    Outcome = SN.MakeGuess(),
                    SecretNumber = SN,
                };
                if (SN.CanMakeGuessCheck)
                {
                    return View("Guessed", model);
                }
                else
                {
                    if (model.Outcome == Outcome.Right)
                    {
                        return View("RightGuess", model);
                    }

                    return View("WrongGuess", model);
                }
            }
            return View();
        }

       
    }
}