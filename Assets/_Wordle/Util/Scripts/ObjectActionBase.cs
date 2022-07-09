using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectActionBase : MonoBehaviour, ObjectAction
{
    protected bool running = false;
    protected bool complete = false;

    public bool isComplete() {
        return complete;
    }

    public void reset() {
       running = false;
        complete = false;
    }

    public bool isRunning() {
        return running;
    }

    public void performAction(GameObject gameObject) {
        if (running) {
            return;
        }
        complete = false;
        performActionExecution(gameObject);
    }

    protected abstract void performActionExecution(GameObject gameObject);
}
