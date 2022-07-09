using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRow : MonoBehaviour
{
    private List<KeyboardKey> keyboardRowKeys = new List<KeyboardKey>();

    public KeyboardRow initialise(string[] keys, KeyboardKey keyboardKeyPrefab) {

        addKeyRow(keys, keyboardKeyPrefab);

        return this;
    }

    private void addKeyRow(string[] keys, KeyboardKey keyboardKeyPrefab) {
        for (int i = 0; i < keys.Length; i++) {
            string text = keys[i];
            KeyboardKey keyboardKey = Instantiate(keyboardKeyPrefab);
            keyboardKey.updateKeyValue(text);
            keyboardKey.transform.parent = transform;

            if (text.Equals("ENTER") || text.Equals("<<")) {
                Transform keyCube = findKeyChild(keyboardKey.gameObject);
                Vector3 scale = new Vector3(keyCube.transform.localScale.x * 3, keyCube.transform.localScale.y, keyCube.transform.localScale.z);
                keyCube.transform.localScale = scale;
            }

            keyboardRowKeys.Add(keyboardKey);
        }
    }

    private Transform findKeyChild(GameObject gameObject) {
        Transform key = null;
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            Transform child = gameObject.transform.GetChild(i);
            if (child.name == "Key") {
                key = child;
            }
        }

        return key;
    }

    public List<KeyboardKey> getKeyboardKeys() {
        return keyboardRowKeys;
    }
}
