using System;
using System.Collections.Generic;
using Project1;

namespace WordFormation
{
    class WordFormation
    {
        static void Main(string[] args)
        {
            EnglishDictionary dict = new EnglishDictionary("words.txt");
            List<string> list;
            Console.WriteLine("Enter a number and letters with no spaces (ex. 5 herarth). The number is the minimum length for the words. The letters are used to create words.");
            string[] toks = Console.ReadLine().Split(' ');
            if (toks.Length == 2)
            {
                int.TryParse(toks[0], out int minLength);
                if (minLength != 0 && minLength > 2)
                    list = (List<string>)dict.ScrabbleWords(toks[1], minLength);
                else
                    Console.WriteLine("This is invalid input, make sure the min length is more than 2 characters long");
            } 
            else
            {
                Console.WriteLine("This is invalid input");
            }
        }
    }
}
