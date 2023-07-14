using System;
using System.Numerics;
using System.Collections.Generic;
using System.Runtime;

namespace Z2
{

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Prostokąt prostokąt = new Prostokąt(5, 10);
                Console.WriteLine($"BokA: {prostokąt.BokA}");
                Console.WriteLine($"BokB: {prostokąt.BokB}");

                Prostokąt arkusz = Prostokąt.ArkuszPapieru("B0");
                Console.WriteLine($"BokA arkusza: {arkusz.BokA}");
                Console.WriteLine($"BokB arkusza: {arkusz.BokB}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił wyjątek: {ex.Message}");
            }
        }
    }


    public class Prostokąt
    {
        private double bokA;
        private double bokB;

        public Prostokąt(double bokA, double bokB)
        {
            BokA = bokA;
            BokB = bokB;
        }

        public double BokA
        {
            get { return bokA; }
            set
            {
                if (double.IsNaN(value) || value < 0)
                    throw new ArgumentException("Długość boku musi być skończoną nieujemną.");
                bokA = value;
            }
        }

        public double BokB
        {
            get { return bokB; }
            set
            {
                if (double.IsNaN(value) || value < 0)
                    throw new ArgumentException("Długość boku musi być skończoną nieujemną.");
                bokB = value;
            }
        }

        public static Dictionary<char, decimal> wysokośćArkusza0 = new Dictionary<char, decimal>
        {
            ['A'] = 1189,
            ['B'] = 1414,
            ['C'] = 1297,
        };

        public static Prostokąt ArkuszPapieru(string format)
        {
            if (string.IsNullOrEmpty(format) || format.Length < 2)
                throw new ArgumentException("Niepoprawny format arkusza.");

            char X = format[0];
            if (!wysokośćArkusza0.ContainsKey(X))
                throw new ArgumentException("Nieznany klucz formatu arkusza.");

            byte indeks;
            if (!byte.TryParse(format.Substring(1), out indeks))
                throw new ArgumentException("Niepoprawny indeks formatu arkusza.");

            decimal wysokość = wysokośćArkusza0[X];
            double pierwiastekZDwóch = Math.Sqrt(2);
            double bokA = (double)(wysokość / (decimal)Math.Pow(pierwiastekZDwóch, indeks));
            double bokB = bokA / pierwiastekZDwóch;

            return new Prostokąt(bokA, bokB);
        }

    }
}
