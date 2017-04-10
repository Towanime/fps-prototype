using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxAnimationListener : MonoBehaviour {

    public AnimationsManager animationsManager;

	void OnIntroAnimationFinished ()
    {
        animationsManager.introFinished = true;
    }
}
