using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;

namespace PracticeTask2
{
    class Program
    {
        // Task: search for a word-combination in an input text. Highlight it with "@" symbol and return the original text.
        // Student: Alexey Subbotin. Group: SE-17-1.
        static void Main(string[] args)
        {
            // Getting the wanted word combination.
            StreamReader sr = new StreamReader("INPUT.txt");
            string comb = sr.ReadLine();
            //string comb = Console.ReadLine();
            string[] wordComb = comb.Split(' ');

            // Output text.
            string outputText = "";
            // Input text is read by 1-2 lines at a time.
            string input = "";

            // Indicates the end of input.
            bool endOfInput = false;

            // Indicates what word of the word combination is being checked.
            int index = 0;

            // Stores the word with "@".
            string backup = "";

            // Number of the string that is being checked.
            int numOfCurrStr = -1;

            // Indexes where "@" should be inserted.
            int numOfStr = -1;
            int numOfWord = -1;

            // Do while it's not the end of input.
            do
            {
                // Getting the string.
                input = sr.ReadLine();
                //input = Console.ReadLine();

                // If it's empty that means it's then end of input.
                if (input != null)
                {
                    // Adding the string to the text.
                    outputText += input + "#";

                    // Definition of the current string number.
                    numOfCurrStr++;

                    // Creating an array of the string words.
                    string[] buf = CreateArrayOfWords(input);

                    // Going through all the words.
                    for (int i = 0; i < buf.Length; i++)
                    {
                        // If it's not a symbol-separator or empty.
                        if (buf[i] != " " && buf[i] != ((char)9).ToString() && buf[i] != "")
                        {
                            // Comparing string current word with the word combination current word.
                            if (CompareStrings(buf[i], wordComb[index]))
                            {
                                // If it's the 1st word of the word combination there should be "@".
                                // Store the updated word and its "coordinates".
                                if (index == 0)
                                {
                                    backup = "@" + buf[i];
                                    numOfStr = numOfCurrStr;
                                    numOfWord = i;
                                }

                                // Going to the next word of the word combination.
                                index++;
                            }
                            else
                            {
                                // If they are not equal that means it should start over again (index = 0).
                                if (index != 0)
                                {
                                    index = 0;
                                    // Maybe the word equals the 1st word of the combination.
                                    if (i != 0 && i != buf.Length - 1)
                                        i--;
                                }
                            }
                        }
                        else
                        {
                            // If all of the words are found.
                            if (index == wordComb.Length)
                            {
                                // Adding "@".
                                Add(ref outputText, ref index, numOfStr, numOfWord, backup);
                            }
                        }
                    }

                    // If all of the words are found (the end of the string).
                    if (index == wordComb.Length)
                    {
                        // Adding "@".
                        Add(ref outputText, ref index, numOfStr, numOfWord, backup);
                    }
                }
                else
                    endOfInput = true;

            } while (endOfInput != true);
            sr.Close();

            // Printing the output text.
            StreamWriter sw = new StreamWriter("OUTPUT.txt");
            for (int i = 0; i < outputText.Length; i++)
            {
                if (outputText[i] != '#')
                    sw.Write(outputText[i]);
                //Console.Write(outputText[i]);
                else
                    if (i != outputText.Length - 1)
                        sw.WriteLine();
                //Console.WriteLine();
            }
            sw.Close();

            //Console.ReadLine();
        }

        // Function to add "@".
        public static void Add(ref string outputText, ref int index, int numOfStr, int numOfWord, string backup)
        {
            // Getting the strings of the output text.
            string[] outputStrArr = outputText.Split('#');

            // Getting the words of the needed string.
            string[] strWordArr = CreateArrayOfWords(outputStrArr[numOfStr]);

            // Inserting "@".
            strWordArr[numOfWord] = backup;

            // Building everything back.
            outputStrArr[numOfStr] = String.Concat(strWordArr);
            outputText = "";
            for (int j = 0; j < outputStrArr.Length; j++)
            {
                if (outputStrArr[j] != "")
                    outputText += outputStrArr[j] + "#";
            }

            // Starting all over again.
            index = 0;
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
