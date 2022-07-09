using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WordRepository
{
    /**
     * Retrieve a Word by line number
     */
    string retrieveWordByIndex(int index);

    /**
     * Returns True if Word exists
      */
    bool wordExists(string word);

    /**
     * Return all words as a List of strings.
     * @return
     */
    List<string> retrieveAllWords();

    /**
     * Get max number of words
     */
    int retrieveNumberOfWords();

}
