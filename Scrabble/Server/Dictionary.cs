using System;
using System.Collections.Generic;
using System.IO;


namespace Scrabble.Server
{
    /// <summary>
    /// Reads a text file containing the dictionary into a list. Prints error 
    /// message to console if dictionary is not found. Has method 
    /// containsWord(string word) that checks if a given word is in the 
    /// dictionary. 
    /// </summary>
    public class Dictionary
    {
        private List<String> dictionary = new List<String>();

        public Dictionary(String filename)
        {
            StringReader reader = new StringReader(filename);
            string line;
            while((line = reader.ReadLine()) != null)
            {
                dictionary.Add(line);
            }
        }

        public bool containsWord(String word)
        {
            word = word.ToLower();
            if (dictionary.Contains(word)) { return true; }
            else { return false; }
        }

        public bool ContainsWordBinSearch(string word)
        {
            word = word.ToLower();
            return dictionary.BinarySearch(word) >= 0;
        }
    }
}
