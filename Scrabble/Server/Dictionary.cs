//-----------------------------------------------------------------------
// <copyright file="Dictionary.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Reads a text file containing the dictionary into a list. Prints error 
    /// message to console if dictionary is not found. Has method 
    /// containsWord(string word) that checks if a given word is in the 
    /// dictionary. 
    /// </summary>
    public class Dictionary
    {
        /// <summary>
        /// The (really long) list of words that win challenges.
        /// </summary>
        private List<string> dictionary = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Dictionary"/> class.
        /// </summary>
        /// <param name="words">The words that are being put into the <see cref="Dictionary"/>.</param>
        public Dictionary(string words)
        {
            StringReader reader = new StringReader(words);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                this.dictionary.Add(line);
            }
        }

        /// <summary>
        /// Returns true if a given word is in the <see cref="Dictionary"/>. Returns false otherwise.
        /// </summary>
        /// <param name="word">The word being checked.</param>
        /// <returns>Whether word is in the dictionary.</returns>
        public bool ContainsWord(string word)
        {
            word = word.ToLower();
            if (this.dictionary.Contains(word))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if a given word is in the <see cref="Dictionary"/>. Returns false otherwise
        /// </summary>
        /// <param name="word">The word being checked.</param>
        /// <returns>Whether word is in the dictionary.</returns>
        public bool ContainsWordBinSearch(string word)
        {
            word = word.ToLower();
            return this.dictionary.BinarySearch(word) >= 0;
        }
    }
}
