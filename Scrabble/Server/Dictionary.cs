using System;
using System.Collections.Generic;
using System.Linq;


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
        private List<String> dictionary;

        public Dictionary()
        {
            string resource_data = Properties.Resources.myDictionary;
            dictionary = resource_data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        public bool containsWord(String word)
        {
            word = word.ToLower();
            if (dictionary.Contains(word)) { return true; }
            else { return false; }
        }
    }
}
