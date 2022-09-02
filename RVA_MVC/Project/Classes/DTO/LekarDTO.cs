using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class LekarDTO
    {
		private int id;
		private string ime;
		private string korisnickoIme;
		private string lozinka;
		private string prezime;
		private Tip tip;
        private Odeljenje odeljenje;
        private Specijalizacija specijalizacija;
        private Titula titula;
        private int bolnicaId;
        private Bolnica bolnica;

        public int Id { get => id; set => id = value; }
        public string Ime { get => ime; set => ime = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Tip Tip { get => tip; set => tip = value; }
        public Odeljenje Odeljenje { get => odeljenje; set => odeljenje = value; }
        public Specijalizacija Specijalizacija { get => specijalizacija; set => specijalizacija = value; }
        public Titula Titula { get => titula; set => titula = value; }
        public int BolnicaId { get => bolnicaId; set => bolnicaId = value; }
        public Bolnica Bolnica { get => bolnica; set => bolnica = value; }

        public Lekar GetLekar()
        {
            return new Lekar(Id, Ime, KorisnickoIme, Lozinka, Prezime, Tip, Odeljenje, Specijalizacija, Titula, Bolnica); 
        }
    }
}