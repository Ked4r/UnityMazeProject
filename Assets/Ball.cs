using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    private float speed = 7f; // Szybko��, z jak� pi�ka b�dzie si� porusza�.
    private GameObject player; // Referencja do obiektu gracza.

    // Mo�na doda� warto�� obra�e�, jakie pi�ka ma zadawa� graczowi.
    public int damage = 10;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player"); // Znajdowanie obiektu gracza po tagu.
    }

    void FixedUpdate()
    {
        // Poruszanie si� w stron� gracza.
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            _rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Sprawdzenie, czy dosz�o do kolizji z graczem.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Zadawanie obra�e� graczowi.
            collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(damage);
        }
    }
}
