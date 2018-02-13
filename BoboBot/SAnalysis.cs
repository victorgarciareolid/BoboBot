﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BoboBot
{
    class SAnalysis
    {
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

            return rate / (2* (a.Length - 2));
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

        public static double totalCoincidence(string[] input, string[] pattern)
        {
            double aux = 0.0;

            foreach(string p in pattern)
            {
                foreach(string s in input)
                {
                    aux += SAnalysis.Coincidence(p, s);
                    Console.WriteLine("{0}, {1} -> {2}", s, p, aux);
                }
            }

            return aux;
        }
    }
}