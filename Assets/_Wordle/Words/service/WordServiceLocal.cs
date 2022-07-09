using System.Net.Sockets;
using UnityEngine;

public class WordServiceLocal : WordService
{
    private WordRepositoryCacheFactory wordRepositoryCacheFactory;

    private WordServiceLocal() {
        this.wordRepositoryCacheFactory = new WordRepositoryCacheFactory(new WordRepositoryFactory());
    }
    private static WordServiceLocal instance = null;
    public static WordServiceLocal Instance {
        get {
            if (instance == null) {
                instance = new WordServiceLocal();
            }
            return instance;
        }
    }


    /**
     * Retrieve a random word and return it to the caller
     * @param numberOfLetters for the word
     * @return the Random Word
     */
    public string randomWord(int numberOfLetters) {

        WordRepository wordRepository = wordRepositoryCacheFactory.getWordRepository(numberOfLetters);

        int randomIndex = Random.Range(0 ,wordRepository.retrieveNumberOfWords() - 1);

        string word = wordRepository.retrieveWordByIndex(randomIndex);

        return word;
    }

    /**
     * Check whether a word exists
     * @param word for the word
     * @return true if the word exists, otherwise false
     */
    public bool isAWord(string word) {

        WordRepository wordRepository = wordRepositoryCacheFactory.getWordRepository(word.Length);

        return wordRepository.wordExists(word.ToLower());
    }

}
