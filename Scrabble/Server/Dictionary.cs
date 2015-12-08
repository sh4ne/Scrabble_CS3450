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
            try
            {
                using (StreamReader fileIn = new StreamReader(filename))
                {
                    string line;
                    while ((line = fileIn.ReadLine()) != null)
                    {
                        dictionary.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Dictionary file could not be opened.");
                Console.WriteLine(e.Message);
            }
        }
        public bool containsWord(String word)
        {
            word = word.ToLower();
            if (dictionary.Contains(word)) { return true; }
            else { return false; }
        }
    }
}
