using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class PacijentController : Controller
    {
        ILogger logger;
        IPacijentProvider pacijentProvider;
        ITerminProvider terminProvider;
        IPregledProvider pregledProvider;
        IObavestenjeProvider obavestenjeProvider = ObavestenjeSingleton.GetObavestenje();
        Subject subject = SubjectSingleton.GetSubject();

        public PacijentController(ILogger logger, IPacijentProvider pacijentProvider, ITerminProvider terminProvider, IPregledProvider pregledProvider)
        {
            this.logger = logger;
            this.pacijentProvider = pacijentProvider;
            this.terminProvider = terminProvider;
            this.pregledProvider = pregledProvider;
        }


        // GET: Pacijent
        public ActionResult Index()
        {
            var k = (Korisnik)Session["user"];
            var p = pacijentProvider.Dobavi(k.Id);
            ViewBag.Korisnik = p;
            ViewBag.Lekar = p.Lekar;
            ViewBag.MogucePrijaviti = p.Lekar != null && terminProvider.Svi().Find(t => t.Pacijent.Id == k.Id && t.m_StatusCekanja is Ceka && t.m_StatusCekanja is UToku ) == null;
            ViewBag.Pregledi = terminProvider.Svi().FindAll(t => t.Pacijent.Id == k.Id);
            if (subject.m_Observer.Find(t => t.m_Korisnik.Id == k.Id) != null)
                ViewBag.Obavestenja = obavestenjeProvider.Obavestenja(k.Id);
            return View();
        }

        public ActionResult UkljuciObavestenja()
        {
            var k = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {k.KorisnickoIme} se registruje na obavestenja");
            subject.Register(new ConcreteObserver(obavestenjeProvider, (ConcreteSubject)subject, k));
            return RedirectToAction("Index");
        }

        public ActionResult IskljuciObavestenja()
        {
            var k = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {k.KorisnickoIme} se odjavljuje sa obavestenja");
            subject.Unregister(subject.m_Observer.Find(t => t.m_Korisnik.Id == k.Id));
            return RedirectToAction("Index");
        }

        public ActionResult Procitano(int id)
        {
            var k = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {k.KorisnickoIme} je oznacio da je procitao obavestenje sa id = {id}");
            obavestenjeProvider.Procitano(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Zakazi(string datum, string vreme)
        {
            var k = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {k.KorisnickoIme} zakazuje pregled");
            var p = pacijentProvider.Dobavi(k.Id);
            pregledProvider.ZakaziPregled(new Termin() { DatumIVreme = DateTime.Parse($"{datum} {vreme}"), Pacijent = p, Bolnica = p.Lekar.m_Bolnica, Lekar = p.Lekar, Trajanje = 15 });
            return RedirectToAction("Index");
        }
    }
}