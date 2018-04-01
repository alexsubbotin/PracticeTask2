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


            // Output text
            string outputText = "";
            // Input text is read by 1-2 lines at a time.
            string input = "";
            //string input2 = "";

            // Variables that indicate whether the wanted were found or not.
            //bool w1 = false;
            //bool w2 = false;

            // Indexes where "@" should be inserted.
            int I = -1;

            bool endOfInput = false;


            int index = 0;
            string backup = "";
            int numOfCurrStr = -1;
            int numOfStr = -1;
            int numOfWord = -1;

            do
            {
                input = Console.ReadLine();
                if (input != "")
                {
                    outputText += input + "#";

                    numOfCurrStr++;

                    string[] buf = CreateArrayOfWords(input);

                    for (int i = 0; i < buf.Length; i++)
                    {

                        if (buf[i] != " " && buf[i] != ((char)9).ToString() && buf[i] != "")
                        {
                            if (index < wordComb.Length)
                            {
                                if (CompareStrings(buf[i], wordComb[index]))
                                {
                                    if (index == 0)
                                    {
                                        backup = "@" + buf[i];
                                        numOfStr = numOfCurrStr;
                                        numOfWord = i;
                                    }

                                    index++;
                                }
                                else
                                {
                                    if (index != 0)
                                    {

                                        index = 0;
                                        if (i != 0 && i != buf.Length - 1)
                                            i--;
                                    }
                                }
                            }
                            else
                            {
                                //outputText += input + "#";
                                string[] outputStrArr = outputText.Split('#');
                                string[] strWordArr = CreateArrayOfWords(outputStrArr[numOfStr]);
                                strWordArr[numOfWord] = backup;
                                outputStrArr[numOfStr] = String.Concat(strWordArr);
                                outputText = "";
                                for (int j = 0; j < outputStrArr.Length; j++)
                                {
                                    if (outputStrArr[j] != "")
                                        outputText += outputStrArr[j] + "#";
                                }
                                index = 0;
                                if (i != 0 && i != buf.Length - 1)
                                    i--;
                                //break;
                            }
                        }
                    }


                    if (index == wordComb.Length)
                    {
                        // outputText += input + "#";

                        string[] outputStrArr = outputText.Split('#');
                        string[] strWordArr = CreateArrayOfWords(outputStrArr[numOfStr]);
                        strWordArr[numOfWord] = backup;
                        outputStrArr[numOfStr] = String.Concat(strWordArr);
                        outputText = "";
                        for (int j = 0; j < outputStrArr.Length; j++)
                        {
                            if (outputStrArr[j] != "")
                                outputText += outputStrArr[j] + "#";
                        }
                        index = 0;
                    }
                    //else
                    //    outputText += input + "#";
                }
                else
                    endOfInput = true;

            } while (endOfInput != true);


            for (int i = 0; i < outputText.Length; i++)
            {
                if (outputText[i] != '#')
                    Console.Write(outputText[i]);
                else
                    if (i != outputText.Length - 1)
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
