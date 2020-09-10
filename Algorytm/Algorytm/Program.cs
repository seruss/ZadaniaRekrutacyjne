using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorytm
{
    class Program
    {
        static void Main(string[] args)
        {
            //dane wejściowe
            DateTime data_wystawienia = new DateTime(2014, 11, 1);
            DateTime niezdolnosc_do_pracy_od = new DateTime(2014, 10, 10);
            DateTime niezdolnosc_do_pracy_do = new DateTime(2014, 11, 10);
            DateTime pobyt_w_szpitalu_od = new DateTime(2014, 11, 2);
            DateTime pobyt_w_szpitalu_do = new DateTime(2014, 11, 5);

            Zaswiadczenie wsteczne, biezace;

            if(data_wystawienia>pobyt_w_szpitalu_od)
            {
                //w trakcie pobytu w szpitalu
                wsteczne = new Zaswiadczenie(niezdolnosc_do_pracy_od, pobyt_w_szpitalu_od.AddDays(-4));
                biezace = new Zaswiadczenie(pobyt_w_szpitalu_od.AddDays(-3), niezdolnosc_do_pracy_do, pobyt_w_szpitalu_od, pobyt_w_szpitalu_do);
            }
            else
            {
                //przed pobytem w szpitalu
                wsteczne = new Zaswiadczenie(niezdolnosc_do_pracy_od, data_wystawienia.AddDays(-4));
                biezace = new Zaswiadczenie(data_wystawienia.AddDays(-3), niezdolnosc_do_pracy_do, pobyt_w_szpitalu_od, pobyt_w_szpitalu_do);
            }
            Console.WriteLine(wsteczne);
            Console.WriteLine(biezace);
            Console.ReadKey();
        }
    }
}
