using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
/**
 * Project: Create a shared project that has two console applications that utilize the EnglishDictionary
 * class.
 * - Start by reading the dictionary file through a task to give a fast user experience.
 * - A method to determine if a word can be found in the dictionary file.
 * - A scrabble words method creates the possible words and confirms that they are in the dictionary.
 * By: Matthew Blackert
 * Class: CSE 382 Mobile Apps Development (Dr. Zmuda)
 */
namespace Project1
{
	public class EnglishDictionary
	{
		private HashSet<string> dict = new HashSet<string>();
		private Task load;
		// This constructor will initiate the loading of all words located
		// in the given dictionary. The constructor must return very quickly,
		// perhaps before the words have been completely loaded. Tasks will be
		// needed to do this.
		public EnglishDictionary(string fileName)
		{
			Action<object> action = (object obj) =>
			{
				// Open and read the file
				using (StreamReader input = new StreamReader(fileName))
				{
					while (!input.EndOfStream)
					{
						string line = input.ReadLine().ToUpper();
						dict.Add(line);
					}
				}
			};
			load = new Task(action, "readDict");
			load.Start();
		}
        // This method will return true only if the word appears in the
        // dictionary. This method will need to wait, if it is called
        // before the words have been completely loaded.
        public bool IsLegal(string word)
		{
			if (!load.IsCompleted)
            {
				load.Wait();
			}
			return dict.Contains(word.ToUpper());
		}
		// This method will return the set of all words that can be formed using a
		// collection of Scrabble tiles, that has a minimum length. Each tile can
		// be used at most once but there may be more than one tile for a given letter.
		// Each letter can only be used once. For example, consider the tiles: HERATH
		// Here are some of the words of at least length 4 that can legally be formed:
		// HEAT, EARTH, HEART, HEARTH, ...
		// Some English words cannot be formed using the tiles: REATA, ARHAT
		// Like the previous method, this method may need to wait too.
		public List<string> ScrabbleWords(string tiles, int minLength)
		{
			HashSet<string> valid = new HashSet<string>();
			// Using a list for future work where the scrabble words method will be used
			// to generate words for the game.
			List<string> final = new List<string>();
			bool[] usedLetters = new bool[tiles.Length];
			if (!load.IsCompleted)
            {
				load.Wait();
			}
            tiles = tiles.ToUpper();
			for (int i = minLength; i <= tiles.Length; i++)
            {
				RecursiveFormation(ref valid, tiles, i, "", usedLetters, true);
			}
			foreach (string word in valid)
            {
				final.Add(word);
				Console.WriteLine(word);
            }
			return final;
		}
		// This is a recursive method that allows for the formation of the
		// possible words that can be made out of the tiles.
		private void RecursiveFormation(ref HashSet<string> validWords, string tiles, int minLength, string start, bool[] usedLetters, bool validChecker)
        {
			// Form the words of the required length
			if (start.Length == minLength)
            {
				if (validChecker)
                {
					if (IsLegal(start))
						validWords.Add(start);
					return;
				}
            }
			// Create a recursive calling to grow the word to the required min length.
			else
            {
				for (int i = 0; i < usedLetters.Length; i++)
                {
					if (usedLetters[i] == false)
                    {
						// Avoid memory clashing by generating a copy
						bool[] usedLettersCopy = new bool[usedLetters.Length];
						Array.Copy(usedLetters, usedLettersCopy, usedLetters.Length);
						usedLettersCopy[i] = true;
						RecursiveFormation(ref validWords, tiles, minLength, start + tiles[i], usedLettersCopy, true);
                    }
                }
            }
        }
	}
}
