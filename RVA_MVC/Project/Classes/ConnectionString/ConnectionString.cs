using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Classes
{
    public static class ConnectionString
    {
        static string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\ConnectionString.xml";
        public static string Read()
        {
            XmlSerializer xml = new XmlSerializer(typeof(string));
            if (!File.Exists(path))
            {
                Write("");
            }
            using (var r = new StreamReader(path))
            {
                return (string)xml.Deserialize(r);
            }
        }

        public static void Write(string text)
        {
            XmlSerializer xml = new XmlSerializer(typeof(string));
            using (var w = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\ConnectionString.xml", false))
            {
                xml.Serialize(w, text);
            }
        }
    }
}