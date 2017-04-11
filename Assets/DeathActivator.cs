using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathActivator : BaseActivator {

    public override void Activate(GameObject trigger)
    {
        Destroy(gameObject);
    }
}
