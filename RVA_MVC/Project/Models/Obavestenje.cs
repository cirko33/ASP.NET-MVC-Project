///////////////////////////////////////////////////////////
//  Obavestenje.cs
//  Implementation of the Class Obavestenje
//  Generated by Enterprise Architect
//  Created on:      30-Aug-2022 22:09:59
//  Original author: Luka
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Classes {
	public class Obavestenje {

		private Korisnik korisnik;
		private string tekst;
		private int id;
		public Obavestenje(){

		}

        public Obavestenje(Korisnik korisnik, string tekst)
        {
            this.korisnik = korisnik;
            this.tekst = tekst;
        }

        ~Obavestenje(){

		}

		public Korisnik Korisnik{
			get{
				return korisnik;
			}
			set{
				korisnik = value;
			}
		}

		public string Tekst{
			get{
				return tekst;
			}
			set{
				tekst = value;
			}
		}

		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

	}//end Obavestenje

}//end namespace Classes