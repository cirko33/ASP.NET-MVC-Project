///////////////////////////////////////////////////////////
//  Termin.cs
//  Implementation of the Class Termin
//  Generated by Enterprise Architect
//  Created on:      29-Aug-2022 18:54:30
//  Original author: Luka
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Classes;
namespace Classes {
	public class Termin {

		private Bolnica bolnica;
		private DateTime datumIVreme;
		private int id;
		private Lekar lekar;
		private Pacijent pacijent;
		private int trajanje;
		public StatusCekanja m_StatusCekanja;

		public Termin(){

		}

        public Termin(Bolnica bolnica, DateTime datumIVreme, int id, Lekar lekar, Pacijent pacijent, int trajanje, StatusCekanja statusCekanja)
        {
            this.bolnica = bolnica;
            this.datumIVreme = datumIVreme;
            this.id = id;
            this.lekar = lekar;
            this.pacijent = pacijent;
            this.trajanje = trajanje;
            m_StatusCekanja = statusCekanja;
        }

        ~Termin(){

		}

		public Bolnica Bolnica{
			get{
				return bolnica;
			}
			set{
				bolnica = value;
			}
		}

		public DateTime DatumIVreme{
			get{
				return datumIVreme;
			}
			set{
				datumIVreme = value;
			}
		}

		public int Id{
			get{
				return id;
			}
			set{
				id = value;
			}
		}

		public Lekar Lekar{
			get{
				return lekar;
			}
			set{
				lekar = value;
			}
		}

		public Pacijent Pacijent{
			get{
				return pacijent;
			}
			set{
				pacijent = value;
			}
		}

		public int Trajanje{
			get{
				return trajanje;
			}
			set{
				trajanje = value;
			}
		}

		public void ChangeState(StatusCekanja status)
        {
			m_StatusCekanja = status;
        }

		public void Odbij()
        {
			m_StatusCekanja.Odbij(this);
        }

		public void Zapocni()
        {
			m_StatusCekanja.Zapocni(this);
        }

		public void Zavrsi()
        {
			m_StatusCekanja.Zavrsi(this);
        }

	}//end Termin

}//end namespace Classes