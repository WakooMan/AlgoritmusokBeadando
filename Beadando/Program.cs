using System;
using System.Collections.Generic;
using System.Linq;

namespace Beadando
{
    public class FutoVallalas
    {
        public readonly int I, E,ID;
        public FutoVallalas(int id,int i, int e)
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
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] splitted = Console.ReadLine().Split(' ');
            int Tavolsag = int.Parse(splitted[0]),FutokSzama = int.Parse(splitted[1]);
            List<FutoVallalas> Futok = new List<FutoVallalas>();
            for (int i = 0; i < FutokSzama; i++)
            {
                splitted = Console.ReadLine().Split(' ');
                Futok.Add(new FutoVallalas(i+1,int.Parse(splitted[0]), int.Parse(splitted[1])));
            }
            Futok.Add(new FutoVallalas(FutokSzama+1, Tavolsag, Tavolsag));
            Futok.Sort((f1, f2) => f1.I.CompareTo(f2.I));
            Dictionary<int,int> kivalogatottak = Kivalogatas(Futok);
            if (Futok[kivalogatottak.Keys.ElementAt(kivalogatottak.Count - 1)].E < Tavolsag)
            {
                Console.WriteLine(0);
                return;
            }
            Console.WriteLine(kivalogatottak.Count);
            for (int i = 0; i < kivalogatottak.Count; i++)
            {
                string szokoz = "";
                if (i < kivalogatottak.Count - 1)
                {
                    szokoz = " ";
                }
                Console.Write(kivalogatottak[kivalogatottak.Keys.ElementAt(i)] + szokoz);
            }
        }

        static Dictionary<int,int> Kivalogatas(List<FutoVallalas> Futok)
        {
            int Count = 0,lm = MaxKereses(Futok); 
            Dictionary<int,int> dict = new Dictionary<int,int>();
            dict.Add(lm,Futok[lm].ID);
            for (int i = lm+1; i < Futok.Count-1; i++)
            {
                if (Futok[i].E > Futok[lm].E)
                {
                    lm = i;
                }
                if (Futok[i + 1].I > Futok[dict.Keys.ElementAt(Count)].E)
                {
                    Count++;
                    dict.Add(lm, Futok[lm].ID);
                }
            }
            return dict;
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
    }
}
