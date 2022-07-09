using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterGuess : MonoBehaviour {
    private string character;

    private Color colour;

    [SerializeField]
    private TextMeshPro frontText;

    [SerializeField]
    private TextMeshPro backText;

    [SerializeField]
    private MeshRenderer meshRenderer;

    public void updateCharacter(string character) {
        this.character = character;
        frontText.SetText(character.ToString());
        backText.SetText(character.ToString());
    }

    public string getCharacter() {
        return character;
    }


    public void setColour(Color colour) {
        this.colour = colour;
    }

    public Color getColour() {
        return colour;
    }

    public void applyMaterialColour() {
        meshRenderer.material.color = colour;
    }
}
