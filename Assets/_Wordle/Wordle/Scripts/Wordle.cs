using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wordle : MonoSingleton<Wordle> {
    [SerializeField]
    private Keyboard keyboard;

    [SerializeField]
    private GuessBoard guessBoard;


    private WordService wordService;

    [SerializeField]
    private int wordLength = 5;

    [SerializeField]
    private int numberOfGuesses;

    private string randomWord;
    private bool gameOver;

    [SerializeField]
    public MessagePanel messagePanel;

    [SerializeField]
    public GameObject statisticPanel;

    public StatisticsPanel statisticsPanel;

    public event Action resetBoard;

    [SerializeField]
    private Color wrongGuessColor;
    [SerializeField]
    private Color correctGuessColor;
    [SerializeField]
    private Color presentInWordGuessColor;

    [SerializeField]
    private GameObject notAWordObjectAction;
    private ObjectAction notAWordInstantiated;

    private List<string> correctCharacters = new List<string>();
    private List<string> wrongPlaceCharacters = new List<string>();

    void Start() {

        wordService = WordServiceLocal.Instance;

        Keyboard.Instance.onKeyPressed += OnKeyPressed;

        notAWordInstantiated = Instantiate(notAWordObjectAction).GetComponent<ObjectAction>();

        initialiseGame();
    }


    private void initialiseGame() {
        retrieveRandomWord();

        guessBoard.initialiseBoard(numberOfGuesses, wordLength);
    }

    public void resetGame() {

        gameOver = false;

        retrieveRandomWord();
        keyboard.resetKeyboard();
        guessBoard.resetGame();

        statisticPanel.SetActive(false);
        messagePanel.hideMessagePopup();
    }

    private void retrieveRandomWord() {
        randomWord = wordService.randomWord(wordLength);

        Debug.Log("Random word is :" + randomWord);
    }

    public void OnKeyPressed(KeyboardKey keyboardKey) {
        Debug.Log("On key pressed");
        if (gameOver) {
            return;
        }

        GuessRow currentGuessRow = guessBoard.retrieveCurrentGuessRow();
        if (currentGuessRow.isFlipping()) {
            return;
        }

        string keyboardKeyValue = keyboardKey.keyValue;

        if ("<<".Equals(keyboardKeyValue)) {
            // Delete Key
            guessBoard.deletePreviousGuessLetter();
            return;
        }

        if ("ENTER".Equals(keyboardKeyValue)) {
            submitGuessRow();
            return;
        }

        guessBoard.applyLetterToGuessRow(keyboardKeyValue);
    }

    private void submitGuessRow() {

        messagePanel.hideMessagePopup();

        if (!guessBoard.allGuessForRowAdded()) {
            return;
        }

        string guessWord = guessBoard.retrieveGuess();
        if (guessWord == null) {
            return;
        }

        bool isAWord = wordService.isAWord(guessWord);

        Debug.Log("Is A Word : " + isAWord);

        if (!isAWord) {
            messagePanel.displayMessagePopup("Not A Word");
            notAWordAction();
            return;
        }

        flipGuessCubes();

        waitForCurrentRow(guessWord);
    }

    private void checkForEndGame(string guessWord) {

        if (guessBoard.hasMoreGuessRows() && !guessWord.ToLower().Equals(randomWord)) {
            guessBoard.moveToNextRow();
            return;
        }
        
        if (guessWord.ToLower().Equals(randomWord)) {
            Debug.Log("Correct!");
            PlayerStatistics.Instance.recordWin(guessBoard.numberOfGuesses());
        } else {
            Debug.Log("Wrong. No more guesses");
            PlayerStatistics.Instance.recordLose();
        }

        gameOver = true;
        messagePanel.displayMessagePopup("Game Over!");
        statisticsPanel.refreshStatistics();

        statisticPanel.SetActive(true);        
    }

    public void waitForCurrentRow(string guessWord) {
        StartCoroutine(waitForFlipRow(guessWord));
    }

    public IEnumerator waitForFlipRow(string guessWord) {
        GuessRow currentGuessRow = guessBoard.retrieveCurrentGuessRow();
        while (currentGuessRow.isFlipping()) {
            yield return null;
        }
        checkForEndGame(guessWord);
    }

    private void notAWordAction() {

        GameObject currentGuessRow = guessBoard.retrieveCurrentGuessRow().gameObject;
        notAWordInstantiated.performAction(currentGuessRow);        
    }

    private void flipGuessCubes() {

        GuessRow currentGuessRow = guessBoard.retrieveCurrentGuessRow();
        List<LetterGuess> letterGuessRow = currentGuessRow.getGuesses();

        string checkWord = randomWord.ToLower();

        List<LetterGuess> updatedLetters = new List<LetterGuess>();

        // All are wrong until guessed
        for (int i = 0; i < letterGuessRow.Count; i++) {
            LetterGuess letterGuess = letterGuessRow[i];
            letterGuess.setColour(wrongGuessColor);
        }

        // if correct position make correct
        for (int i = 0; i < letterGuessRow.Count; i++) {
            LetterGuess letterGuess = letterGuessRow[i];
            string lowerCharacter = letterGuess.getCharacter().ToLower();
            Debug.Log("Lower character is" + lowerCharacter);
            Debug.Log("Comparing to : " + checkWord[i].ToString());
            if (lowerCharacter.Equals(checkWord[i].ToString())) {
                letterGuess.setColour(correctGuessColor);
                Debug.Log("Correct guess color");
                // Remove if already in wrong place characters

                if (wrongPlaceCharacters.Contains(letterGuess.getCharacter())) {
                    wrongPlaceCharacters.Remove(letterGuess.getCharacter());
                }
                // and add to correct characters
                if (!correctCharacters.Contains(letterGuess.getCharacter())) {
                    correctCharacters.Add(letterGuess.getCharacter());
                    updatedLetters.Add(letterGuess);
                }

                // remove from checkword to remove duplicates
                checkWord.Remove(i, 1);
            }
        }

        // if not correct position make wrong place
        for (int i = 0; i < letterGuessRow.Count; i++) {
            LetterGuess letterGuess = letterGuessRow[i];
            if ((checkWord.IndexOfAny(letterGuess.getCharacter().ToLower().ToCharArray()) > -1)
                && !letterGuess.getColour().Equals(correctGuessColor)) {
                letterGuess.setColour(presentInWordGuessColor);
                Debug.Log("Present in word");

                if (!correctCharacters.Contains(letterGuess.getCharacter()) &&
                    !wrongPlaceCharacters.Contains(letterGuess.getCharacter())) {
                    updatedLetters.Add(letterGuess);
                    wrongPlaceCharacters.Add(letterGuess.getCharacter());
                }

                // remove from checkword to remove duplicates
                checkWord.Remove(i, 1);
            }
        }

        // Check for wrong colour update
        for (int i = 0; i < letterGuessRow.Count; i++) {
            LetterGuess letterGuess = letterGuessRow[i];
            if (!correctCharacters.Contains(letterGuess.getCharacter()) &&
                 !wrongPlaceCharacters.Contains(letterGuess.getCharacter())) {
                updatedLetters.Add(letterGuess);
            }
        }

        currentGuessRow.flipCurrentRow();

        for (int i = 0; i < updatedLetters.Count; i++) {
            LetterGuess letterGuess = updatedLetters[i];
            keyboard.updateKeyColor(letterGuess.getCharacter(), letterGuess.getColour());
        }
    }
}
