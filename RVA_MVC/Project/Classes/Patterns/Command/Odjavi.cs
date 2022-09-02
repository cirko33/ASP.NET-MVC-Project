///////////////////////////////////////////////////////////
//  Odjavi.cs
//  Implementation of the Class Odjavi
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
	public class Odjavi : ConcreteCommand {

		public Odjavi(){

		}

        public Odjavi(DodelaLekara dodelaLekara, Pacijent pacijent, Lekar lekar) : base(dodelaLekara, pacijent, lekar)
        {
        }

        ~Odjavi(){

		}

		public override void Execute(){
			lekar = pacijent.Lekar;
			m_DodelaLekara.Odjavi(pacijent);
		}

		public override void Unexecute(){
			m_DodelaLekara.Dodeli(pacijent, lekar);
		}

	}//end Odjavi

}//end namespace Classes