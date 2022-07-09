using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLayout : MonoBehaviour, Layout {
    public float calculateHeight() {
        Layout layout = getFirstChildLayout();

        return layout == null ? 0 : layout.calculateHeight();
    }
    
    public float calculateWidth() {
        Layout layout = getFirstChildLayout();

        return layout == null? 0: layout.calculateWidth();
    }

    private Layout getFirstChildLayout() {
        if (transform.childCount == 0) {
            return null;
        }
        var gameObject = transform.GetChild(0);
        Layout layout = gameObject.GetComponent<Layout>();
        if (layout == null) {
            Debug.Log("No child in GuessRowLayout with Layout");
            return null;
        }
        return layout;
    }
}
