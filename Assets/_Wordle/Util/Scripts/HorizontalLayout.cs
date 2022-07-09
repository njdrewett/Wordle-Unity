using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HorizontalLayout : MonoBehaviour {
    
    [SerializeField]
    private float padding = 0f;

//    int numberOfChildren = 0;

    public void Update() {


        // if not dirty
 //       if (numberOfChildren == 0 || transform.childCount == numberOfChildren) {
//            return;
//        }
        // Determine width
        float width = calculateHorizontalWidth();
        
        layoutChildren(width);
    }

    private void layoutChildren(float width) {
        float centrePoint = width / 2;
        float offset = 0f;
        // for each. Set position (centrepoint) of each object
        for (int i = 0; i < transform.childCount; i++) {
            var gameObject = transform.GetChild(i);
            if (gameObject != null) {
                Layout layout = gameObject.GetComponent<Layout>();
                if (layout != null) {
                    float meshWidth = layout.calculateWidth() + padding;
                    float meshCentrePoint = offset + (meshWidth / 2) - centrePoint;
                    gameObject.transform.localPosition = new Vector3(meshCentrePoint, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
                    offset += meshWidth;
                }
            }
        }
    }

    private float calculateHorizontalWidth() {
        float width = 0f;
        for (int i = 0; i < transform.childCount; i++) {
            var gameObject = transform.GetChild(i);
            if (gameObject != null) {
                Layout mesh = gameObject.GetComponent<Layout>();
                width += mesh.calculateWidth() + padding;
            }
        }

        return width;
    }
}
