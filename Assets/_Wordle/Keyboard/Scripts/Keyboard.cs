using System;
using System.Collections.Generic;
using UnityEngine;



public class Keyboard : MonoSingleton<Keyboard> {

    private string[] keysTop = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" };

    private string[] keysMiddle = { "A", "S", "D", "F", "G", "H", "J", "K", "L" };

    private string[] keysBottom = { "ENTER", "Z", "X", "C", "V", "B", "N", "M", "<<" };

    private Dictionary<string, KeyboardKey> keyMap = new Dictionary<string, KeyboardKey>(28);

    [SerializeField]
    private KeyboardKey keyPrefab;

    [SerializeField]
    private KeyboardRow keyboardRowPrefab;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Material defaultMaterial;

    public event Action<KeyboardKey> onKeyPressed;

    // Start is called before the first frame update
    void Start() {
        initialiseBoard();
    }

    public void resetKeyboard() {
        foreach (var key in keyMap.Values) {
            key.updateKeyColor(defaultMaterial.color);        
        }
    }

    public void updateKeyColor(string key, Color colour) {
        KeyboardKey keyboardKey = keyMap[key];
        keyboardKey.updateKeyColor(colour);
    }

    public void initialiseBoard() {
        initialiseRow(keysTop);
        initialiseRow(keysMiddle);
        initialiseRow(keysBottom);
    }

    private void initialiseRow( string[] keys) {
        KeyboardRow keyboardRow = Instantiate(keyboardRowPrefab).initialise(keys, keyPrefab);
        keyboardRow.transform.parent = transform;
        foreach (KeyboardKey key in keyboardRow.getKeyboardKeys()) {
          keyMap.Add(key.keyValue, key);
        }
    }

    // Update is called once per frame
    void Update() {

        // If Key is clicked, fire event
        KeyboardKey keyboardKey = checkMouseButtonClick();

        if (keyboardKey == null) {
            keyboardKey = checkKeyboardInput();
        }
        
        if (keyboardKey != null) {
            onKeyPressed?.Invoke(keyboardKey);
        }
    }

    private KeyboardKey checkMouseButtonClick() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                // Hit Key?
                if (hit.transform.CompareTag("KeyboardKey")) {
                    // Hit a key
                    return hit.transform.GetComponentInParent<KeyboardKey>();

                }
            }
        }
        return null;
    }

    private KeyboardKey checkKeyboardInput() {
        // If actual keyboard key is pressed by user
        if (Input.anyKeyDown) {
            return retrieveKeyboardKeyPressed();
        }
        return null;
    }

    private KeyboardKey retrieveKeyboardKeyPressed() {
         if (Input.GetKeyDown(KeyCode.Return)) {
            return keyMap["ENTER"];
         }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            return keyMap["<<"];
        }

        foreach (KeyValuePair<string, KeyboardKey> dictionaryKey in keyMap) {
            if (!dictionaryKey.Key.Equals("ENTER") && !dictionaryKey.Key.Equals("<<")) {
                if (Input.GetKeyDown(dictionaryKey.Key.ToLower())) {
                    return dictionaryKey.Value;
                }
            }
        }

        return null;
    }
}
