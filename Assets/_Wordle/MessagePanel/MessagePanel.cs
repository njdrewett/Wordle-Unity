using UnityEngine;

public class MessagePanel :MonoBehaviour {

    [SerializeField]
    public TMPro.TextMeshProUGUI messageText;

    public void displayMessagePopup(string text) {
        Debug.Log("Display message " + text);
        messageText.SetText(text);
        gameObject.SetActive(true);
    }

    public void hideMessagePopup() {
        gameObject.SetActive(false);
    }

}
