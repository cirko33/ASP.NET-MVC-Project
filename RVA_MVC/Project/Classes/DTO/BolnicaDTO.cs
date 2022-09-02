using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class BolnicaDTO
    {
        private int id;
        private int brojlekara;
		private int brojodeljenja;
		private string naziv;
		private VrstaBolnice vrsta;
		private List<int> lekarIDs = new List<int>();
		private List<int> pacijentIDs = new List<int>();
		private List<Lekar> lekari = new List<Lekar>();
		private List<Pacijent> pacijenti = new List<Pacijent>();

        public int Id { get => id; set => id = value; }
        public int BrojLekara { get => brojlekara; set => brojlekara = value; }
        public int BrojOdeljenja { get => brojodeljenja; set => brojodeljenja = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public VrstaBolnice Vrsta { get => vrsta; set => vrsta = value; }
        public List<int> LekarIDs { get => lekarIDs; set => lekarIDs = value; }
        public List<int> PacijentIDs { get => pacijentIDs; set => pacijentIDs = value; }
        public List<Lekar> Lekari { get => lekari; set => lekari = value; }
        public List<Pacijent> Pacijenti { get => pacijenti; set => pacijenti = value; }

        public Bolnica GetBolnica()
        {
			return new Bolnica(Id, BrojLekara, BrojOdeljenja, Naziv, Vrsta, Lekari, Pacijenti);
        }
	}
}