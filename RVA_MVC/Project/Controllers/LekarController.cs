using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class LekarController : Controller
    {
        ILogger logger;
        IPregledProvider pregledProvider;
        IObavestenjeProvider obavestenjeProvider = ObavestenjeSingleton.GetObavestenje();
        Subject subject = SubjectSingleton.GetSubject();

        public LekarController(ILogger logger, IPregledProvider pregledProvider)
        {
            this.logger = logger;
            this.pregledProvider = pregledProvider;
        }

        // GET: Lekar
        public ActionResult Index()
        {
            if(Session["user"] == null)
            {
                logger.LogWarn($"Korisnik je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Index", "Home");
            }
            var k = (Korisnik)Session["user"];
            if (k.Tip != Tip.Lekar)
            {
                logger.LogWarn($"Korisnik {k.KorisnickoIme} je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Error", "Home");
            }
            ViewBag.ImaUToku = pregledProvider.SviPregledi(k.Id).FindAll(t => t.m_StatusCekanja is UToku) == null;
            ViewBag.Pregledi = pregledProvider.SviPregledi(k.Id).FindAll(t => t.m_StatusCekanja is Ceka || t.m_StatusCekanja is UToku);
            if(subject.m_Observer.Find(t => t.m_Korisnik.Id == k.Id) != null)
                ViewBag.Obavestenja = obavestenjeProvider.Obavestenja(k.Id);
            return View();
        }

        public ActionResult Potvrdi(int id)
        {
            if (Session["user"] == null)
            {
                logger.LogWarn($"Korisnik je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Index", "Home");
            }
            var k = (Korisnik)Session["user"];
            if (k.Tip != Tip.Lekar)
            {
                logger.LogWarn($"Korisnik {k.KorisnickoIme} je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Error", "Home");
            }
            logger.LogInfo($"Korisnik {k.KorisnickoIme} potvrdjuje pregled sa id = {id}");
            pregledProvider.ZapocniPregled(id);
            return RedirectToAction("Index");
        }
        public ActionResult Odbij(int id)
        {
            if (Session["user"] == null)
            {
                logger.LogWarn($"Korisnik je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Index", "Home");
            }
            var k = (Korisnik)Session["user"];
            if (k.Tip != Tip.Lekar)
            {
                logger.LogWarn($"Korisnik {k.KorisnickoIme} je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Error", "Home");
            }
            logger.LogInfo($"Korisnik {k.KorisnickoIme} odbija pregled sa id = {id}");
            pregledProvider.OdbijPregled(id);
            return RedirectToAction("Index");
        }

        public ActionResult Zavrsi(int id)
        {
            if (Session["user"] == null)
            {
                logger.LogWarn($"Korisnik je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Index", "Home");
            }
            var k = (Korisnik)Session["user"];
            if (k.Tip != Tip.Lekar)
            {
                logger.LogWarn($"Korisnik {k.KorisnickoIme} je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Error", "Home");
            }
            logger.LogInfo($"Korisnik {k.KorisnickoIme} zavrsava pregled sa id = {id}");
            pregledProvider.ZavrsiPregled(id);
            return RedirectToAction("Index");
        }

        public ActionResult PrethodniPregledi()
        {
            if (Session["user"] == null)
            {
                logger.LogWarn($"Korisnik je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Index", "Home");
            }
            var k = (Korisnik)Session["user"];
            if (k.Tip != Tip.Lekar)
            {
                logger.LogWarn($"Korisnik {k.KorisnickoIme} je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Error", "Home");
            }
            ViewBag.Pregledi = pregledProvider.SviPregledi(k.Id).FindAll(t => t.m_StatusCekanja is Odbijen || t.m_StatusCekanja is Pregledan); ;
            return View();
        }

        public ActionResult UkljuciObavestenja()
        {
            var k = (Korisnik)Session["user"];
            if (k.Tip != Tip.Lekar)
            {
                logger.LogWarn($"Korisnik {k.KorisnickoIme} je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Index", "Home");
            }
            logger.LogInfo($"Korisnik {k.KorisnickoIme} se registruje na obavestenja");
            subject.Register(new ConcreteObserver(obavestenjeProvider, (ConcreteSubject)subject, k));
            return RedirectToAction("Index");
        }

        public ActionResult IskljuciObavestenja()
        {
            var k = (Korisnik)Session["user"];
            if (k.Tip != Tip.Lekar)
            {
                logger.LogWarn($"Korisnik {k.KorisnickoIme} je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Index", "Home");
            }
            logger.LogInfo($"Korisnik {k.KorisnickoIme} se odjavljuje sa obavestenja");
            subject.Unregister(subject.m_Observer.Find(t => t.m_Korisnik.Id == k.Id));
            return RedirectToAction("Index");
        }

        public ActionResult Procitano(int id)
        {
            var k = (Korisnik)Session["user"];
            if (k.Tip != Tip.Lekar)
            {
                logger.LogWarn($"Korisnik {k.KorisnickoIme} je pokusao da udje na stranicu sa lekar pravima");
                return RedirectToAction("Index", "Home");
            }
            logger.LogInfo($"Korisnik {k.KorisnickoIme} je oznacio da je procitao obavestenje sa id = {id}");
            obavestenjeProvider.Procitano(id);
            return RedirectToAction("Index");
        }
    }
}