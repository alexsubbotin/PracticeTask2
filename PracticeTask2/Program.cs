using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

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
            string outputText = "";
            string input1 = "";
            string input2 = "";

            // Variables that indicate whether the wanted were founf or not.
            bool w1 = false;
            bool w2 = false;

            // Indexes where "@" should be inserted.
            int I = -1;
            //int J = -1;

            bool endOfInput = false;

            do
            {
                if (input1 == "")
                    input1 = Console.ReadLine();

                string[] buf1 = new string[0];

                if (input1 != "")
                {
                    buf1 = CreateArrayOfWords(input1);

                    for (int i = 0; i < buf1.Length; i++)
                    {
                        if (buf1[i] != " " && buf1[i] != ((char)9).ToString())
                        {
                            if (!w1)
                            {
                                w1 = CompareStrings(buf1[i], wordComb[0]);
                                I = i;
                            }
                            else
                            {
                                w2 = CompareStrings(buf1[i], wordComb[1]);

                                if (!w2)
                                {
                                    w1 = false;
                                }
                                else
                                {
                                    buf1[I] = "@" + buf1[I];
                                    input1 = String.Concat(buf1);
                                    //outputText += input1 + "#";
                                    w1 = false;
                                    w2 = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    endOfInput = true;
                }

                if (w1)
                {
                    input2 = Console.ReadLine();

                    if(input2 != "")
                    {
                        string[] buf2 = CreateArrayOfWords(input2);

                        w2 = CompareStrings(buf2[0], wordComb[1]);

                        if (w2)
                        {
                            buf1[I] = "@" + buf1[I];
                            input1 = String.Concat(buf1);
                            outputText += input1 + "#";
                            //outputText += input2 + "#";
                            w1 = false;
                            w2 = false;
                        }
                        else
                        {
                            w1 = false;
                        }

                        input1 = input2;
                    }
                    else
                    {
                        endOfInput = true;
                    }
                }
                else
                {
                    outputText += input1 + "#";
                    input1 = "";
                }
            } while (!endOfInput);

            for(int i = 0; i < outputText.Length; i++)
            {
                if (outputText[i] != '#')
                    Console.Write(outputText[i]);
                else
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
