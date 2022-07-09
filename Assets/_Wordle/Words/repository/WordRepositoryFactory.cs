using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordRepositoryFactory
{
    private string filePostFix = "-letter-words.txt";

    private string wordsFolder = "Assets/_Wordle/Words/resources";
   
    public WordRepository getWordRepository(int numberOfWords) {
        if (numberOfWords == 0) {
            return new StubWordRepository();
        }
        return new FileWordRepository(numberOfWords, wordsFolder, filePostFix);
    }
}
