using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class AuthController : Controller
    {
        IKorisnikProvider korisnikProvider = KorisnikStrategy.GetStrategy();
        ILekarProvider lekarProvider;
        IPacijentProvider pacijentProvider;
        ILogger logger;

        public AuthController(ILekarProvider lekarProvider, IPacijentProvider pacijentProvider, ILogger logger)
        {
            this.lekarProvider = lekarProvider;
            this.pacijentProvider = pacijentProvider;
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult Login(Korisnik k)
        {
            var list = korisnikProvider.SviKorisnici();
            var user = list.Find(t => t.KorisnickoIme == k.KorisnickoIme && t.Lozinka == k.Lozinka);
            if (user == null)
            {
                logger.LogWarn($"Pogresno logovanje sa {k.KorisnickoIme}");
                return RedirectToAction("Index", "Home");
            }
            Session["user"] = user;
            var u = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {u.KorisnickoIme} se ulogovao");
            if (user.Tip == Tip.Administrator)
                return RedirectToAction("Index", "Admin");
            else if (user.Tip == Tip.Lekar)
                return RedirectToAction("Index", "Lekar");
            else
                return RedirectToAction("Index", "Pacijent");
        }

        public ActionResult Logout()
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} se odjavio");
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Registracija(Korisnik k, string jmbg, Specijalizacija specijalizacija = Specijalizacija.Hirurg, Titula titula = Titula.Dr, int bolnicaId = -1)
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} registruje korisnika sa {k.KorisnickoIme}");
            switch (k.Tip)
            {
                case Tip.Lekar:
                    lekarProvider.Dodaj(new Lekar(-1, k.Ime, k.KorisnickoIme, k.Lozinka, k.Prezime, k.Tip, Odeljenje.Hirurgija, specijalizacija, titula, new Bolnica { Id = bolnicaId }));
                    break;
                case Tip.Pacijent:
                    pacijentProvider.Dodaj(new Pacijent(-1, k.Ime, k.KorisnickoIme, k.Lozinka, k.Prezime, k.Tip, jmbg, null));
                    break;
                case Tip.Administrator:
                    korisnikProvider.DodajKorisnika(k);
                    break;
                default:
                    break;
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}