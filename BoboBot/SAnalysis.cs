using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BoboBot
{
    class SAnalysis
    {
        private static HttpClient c = new HttpClient();
        private static string appid = "1a98e117a2a137a5977e46d9b8f6cf2e";
        private static string city = "barcelona";

        public static string Clean(string input)
        {
            string output = "";
            input = input.ToLower();
            for(int i=0; i<input.Length; i++)
            {
                if (Char.IsLetterOrDigit(input[i]))
                {
                    output += input[i];
                }
            }
            return output;
        }

        // Coincidence degree between two strings expressed as a percentage
        public static double Coincidence(string a, string b)
        {
            string aux;
            double rate=0.0;
            int j = 0;

            if (a.Length == 0 || b.Length == 0) return 0.0;
            if (a.Equals(b)) return 1.0;

            if (a.Length > b.Length)
            {
                aux = a;
                a = b;
                b = aux;
            }
            // Now "a" is the shortest string
            a = " " + a + " ";
            b = " " + b + " ";
            for (int i=1; i<a.Length-1; i++)
            {
                j = b.IndexOf(a[i]);
                if (j > 0 && b[j - 1].Equals(a[i - 1])) rate += 1;
                if (j > 0 && b[j + 1].Equals(a[i + 1])) rate += 1;
            }

            return rate / (2* (b.Length - 2));
        }

        public static string[] getWords(string input)
        {
            string[] aux = Regex.Split(input, " ");
            for(int i = 0; i<aux.Length; i++)
            {
                aux[i] = SAnalysis.Clean(aux[i]);
            }

            return aux;
        }
        public static void print(string[] input)
        {
            foreach(string s in input)
            {
                Console.WriteLine(s);
            }
        }

        public static double totalCoincidence(string[] input, string[] pattern, double dim)
        {
            double aux, coi = 0.0;
            int maincoi = 0;

            foreach(string p in pattern)
            {
                foreach(string s in input)
                {
                    aux = SAnalysis.Coincidence(p, s); ;
                    if (aux>=dim)
                    {
                        maincoi++;
                        coi += aux;
                    }
                    //Console.WriteLine("{0}, {1} -> {2}", s, p, aux);
                }
            }
            if (coi == 0) maincoi = 1;
            return coi/maincoi;
        }

        public async static Task<string> getWeather()
        {
            c.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
            string param =  "?" + "q=" + city + "&" + "appid=" + appid;
            HttpResponseMessage m = await c.GetAsync(param);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Tiempo));
            Stream weatherJson = await m.Content.ReadAsStreamAsync();

            Tiempo t = (Tiempo)ser.ReadObject(weatherJson);


            return "La temperatura es " + (t.main.temp - 273) + "C y hay una humedad relativa del " + t.main.humidity + "%";

        }

        public static string setLed()
        {

            return "Led Switched!";
        }

        public static string getSaludo()
        {
            int hour = DateTime.Now.Hour;
            if (6<hour && hour<12)
            {
                return "Buenoooos Diaaas!!";
            }else if (12<= hour && hour < 20)
            {
                return "Bueeenas Tardes!!";
            }
            else
            {
                return "Bueenas Noches!!";
            }
        }
    }
}
