///////////////////////////////////////////////////////////
//  ConcreteSubject.cs
//  Implementation of the Class ConcreteSubject
//  Generated by Enterprise Architect
//  Created on:      29-Aug-2022 18:54:29
//  Original author: Luka
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Classes;
namespace Classes {
	public class ConcreteSubject : Subject {
		private string tekst;

		public ConcreteSubject(){

		}

		~ConcreteSubject(){

		}

		/// 
		/// <param name="tekst"></param>
		public override void NotifyObservers(){
			m_Observer.ForEach(t => t.Notify());
		}

		/// 
		/// <param name="observer"></param>
		public override void Register(Observer observer){
			m_Observer.Add(observer);
		}

		public string Tekst
		{
			get
			{
				return tekst;
			}
			set
			{
				tekst = value;
			}
		}

		/// 
		/// <param name="observer"></param>
		public override void Unregister(Observer observer){
			m_Observer.Remove(observer);
		}

	}//end ConcreteSubject

}//end namespace Classes