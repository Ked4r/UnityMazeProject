using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerEnemy : NewBehaviourScript
{
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        // Sprawdzenie, czy dosz³o do kolizji z graczem.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Zadawanie obra¿eñ graczowi.
            base.Spawn();
        }
    }
}
