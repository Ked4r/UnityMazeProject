using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    private float speed = 7f; // Szybkoœæ, z jak¹ pi³ka bêdzie siê poruszaæ.
    private GameObject player; // Referencja do obiektu gracza.

    // Mo¿na dodaæ wartoœæ obra¿eñ, jakie pi³ka ma zadawaæ graczowi.
    public int damage = 10;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player"); // Znajdowanie obiektu gracza po tagu.
    }

    void FixedUpdate()
    {
        // Poruszanie siê w stronê gracza.
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            _rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Sprawdzenie, czy dosz³o do kolizji z graczem.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Zadawanie obra¿eñ graczowi.
            collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(damage);
        }
    }
}
