using System;
using System.Collections.Generic;
using System.Linq;

namespace FelhasznaloBaratBeadando
{
    internal class Program
    {
        public class FutoVallalas
        {
            public readonly int I, E, ID;
            public FutoVallalas(int id, int i, int e)
            {
                ID = id;
                I = i;
                E = e;
            }

            public override string ToString()
            {
                return $"ID:{ID}, Indulási: {I}, Érkezési: {E}";
            }
        }

        static int MaxKereses(List<FutoVallalas> Futok)
        {
            int max = Futok[0].E;
            int maxindex = 0;
            for (int i = 1; i < Futok.Count && Futok[i].I == 0; i++)
            {
                if (Futok[i].E > max)
                {
                    max = Futok[i].E;
                    maxindex = i;
                }
            }
            return maxindex;
        }

        static Dictionary<int, int> Kivalogatas(List<FutoVallalas> Futok)
        {
            int Count = 0, lm = MaxKereses(Futok);
            Dictionary<int, int> dict = new Dictionary<int, int>();
            dict.Add(lm, Futok[lm].ID);
            for (int i = lm + 1; i < Futok.Count - 1; i++)
            {
                if (Futok[i].E > Futok[lm].E)
                {
                    lm = i;
                }
                if (Futok[i + 1].I > Futok[dict.Keys.ElementAt(Count)].E && Futok[dict.Keys.ElementAt(Count)].E >= Futok[lm].I && !dict.ContainsKey(lm))
                {
                    Count++;
                    dict.Add(lm, Futok[lm].ID);
                }
            }
            return dict;
        }

        static void Main(string[] args)
        {
            int Tavolsag = SzamotBeker($"Kérem adja meg a két város közti távolságot 10 és 1000 között:  ",10,1000);
            int FutokSzama = SzamotBeker($"Kérem adja meg a futók számát 2 és 20 000 között:  ",2,20000);
            List<FutoVallalas> Futok = new List<FutoVallalas>();
            for (int i = 0; i < FutokSzama; i++)
            {
                int I = SzamotBeker($"Kérem adja meg az {i + 1}. futó indulási pontjának távolságát az első várostól 0 és {Tavolsag} között: ",0,Tavolsag);
                int E = SzamotBeker($"Kérem adja meg az {i + 1}Kérem adja meg a két város közti távolságot 10 és 10000 között:   {Tavolsag} között: ",I+1,Tavolsag);
                Futok.Add(new FutoVallalas(i + 1, I, E));
            }
            Futok.Add(new FutoVallalas(FutokSzama + 1, Tavolsag, Tavolsag));
            Futok.Sort((f1, f2) => f1.I.CompareTo(f2.I));
            Dictionary<int, int> kivalogatottak = Kivalogatas(Futok);
            if (Futok[kivalogatottak.Keys.ElementAt(kivalogatottak.Count - 1)].E < Tavolsag)
            {
                Console.WriteLine("A láng nem juttatható el a cél városig a jelentkezett futókkal!");
                return;
            }
            Console.WriteLine($"Ennyi futó kell minimum a láng célba juttatásához: {kivalogatottak.Count}");
            Console.WriteLine("\nA futók sorszámai:");
            for (int i = 0; i < kivalogatottak.Count; i++)
            {
                Console.WriteLine($"\t {kivalogatottak[kivalogatottak.Keys.ElementAt(i)]}");
            }
        }

        static int SzamotBeker(string szoveg,int min,int max)
        {
            Console.Write(szoveg);
            string line = Console.ReadLine();
            int result;
            bool nemszam = !int.TryParse(line, out result);
            bool intervallumonkivul = result < min || result > max;
            bool l = nemszam || intervallumonkivul;
            while (l)
            {
                if (nemszam)
                {
                    Console.WriteLine("A megadott szöveg nem egy szám!");
                }
                else if (intervallumonkivul)
                {
                    Console.WriteLine($"{min}-{max} közötti számot kell megadni!");
                }
                Console.Write(szoveg);
                line = Console.ReadLine();
                nemszam = !int.TryParse(line, out result);
                intervallumonkivul = result < min || result > max;
                l = nemszam || intervallumonkivul;
            }
            return result;
        }
    }
}
