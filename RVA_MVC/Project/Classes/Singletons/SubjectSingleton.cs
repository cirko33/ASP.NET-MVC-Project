using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class SubjectSingleton
    {
        static Subject subject;
        public static Subject GetSubject() => subject;
        public static void SetSubject(Subject s) => subject = s;
    }
}