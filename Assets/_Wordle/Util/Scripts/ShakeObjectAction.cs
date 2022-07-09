using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObjectAction : ObjectActionBase {
 
    [SerializeField]
    private float shakeSpeed =10;
    [SerializeField]
    private float duration = 1;

    protected override void performActionExecution(GameObject gameObject) {
        StartCoroutine(shakeObject(gameObject, duration, gameObject.transform.position));
    }


    IEnumerator shakeObject(GameObject gameObject, float duration, Vector3 originalPosition, float currentDuration = 0) {
        running = true;
        while (currentDuration < duration) {
            gameObject.transform.position = 
                //Vector3.MoveTowards(gameObject.transform.position, originalPosition + Random.insideUnitSphere, step);
             new Vector3(Mathf.PingPong(Time.time / 2, 0.12f), gameObject.transform.position.y, gameObject.transform.position.z);
            Debug.Log("Shaking");
            currentDuration += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = originalPosition;
        running = false;
        complete = true;
    }

}
