using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        ILogger logger;
        IPacijentProvider pacijentProvider;
        ILekarProvider lekarProvider;
        IBolnicaProvider bolnicaProvider;
        IKorisnikProvider korisnikProvider = KorisnikStrategy.GetStrategy();
        IDodelaProvider dodelaProvider = DodelaProviderSingleton.GetDodelaProvider();
        Subject subject = SubjectSingleton.GetSubject();

        public AdminController(ILogger logger, IPacijentProvider pacijentProvider, ILekarProvider lekarProvider, IBolnicaProvider bolnicaProvider)
        {
            this.logger = logger;
            this.pacijentProvider = pacijentProvider;
            this.lekarProvider = lekarProvider;
            this.bolnicaProvider = bolnicaProvider;
        }

        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Korisnici = korisnikProvider.SviKorisnici().FindAll(t => t.KorisnickoIme != "admin");
            ViewBag.Bolnice = bolnicaProvider.SveBolnice();
            return View();
        }

        public ActionResult Korisnik(int id, Tip tip)
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je pristupio detaljima korisnika sa id = {id}");
            switch (tip)
            {
                case Tip.Lekar:
                    ViewBag.Korisnik = lekarProvider.Dobavi(id);
                    break;
                case Tip.Pacijent:
                    ViewBag.Korisnik = pacijentProvider.Dobavi(id);
                    break;
                case Tip.Administrator:
                    ViewBag.Korisnik = korisnikProvider.DobaviKorisnika(id);
                    break;
                default:
                    break;
            }
            return View();
        }

        [HttpPost]
        public ActionResult IzmeniKorisnika(Korisnik k)
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je izmenio korisnika sa id = {k.Id}");
            var kor = korisnikProvider.DobaviKorisnika(k.Id);
            kor.Ime = k.Ime;
            kor.Prezime = k.Prezime;
            kor.KorisnickoIme = k.KorisnickoIme;
            kor.Lozinka = k.Lozinka;
            korisnikProvider.IzmeniKorisnika(kor.Id, kor);
            return RedirectToAction("Index");
        }

        public ActionResult Bolnica(int id)
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je pristupio detaljima bolnice sa id = {id}");
            ViewBag.Bolnica = bolnicaProvider.BolnicaSaID(id);
            return View();
        }

        [HttpPost]
        public ActionResult IzmeniBolnicu(Bolnica b)
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je izmenio bolnicu sa id = {b.Id}");
            var bol = bolnicaProvider.BolnicaSaID(b.Id);
            bol.BrojOdeljenja = b.BrojOdeljenja;
            bol.Naziv = b.Naziv;
            bol.Vrsta = b.Vrsta;
            bolnicaProvider.IzmeniBolnicu(bol.Id, bol);
            return RedirectToAction("Index");
        }

        public ActionResult Registracija(int tip)
        {
            ViewBag.Tip = (tip == 0 ? Tip.Administrator : (tip == 1 ? Tip.Pacijent : Tip.Lekar));
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je pristupio registraciji {Enum.GetName(typeof(Tip), ViewBag.Tip)}a");
            if (ViewBag.Tip == Tip.Lekar)
                ViewBag.Bolnice = bolnicaProvider.SveBolnice();
            return View();
        }

        public ActionResult RegistracijaBolnice()
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je pristupio registraciji bolnice");
            return View();
        }

        [HttpPost]
        public ActionResult RegistrujBolnicu(Bolnica bolnica)
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je pristupio registrovao bolnicu sa nazivom {bolnica.Naziv}");
            bolnicaProvider.DodajBolnicu(bolnica);
            return RedirectToAction("Index");
        }

        public ActionResult DodelaLekara()
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je pristupio dodeli lekara");
            ViewBag.Pacijenti = pacijentProvider.Svi();
            ViewBag.Lekari = lekarProvider.Svi();
            ViewBag.Undo = ((DodelaProvider)dodelaProvider).currentCommand != 0;
            ViewBag.Redo = ((DodelaProvider)dodelaProvider).currentCommand != ((DodelaProvider)dodelaProvider).m_Command.Count;
            return View();
        }

        public ActionResult Undo()
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je pozvao undo");
            dodelaProvider.Undo();
            return RedirectToAction("DodelaLekara");
        }

        public ActionResult Redo()
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je pozvao redo");
            dodelaProvider.Redo();
            return RedirectToAction("DodelaLekara");
        }

        [HttpPost]
        public ActionResult SetLekar(int pacijentId, int lekarId)
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je dodelio lekara korisniku sa id = {pacijentId}");
            dodelaProvider.Dodeli(pacijentProvider.Dobavi(pacijentId), lekarProvider.Dobavi(lekarId));
            return RedirectToAction("DodelaLekara");
        }
        [HttpPost]
        public ActionResult DeleteLekar(int pacijentId)
        {
            var user = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {user.KorisnickoIme} je odjavio lekara korisniku sa id = {pacijentId}");
            dodelaProvider.Odjavi(pacijentProvider.Dobavi(pacijentId));
            return RedirectToAction("DodelaLekara");
        }

        [HttpPost]
        public ActionResult DeleteKorisnik(int id)
        {
            var k = korisnikProvider.DobaviKorisnika(id);
            var user = (Korisnik)Session["user"];
            if(k.KorisnickoIme == "admin")
            {
                logger.LogError($"Korisnik {user.KorisnickoIme} je pokusao da obrise admin profil");
                return RedirectToAction("Index");
            }
            if(k.KorisnickoIme == user.KorisnickoIme)
            {
                logger.LogError($"Korisnik {user.KorisnickoIme} je pokusao da obrise sam sebe");
                return RedirectToAction("Index");
            }
            logger.LogInfo($"Korisnik {user.KorisnickoIme} brise {k.KorisnickoIme} korisnika");
            switch (k.Tip)
            {
                case Tip.Lekar:
                    lekarProvider.Obrisi(id);
                    break;
                case Tip.Pacijent:
                    pacijentProvider.Obrisi(id);
                    break;
                case Tip.Administrator:
                    korisnikProvider.ObrisiKorisnika(id);
                    break;
                default:
                    break;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Obavestenje(string tekst)
        {
            var k = (Korisnik)Session["user"];
            logger.LogInfo($"Korisnik {k.KorisnickoIme} je poslao obavestenje sa tekstom \"{tekst}\"");
            ((ConcreteSubject)subject).Tekst = tekst;
            subject.NotifyObservers();
            return RedirectToAction("Index");
        }
    }
}