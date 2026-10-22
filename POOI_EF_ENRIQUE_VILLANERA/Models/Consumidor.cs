using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POOI_EF_ENRIQUE_VILLANERA.Models
{
    public class Consumidor
    {

        public string dnicont { get; set; }

        public string nomcont { get; set; }

        public string apecont { get; set; }

        public string emailcont { get; set; }

        public string idpais { get; set; }

        public Consumidor() { }

        public Consumidor(string dnicont,string nomcont,string apecont, string emailcont,string idpais)
        {
            this.dnicont = dnicont;
            this.nomcont = nomcont;
            this.apecont = apecont;
            this.emailcont = emailcont;
            this.idpais = idpais;
        }
    
}
}