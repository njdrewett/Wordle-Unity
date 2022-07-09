using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessBoard : MonoSingleton<GuessBoard> {
    // Start is called before the first frame update
    private List<GuessRow> guessRows;

    [SerializeField]
    private LetterGuess letterGuessPrefab;

    [SerializeField]
    private GuessRow guessRowPrefab;

    private int guessRowIndex = 0;

    void Start() {

    }

    public void resetGame() {

        guessRowIndex = 0;

        foreach(GuessRow row in guessRows) {
            row.reset();
        }
    }

    public bool hasMoreGuessRows() {
        return guessRowIndex < guessRows.Count-1;
    }
    public int numberOfGuesses() {
        return guessRowIndex+1;
    }
    public bool canAddGuessLetter() {
        return guessRows[guessRowIndex].canAddGuess();
    }

    public bool allGuessForRowAdded() {
        return guessRows[guessRowIndex].allGuessesAdded();
    }
 
    public GuessRow retrieveCurrentGuessRow() {
        return guessRows[guessRowIndex];
    }

    public void applyLetterToGuessRow(string keyValue) {
        if (canAddGuessLetter()) {
            guessRows[guessRowIndex].addGuess(keyValue[0].ToString());
        }
    }

    public string retrieveGuess() {
        //// check that we have all required characters
        if (!allGuessForRowAdded()) {
            return null;
        }

        return guessRows[guessRowIndex].retrieveGuess();
    }

    public void deletePreviousGuessLetter() {
        GuessRow letterGuessRow = guessRows[guessRowIndex];
        letterGuessRow.deleteLastGuess();
    }


    public void moveToNextRow() {
        guessRowIndex++;
    }

    public void initialiseBoard(int numberOfGuesses, int wordLength) {

        guessRows = new List<GuessRow>();
        for (int i = 0; i < numberOfGuesses; ++i) {
            initialiseGuessRow(wordLength);
        }
    }

    public void initialiseGuessRow(int wordLength) {
        GuessRow guessRow = Instantiate(guessRowPrefab).initialise(wordLength, letterGuessPrefab);
        guessRow.transform.parent = transform;
        guessRows.Add(guessRow);
    }
}
