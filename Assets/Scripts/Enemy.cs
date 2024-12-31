using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyProjectile;

    private void Start()
    {
        InvokeRepeating("ShouldShoot", Random.Range(1f, 5f), 1f);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
    }

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f, 0), Vector2.down);
        if (hit.collider == null || hit.transform.tag != "Enemy")
        {
            Debug.Log("Raycast nulled");
            Instantiate(enemyProjectile, transform.position - new Vector3(0, 0.4f, 0), Quaternion.identity);
        }
    }

    private void ShouldShoot()
    {
        if (Random.Range(0, 10) >= 9)
        {
            Shoot();
        }

    }
}

