using System;
using System.Linq;

namespace CA191010OB
{
    
    //cica struktúra (nem kell a { get; set; } feltétlenül
    struct Cica
    {
        public string Nev { get; set; }
        public ConsoleColor Szin { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //cica példányok egy hard coded cica-vektorban
            var ct = new Cica[]
            {
                new Cica() { Nev = "Bubópestis", Szin = ConsoleColor.White },
                new Cica() { Nev = "COPD", Szin = ConsoleColor.Green },
                new Cica() { Nev = "Alzheimer", Szin = ConsoleColor.Red },
            };

            //cicák kiírása eredeti sorrendben
            foreach (var c in ct)
            {
                Console.ForegroundColor = c.Szin;
                Console.WriteLine(c.Nev);
            }

            //vissza az eredeti színre:
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-----------------");

            //Array.Sort():
            //magyarázat:
            /*
                Array: vektor
                Sort: rendezés
                <Cica>: ilyen típusú tömböt kívánok rendezni
                    (elhagyható, ha a zárójelben egyértelműen kiderül: most is pl.)
                ---
                ct: a vektor, aminek az elemeit rendezni kívánom
                (x, y) paraméterben átadott anonim függvény:
                    neve nincs, csak két paramétere X és Y
                    mindkettő olyan típusú, mint a ct elemei (struct Cica)
                => rövid formája a függvény "törzsének",
                    NEM "nagyobb vagy egyenlő (az >= így van)
                    akkor használható, ha egy szekvenciába elfér a teljes törzs
                    így elhagyható a return szó, és a zárójelek
                x.Nev.CompareTo(y.Nev)
                    a CompareTo(valami) az IComperable ("összahasonlítható") 
                    interface-t teljesítő típusok példányaira hívható meg.
                    Működése pl. dolog.CompareTo(valami) esetén:
                        megnézi, hogyha a dolgot a valamivel szeretné rendezni,
                        akkor a dolok a valami előtt van, ugyan ott, vagy mögötte
                        egy számmal tér vissza ennek megfelelően (-1, 0, +1, azt hiszem)
                    az IComperable interfacet MINDEN alaptípus teljesíti, és bár a String 
                    nem "valódi" primitív típus
                        (valójában karakterek vektora, a char pedig egy 16bit-es egész 
                        szám a UNICODE táblában -> tehát gépi kód szintjén egy egész 
                        számokat tartalmazó tömb)
                    DE a gyakorisága miatt erre is megcsinálták, és ugyan úgy működik, mint
                    a többin.

            */
            Array.Sort<Cica>(ct, (x, y) => x.Nev.CompareTo(y.Nev));
            
            foreach (var c in ct)
            {
                Console.ForegroundColor = c.Szin;
                Console.WriteLine(c.Nev);
            }

            //kever:
            var csc = ct[1]; ct[1] = ct[0]; ct[0] = ct[2]; ct[2] = csc;

            //megint kiír:
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-----------------");
            Console.WriteLine("-----------------");
            Console.WriteLine("-----------------");
            foreach (var c in ct)
            {
                Console.ForegroundColor = c.Szin;
                Console.WriteLine(c.Nev);
            }

            //LINQ:
            //lényeg: adatbázis szerű logikát átvezeti a magasszintű nyelvek logikájára:
            //rendezés pl: OrderBy(mi szerint) függvény ->
            //visszaad valamit szart, amit lehet "ismert" adatszerkezetté alakítani:
            // tömb = tömb.OrderBy(e => e).ToTömb() 
            //itt pl. "e" azaz elemek szerint rendezi a 'tömb'-öt, visszaalakítja 'tömb'
            //típusúvá, miután végzett, és ezt bemásolja az eredeti 'tömb' változó helyére
            //felülírva azt --->


            Cica[] rendezettCicak = ct.OrderBy(c => c.Nev).ToArray();


            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-----------------");
            foreach (var c in rendezettCicak)
            {
                Console.ForegroundColor = c.Szin;
                Console.WriteLine(c.Nev);
            }

            //TEHÁT: a lényegi különbség, Sort() helyben csinálja a dolgát, 
            //az OrderBy() pedig (akárcsak egy relációs adatbázis lekérdezés) egy "nézetet"
            //hoz létre arról, amit "rendezés" után csinált, de az eredeti adatszerkezet strukturáját
            //nem b*ssza el

            //mgj: ebben a formában az ékezetekre pl. magasról tesz mind az OrderBy(), mind  a Sort()

            //van kultúra specifikus változata a CompareTo()-nak, a 
            //System.Globalization; és az System.Threading
            //névterekben van rá megoldás, hogy 'madjar' nyelven rendezzenek ezek a cuccok.
            //elméletileg itt részletezve van:
            //https://docs.microsoft.com/en-us/dotnet/api/system.stringcomparer.currentculture?view=netframework-4.8
            //engem most így hirtelen annyira nem érdekel, hogy elolvassam

            Console.ReadKey();
        }
    }
}
