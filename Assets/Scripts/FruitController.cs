using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour {

    /**
     * Destroy itself is it is spawned inside a snake
     * - Will not spawn at the same location as the SnakeHead as that is guarded by the FruitSpawner
     */
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.name.Equals("SnakeHead"))
        {
            Destroy(gameObject);
        }
    }

}
