using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachedWordRepository : WordRepository {

    private List<string> words;

    public CachedWordRepository(WordRepository wordRepository) {
        words = wordRepository.retrieveAllWords();  
    }


    public List<string> retrieveAllWords() {
        return words;
    }

    public int retrieveNumberOfWords() {
        return words.Count;

    }

    public string retrieveWordByIndex(int index) {
        return words[index];
    }

    public bool wordExists(string word) {
        bool wordExists = words.Contains(word);
        return wordExists;
    }
}
