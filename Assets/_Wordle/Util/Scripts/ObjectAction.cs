using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectAction {

    void performAction(GameObject gameObject);

    bool isRunning();

    bool isComplete();

    public void reset();
}
