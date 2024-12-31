using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float moveSpeed = 2f; // Speed for left-and-right movement
    [SerializeField] float moveRange = 6f; // Range for left-and-right movement

    private Vector3 initialPosition;
    private bool movingRight = true;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float movement = moveSpeed * Time.deltaTime;
        if (movingRight)
        {
            transform.Translate(movement, 0, 0);
            if (transform.position.x >= initialPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(-movement, 0, 0);
            if (transform.position.x <= initialPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }

    public void ShootProjectile()
    {
        // Shoot center bullet
        InstantiateProjectile(0);

        // Shoot angled bullets
        InstantiateProjectile(30); // Left angled bullet
        InstantiateProjectile(-30); // Right angled bullet
    }

    private void InstantiateProjectile(float angle)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.down; // Adjust angle relative to downward
        rb.velocity = direction * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Destroy(collision.gameObject);
            FindObjectOfType<GameManager>().HitBoss();
        }
    }
}
