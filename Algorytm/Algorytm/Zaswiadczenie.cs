using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorytm
{

    public enum RodzajZaswiadczenia
    {
        wsteczne,
        biezace
    }

    class Zaswiadczenie
    {

        public RodzajZaswiadczenia rodzaj_zaswiadczenia { get; private set; }
        public DateTime niezdolnosc_do_pracy_od { get; private set; }
        public DateTime niezdolnosc_do_pracy_do { get; private set; }
        public DateTime pobyt_w_szpitalu_od { get; private set; }
        public DateTime pobyt_w_szpitalu_do { get; private set; }
        private string dateFormat = "dd.MM.yyyy";

        public Zaswiadczenie()
        {
            niezdolnosc_do_pracy_od = new DateTime();
            niezdolnosc_do_pracy_do = new DateTime();
            pobyt_w_szpitalu_od = new DateTime();
            pobyt_w_szpitalu_do = new DateTime();
        }

        public Zaswiadczenie(DateTime ndpo, DateTime ndpd)
        {
            niezdolnosc_do_pracy_od = ndpo;
            niezdolnosc_do_pracy_do = ndpd;
            rodzaj_zaswiadczenia = RodzajZaswiadczenia.wsteczne;
        }

        public Zaswiadczenie(DateTime ndpo, DateTime ndpd, DateTime pwso, DateTime pwsd)
        {
            niezdolnosc_do_pracy_od = ndpo;
            niezdolnosc_do_pracy_do = ndpd;
            pobyt_w_szpitalu_od = pwso;
            pobyt_w_szpitalu_do = pwsd;
            rodzaj_zaswiadczenia = RodzajZaswiadczenia.biezace;
        }

        public override string ToString()
        {
            if(rodzaj_zaswiadczenia==RodzajZaswiadczenia.biezace)
            return "Niezdolność do pracy od " + niezdolnosc_do_pracy_od.ToString(dateFormat) +"\n"+
                "Niezdolność do pracy do " + niezdolnosc_do_pracy_do.ToString(dateFormat) + "\n" +
                "Pobyt w szpitalu od " + pobyt_w_szpitalu_od.ToString(dateFormat) + "\n" +
                "Pobyt w szpitalu do " + pobyt_w_szpitalu_do.ToString(dateFormat) + "\n" +
                "Rodzaj zaświadczenia " + rodzaj_zaswiadczenia.ToString();
            else return "Niezdolność do pracy od " + niezdolnosc_do_pracy_od.ToString(dateFormat) + "\n" +
                "Niezdolność do pracy do " + niezdolnosc_do_pracy_do.ToString(dateFormat) + "\n" +
                "Rodzaj zaświadczenia " + rodzaj_zaswiadczenia.ToString();
        }
    }
}
