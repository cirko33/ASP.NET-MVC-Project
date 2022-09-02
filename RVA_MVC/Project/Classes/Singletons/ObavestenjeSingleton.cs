using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class ObavestenjeSingleton
    {
        static IObavestenjeProvider obavestenje;
        public static IObavestenjeProvider GetObavestenje() => obavestenje;
        public static void SetObavestenje(IObavestenjeProvider o) => obavestenje = o;
    }
}