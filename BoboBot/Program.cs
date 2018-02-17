using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BoboBot
{
    class Program
    {
        static void Main(string[] args)
        {
            string request = "";
            string response = "";
            string[] acciones = {"holas buenas saludos salud", "tiempo hará", "enciende led" };
            double[] coincidencias = {0.0, 0.0, 0.0};
            TaskAwaiter<string> tiempo = SAnalysis.getWeather().GetAwaiter();
            while (!request.Equals("quit")){
                Console.Write("User: ");
                request = Console.ReadLine();
                SAnalysis.totalCoincidence(SAnalysis.getWords(request), SAnalysis.getWords(acciones[0]), 0.5);
                for(int i=0; i<2; i++)
                {
                    coincidencias[i] = SAnalysis.totalCoincidence(SAnalysis.getWords(request), SAnalysis.getWords(acciones[i]), 0.5);
                }

                double max = coincidencias.Max();

                if (max > 0.6)
                {
                    int id = Array.IndexOf(coincidencias, max);

                    switch (id)
                    {
                        case 0:
                            response = SAnalysis.getSaludo();
                            break;
                        case 1:
                            response = tiempo.GetResult();
                            break;
                        case 2:
                            response = SAnalysis.setLed();
                            break;
                    }
                    Console.WriteLine(response);
                }
                else
                {
                    Console.WriteLine("Lo siento, no lo he entendido!!\nHumans Win ;P");
                }
            }
            Console.WriteLine("Goood bye!!");
        }
    }
}
