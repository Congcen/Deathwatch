using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject playerProjectile;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;
    public int lives = 3;
    public int score = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown("space"))
        {
            shoot();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void shoot()
    {
        if (GameObject.FindGameObjectsWithTag("PlayerProjectile").Length == 0)
        {
            Instantiate(playerProjectile, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lives -= 1;
        if (lives == 0)
        { 

        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        foreach (var bullet in FindObjectsOfType<EnemyProjectile>())
        {
            Destroy(bullet.gameObject);
        }
        transform.position = new Vector3(0, -4.5f, 0);
    }
}
