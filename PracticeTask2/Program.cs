using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;

namespace PracticeTask2
{
    class Program
    {
        // Task: search for a word-combination in an input text. Highlight it with "@" symbol and return the original text.
        // Student: Alexey Subbotin. Group: SE-17-1.
        static void Main(string[] args)
        {
            //StreamReader sr = new StreamReader("input.txt");

            // Request.
            //string requestStr = sr.ReadLine();
            string requestStr = Console.ReadLine();
            List<char> request = new List<char>(requestStr);

            // The text.
            //string textStr = sr.ReadToEnd();
            string textStr = "";
            string buf = Console.ReadLine();
            while(buf != "")
            {
                textStr += "\r\n" + buf;
                buf = Console.ReadLine();
            }
            textStr = textStr.Substring(2, textStr.Length - 2);
            List<char> text = new List<char>(textStr.ToCharArray());

            //sr.Close();

            // Compressed text.
            List<char> compressed = new List<char>();

            // Text positions.
            List<int> textPos = new List<int>();

            // Creating compressed.
            for (int i = 0; i < text.Count; i++)
            {
                char c = text[i];

                if (c == ' ' || c == '\r' || c == '\n' || c == '\t')
                {
                    if (compressed != null && compressed[compressed.Count - 1] != ' ')
                    {
                        compressed.Add(' ');
                        textPos.Add(i);
                    }
                }
                else
                {
                    compressed.Add(c);
                    textPos.Add(i);
                }
            }

            // To lower.
            request = new List<char>(requestStr.ToLower());
            for (int i = 0; i < compressed.Count; i++)
            {
                if (compressed[i] >= 'A' && compressed[i] <= 'Z')
                {
                    compressed[i] = Convert.ToChar(compressed[i].ToString().ToLower());
                }
            }

            // Matches in text.
            List<bool> match = new List<bool>(text.Count);
            for (int i = 0; i < match.Capacity; i++)
                match.Add(false);


            // Finding matches.
            for (int i = 0; i + request.Count <= compressed.Count; i++)
            {
                bool matches = true;
                for (int d = 0; d < request.Count; d++)
                {
                    if (compressed[i + d] != request[d])
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    match[textPos[i]] = true;
                }
            }

            // Writing the new text.
            //StreamWriter sw = new StreamWriter("output.txt");
            //for (int i = 0; i < text.Count; i++)
            //{
            //    if (match[i])
            //        sw.Write("@");
            //    sw.Write(text[i]);

            //}
            //sw.Close();

            for (int i = 0; i < text.Count; i++)
            {
                if (match[i])
                    Console.Write("@");
                Console.Write(text[i]);

            }

            //Console.ReadLine();
        }
    }
}
