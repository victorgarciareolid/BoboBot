using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoboBot
{
    class Program
    {
        static void Main(string[] args)
        {
            string saludo = "Hola!! me Llamo victor!";

            SAnalysis.print(SAnalysis.getWords(saludo));
            Console.WriteLine(SAnalysis.totalCoincidence(SAnalysis.getWords(saludo), SAnalysis.getWords("hola saludos buenas")));
            Console.ReadKey();
        }
    }
}
