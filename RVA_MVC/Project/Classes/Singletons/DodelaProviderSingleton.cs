using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class DodelaProviderSingleton
    {
        static IDodelaProvider dodela;
        public static IDodelaProvider GetDodelaProvider() => dodela;
        public static void SetDodelaProvider(IDodelaProvider d) => dodela = d;
    }
}