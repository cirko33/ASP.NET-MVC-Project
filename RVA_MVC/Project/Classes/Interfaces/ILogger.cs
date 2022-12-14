///////////////////////////////////////////////////////////
//  ILogger.cs
//  Implementation of the Interface ILogger
//  Generated by Enterprise Architect
//  Created on:      29-Aug-2022 18:54:29
//  Original author: Luka
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Classes {
	public interface ILogger  {

		/// 
		/// <param name="text"></param>
		void LogDebug(string text);

		/// 
		/// <param name="text"></param>
		void LogError(string text);

		/// 
		/// <param name="text"></param>
		void LogFatal(string text);

		/// 
		/// <param name="text"></param>
		void LogInfo(string text);

		/// 
		/// <param name="text"></param>
		void LogWarn(string text);
	}//end ILogger

}//end namespace Classes