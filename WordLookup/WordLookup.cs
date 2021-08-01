using System;
using Project1;

namespace WordLookup
{
    class WordLookup
    {
        static void Main(string[] args)
        {
            EnglishDictionary dict = new EnglishDictionary("words.txt");
            Console.WriteLine("This application allows you to lookup words within a text file (words.txt). Please enter the list of words that you would like to check, separate" +
                "each word by a space. This application is insensitive to capitalization.");
            string[] toks = Console.ReadLine().Split(' ');
            if (toks.Length > 0)
            {
                foreach (string word in toks)
                {
                    bool isLegal = dict.IsLegal(word.ToLower());
                    if (isLegal)
                    {
                        Console.WriteLine(word + " (legal)");
                    }
                    else
                    {
                        Console.WriteLine(word + " (illegal)");
                    }
                }
            }
            else
            {
                Console.WriteLine("You cannot enter an empty list");
            }
        }
    }
}
