using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public enum Status { Odbijen, Ceka, UToku, Pregledan }
    public class TerminDTO
    {
        private int id;
        private DateTime datumIVreme;
        private int bolnicaId;
        private int lekarId;
        private int pacijentId;
        private int trajanje;
        private Status statusCekanja;
        private Bolnica bolnica;
        private Lekar lekar;
        private Pacijent pacijent;

        public int Id { get => id; set => id = value; }
        public DateTime DatumIVreme { get => datumIVreme; set => datumIVreme = value; }
        public int BolnicaId { get => bolnicaId; set => bolnicaId = value; }
        public int LekarId { get => lekarId; set => lekarId = value; }
        public int PacijentId { get => pacijentId; set => pacijentId = value; }
        public int Trajanje { get => trajanje; set => trajanje = value; }
        public Status StatusCekanja { get => statusCekanja; set => statusCekanja = value; }
        public Bolnica Bolnica { get => bolnica; set => bolnica = value; }
        public Lekar Lekar { get => lekar; set => lekar = value; }
        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }

        public Termin GetTermin()
        {
            StatusCekanja s;
            switch (StatusCekanja)
            {
                case Status.Odbijen:
                    s = new Odbijen();
                    break;
                case Status.Ceka:
                    s = new Ceka();
                    break;
                case Status.UToku:
                    s = new UToku();
                    break;
                case Status.Pregledan:
                    s = new Pregledan();
                    break;
                default:
                    s = new Ceka();
                    break;
            }
            return new Termin(Bolnica, DatumIVreme, Id, Lekar, Pacijent, Trajanje, s);
        }
    }
}