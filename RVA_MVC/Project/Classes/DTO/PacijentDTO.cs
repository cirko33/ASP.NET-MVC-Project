using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class PacijentDTO
    {
        private int id;
        private string ime;
        private string korisnickoIme;
        private string lozinka;
        private string prezime;
        private Tip tip;
        private string jmbg;
        private int lekarId;
        private Lekar lekar;

        public int Id { get => id; set => id = value; }
        public string Ime { get => ime; set => ime = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Tip Tip { get => tip; set => tip = value; }
        public string JMBG { get => jmbg; set => jmbg = value; }
        public int LekarId { get => lekarId; set => lekarId = value; }
        public Lekar Lekar { get => lekar; set => lekar = value; }

        public Pacijent GetPacijent()
        {
            return new Pacijent(Id, Ime, KorisnickoIme, Lozinka, Prezime, Tip, JMBG, Lekar);
        }
    }
}