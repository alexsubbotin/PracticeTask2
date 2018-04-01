using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Getting the wanted word combination.
            string comb = Console.ReadLine();
            string[] wordComb = comb.Split(' ');


            // Creating the array of input strings.
            string text = "";
            string inputString;
            do
            {
                inputString = Console.ReadLine();
                if (inputString != "")
                    text += inputString + "+";
            } while (inputString != "");

            text = text.Remove(text.Length - 1, 1);
            string[] inpStr = text.Split('+');

            // Creating a jagged array of ALL words where i - string, j - word.
            string[][] jagArrWords = new string[inpStr.Length][];

            // Variables that indicate whether the wanted were founf or not.
            bool w1 = false;
            bool w2 = false;
            // Indexes where "@" should be inserted.
            int I = -1;
            int J = -1;

            for (int i = 0; i < jagArrWords.Length; i++)
            {
                jagArrWords[i] = CreateArrayOfWords(inpStr[i]);

                for (int j = 0; j < jagArrWords[i].Length; j++)
                {
                    if (jagArrWords[i][j] != " " && jagArrWords[i][j] != ((char)9).ToString())
                    {
                        if (!w1)
                        {
                            w1 = CompareStrings(jagArrWords[i][j], wordComb[0]);
                            I = i;
                            J = j;
                        }
                        else
                        {
                            w2 = CompareStrings(jagArrWords[i][j], wordComb[1]);

                            if (!w2)
                            {
                                w1 = false;
                                w2 = false;
                                //I = -1;
                                //J = -1;
                            }
                            else
                            {
                                jagArrWords[I][J] = "@" + jagArrWords[I][J];
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < jagArrWords.Length; i++)
            {
                for (int j = 0; j < jagArrWords[i].Length; j++)
                    Console.Write(jagArrWords[i][j]);
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        // Function to compare input strings with the wanted ones (even with letters of different cases)
        public static bool CompareStrings(string sInput, string sWanted)
        {
            bool ok = true;

            if (sInput.Length >= sWanted.Length)
            {
                // Going through all letters in the wanted string (beacuse in the input one might be more me due to variuos endings).
                for (int i = 0; i < sWanted.Length; i++)
                {
                    if ((int)sInput[i] == (int)sWanted[i] || (int)sInput[i] == ((int)sWanted[i] - 32) ||
                        (int)sInput[i] == ((int)sWanted[i] + 32))
                    // If letters are equal. 
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                        break;
                    }
                }
            }
            else
                ok = false;

            return ok;
        }

        // Fucntion to create an array of different words in an input string (SPACE is a word too).
        public static string[] CreateArrayOfWords(string s)
        {
            // Creating a StringBuilder "clone" of the input string to use Insert() method.
            StringBuilder sb = new StringBuilder(s);

            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == ' ' || sb[i] == (char)9)
                // If it is a symbol-separator we add + before and after it.
                {
                    sb.Insert(i, '+');
                    sb.Insert(i + 2, '+');
                    i = i + 2;
                }
            }

            s = sb.ToString();

            string[] words = s.Split('+');

            return words;
        }
    }
}
