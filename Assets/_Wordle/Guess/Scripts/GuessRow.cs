using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessRow : MonoBehaviour
{

    private List<LetterGuess> guesses = new List<LetterGuess>();

    private int guessIndex = 0;

    private bool flipping = false;

    private int flipCubeIndex = 0;

    [SerializeField]
    private List<GameObject> guessObjectActionPrefabs;
    private List<ObjectAction> guessObjectActions = new List<ObjectAction>();

    public bool isFlipping() {
        return flipping;
    }

    public List<LetterGuess> getGuesses() {
        return guesses;
    }

    public GuessRow initialise( int numberOfCharacters, LetterGuess letterGuessPrefab) {

        addGuessesCubes(numberOfCharacters, letterGuessPrefab);

        foreach (GameObject guessObject in guessObjectActionPrefabs) {
            guessObjectActions.Add(Instantiate(guessObject).GetComponent<ObjectAction>());
        }

        return this;
    }

    private void addGuessesCubes( int numberOfCharacters, LetterGuess letterGuessPrefab) {

        for (int i = 0; i < numberOfCharacters; i++) {
            LetterGuess guessCube = Instantiate(letterGuessPrefab);
            guessCube.transform.parent = transform;
            guesses.Add(guessCube);
        }

    }

    public bool allGuessesAdded() {
        return guessIndex == guesses.Count;
    }

    public bool canAddGuess() {
        return guessIndex < guesses.Count;
    }

    public void addGuess(string guess) {
        if (canAddGuess()) {
            guesses[guessIndex].updateCharacter(guess);
            guessIndex++;
        }
    }

    public void reset() {
        foreach (LetterGuess letterGuess in guesses) {
            updateGuess(letterGuess, Color.white, "");
            letterGuess.transform.SetPositionAndRotation(letterGuess.transform.position, Quaternion.Euler(0, 0, 0));
        }
        guessIndex = 0;
        flipping = false;
        flipCubeIndex = 0;

        foreach (ObjectAction objectActions in guessObjectActions) {
            objectActions.reset();
        }

    }

    public void updateGuess(LetterGuess letterGuess, Color colour, string text) {
        letterGuess.setColour(colour);
        letterGuess.updateCharacter(text);
        letterGuess.applyMaterialColour();
    }

    public void deleteLastGuess() {
        if (guessIndex > 0) {
            guessIndex --;
            LetterGuess letterGuess = guesses[guessIndex];
            letterGuess.updateCharacter("");
        }
    }

    public string retrieveGuess() {
        return joinLetters();
    }

    private string joinLetters() {
        string guessWord = "";
        foreach (LetterGuess letterGuess in guesses) {
            guessWord = guessWord + letterGuess.getCharacter();
        }

        return guessWord;
    }

    public void flipCurrentRow() {
        StartCoroutine(FlipRow());
    }

    public IEnumerator FlipRow() {
        flipping = true;
        flipCubeIndex = 0;
        while (flipCubeIndex < guesses.Count) {
            if (guessObjectActions[0].isRunning()) {
                yield return null;
            }

            if (guessObjectActions[0].isComplete()) {
                flipCubeIndex++;                
            }

            if (!guessObjectActions[0].isRunning() && flipCubeIndex < guesses.Count) {
                LetterGuess letterGuess = guesses[flipCubeIndex];
                guessObjectActions[0].performAction(letterGuess.gameObject);
            }
         }
        flipping = false;
    }
}
