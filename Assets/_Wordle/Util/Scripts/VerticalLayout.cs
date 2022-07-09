using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class VerticalLayout : MonoBehaviour {
    
    [SerializeField]
    private float padding = 0f;

    public void Update() {

        // Determine height
        float height = calculateChildHeight();

        layoutVerticalChildren(height);
    }

    private void layoutVerticalChildren(float height) {
        float centrePoint = height / 2;

        float offset = 0f;
        // for each. Set position (centrepoint) of each object
        for (int i = 0; i < this.transform.childCount; i++) {
            var gameObject = transform.GetChild(i);
            if (gameObject != null) {
                Layout layout = gameObject.GetComponent<Layout>();
                if (layout != null) {
                    float meshHeight = layout.calculateHeight() + padding;
                    float meshCentrePoint = (offset + (meshHeight / 2) - centrePoint ) * -1;
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, meshCentrePoint, gameObject.transform.localPosition.z);
                    offset += meshHeight;
                }
            }
        }
    }

    private float calculateChildHeight() {
        float height = 0f;
        for (int i = 0; i < transform.childCount; i++) {
            var gameObject = transform.GetChild(i);
            if (gameObject != null) {
                Layout layout = gameObject.GetComponent<Layout>();
                if (layout != null) {
                    height += layout.calculateHeight() + padding;
                } else {
                    Debug.Log("No mesh child");
                }
            } else {
                Debug.Log("No child");
            }
        }

        return height;
    }
}
