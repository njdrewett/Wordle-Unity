using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileWordRepository : WordRepository {

    private readonly string wordsFolder;

    private readonly string filePostFix;

    private readonly int numberOfLetters;

    public FileWordRepository(int numberOfLetters, string wordsFolder, string filePostFix) {
        this.wordsFolder = wordsFolder;
        this.filePostFix = filePostFix;
        this.numberOfLetters = numberOfLetters;
    }

    public List<string> retrieveAllWords() {
        return readWordsFromFile();
    }

    private List<string> readWordsFromFile() {
        string wordsFile = concatFilePathAndName();

        List<string> words = new List<string>();

        try {

            StreamReader reader = new StreamReader(wordsFile);

            while (reader.Peek() >= 0) {
                words.Add(reader.ReadLine());
            }
        } catch (Exception exception) {
            throw new WordRepositoryException("Exception from reading file: "+ wordsFile, exception);
        }

        return words;
    }

    private string concatFilePathAndName() {
        string filePath = "";
        if (wordsFolder != null && (wordsFolder.Trim().Length !=0)) {
            filePath += wordsFolder;
            filePath += Path.DirectorySeparatorChar;
        }
        filePath += numberOfLetters;
        filePath += filePostFix;
        return filePath;
    }

    public int retrieveNumberOfWords() {
        Debug.Log("ENTER RetrieveNumberOfWords");

        int numberOfWords = retrieveAllWords().Count;

        Debug.Log("Exiting RetrieveNumberOfWords" + numberOfWords);

        return numberOfWords;
    }

    public string retrieveWordByIndex(int index) {
        Debug.Log("RetrieveWordByIndex " + index);

        string word = retrieveAllWords()[index];

        Debug.Log("Returned Word " + word);

        return word;

    }

    public bool wordExists(string word) {

        Debug.Log("wordExists " + word);

        bool wordExists = retrieveAllWords().Contains(word);

        Debug.Log("Word exists " + wordExists);

        return wordExists;
    }
}
