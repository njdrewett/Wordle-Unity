using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObjectAction : ObjectActionBase
{
    Vector3 current ;
    Vector3 target ;

    override protected void performActionExecution(GameObject gameObject) {
         current = gameObject.transform.rotation.eulerAngles;
         target = new Vector3(current.x + 180, current.y, current.z);

        StartCoroutine(flipObject(gameObject));
    }

    private IEnumerator flipObject(GameObject gameObject) {
        running = true;

        while (gameObject.transform.rotation.x < 1) {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(target) , 400f * Time.deltaTime);
            yield return null;
        }
        LetterGuess letterguess = gameObject.GetComponent<LetterGuess>();
        if (letterguess != null) {
            letterguess.applyMaterialColour();
        }
        complete = true;
        running = false;        
    }

}
