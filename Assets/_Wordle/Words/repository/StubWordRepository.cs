using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StubWordRepository : WordRepository {

    private List<string> words;

    public StubWordRepository() {
        words = new List<string>();
        initialiseStore();
    }

    public void initialiseStore() {
        words.Add("words");
        words.Add("abbey");
        words.Add("slept");
        words.Add("drawn");
        words.Add("petty");
        words.Add("solar");
    }

    public string retrieveWordByIndex(int index) {
        Debug.Log("retrieveWordByIndex " + index);

        string value = words[index];

        Debug.Log("retrieveWordByIndex Return " + value);

        return value;
    }


    public bool wordExists(string word) {
        Debug.Log("wordExists " + word);

        bool wordExists = words.Contains(word);

        Debug.Log("wordExists " + wordExists);

        return wordExists;
    }

    public List<string> retrieveAllWords() {
        return words;
    }

    public int retrieveNumberOfWords() {
        Debug.Log("retrieveNumberOfWords ");

        int numberOfWords = retrieveAllWords().Count;

        Debug.Log("retrieveNumberOfWords Result " + numberOfWords);

        return numberOfWords;
    }

}
