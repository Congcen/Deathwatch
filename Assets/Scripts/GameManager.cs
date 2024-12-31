using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float vertical = 1f;
    [SerializeField] private float horizontal = 1.5f;
    [SerializeField] private float Vstart = 3.5f;
    [SerializeField] private float Hstart = -7f;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject bossPrefab; // Boss prefab
    [SerializeField] private Transform bossSpawnPoint;

    [SerializeField] private float moveH = 0.5f;
    [SerializeField] private float moveV = 0.25f;
    [SerializeField] private float moveDelay = 1f;

    private bool collided = false;
    private bool bossFight = false; // Check if boss fight is active
    private GameObject boss; // Boss instance
    private int bossHealth = 5; // Boss hit points

    void Start()
    {
        spawnEnemies();
        InvokeRepeating("enemyMove", 1f, 1f);

        // Continuously check for remaining enemies
        StartCoroutine(CheckForRemainingEnemies());
    }

    private void spawnEnemies()
    {
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                Instantiate(enemy, new Vector3(Hstart + (j * horizontal), Vstart - (i * vertical), 0), Quaternion.identity);
            }
        }
    }

    private void enemyMove()
    {
        if (bossFight) return; // Stop enemy movement during the boss fight

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Vector3 currentPos = enemy.transform.position;
            enemy.transform.position = currentPos + new Vector3(moveH, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !collided)
        {
            collided = true;
            Invoke("resetHasCollided", 2f);
            moveH *= -1;
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Vector3 currentPos = enemy.transform.position;
                enemy.transform.position = currentPos - new Vector3(0, moveV, 0);
            }
        }
    }

    private void resetHasCollided()
    {
        collided = false;
    }

    private IEnumerator CheckForRemainingEnemies()
    {
        while (!bossFight)
        {
            yield return new WaitForSeconds(1f);

            // Check if there are no enemies left
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                StartBossFight();
                yield break;
            }
        }
    }

    private void StartBossFight()
    {
        bossFight = true;
        CancelInvoke("enemyMove");

        // Spawn the boss
        boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

        // Trigger boss attack coroutine
        StartCoroutine(BossAttack());
    }

    private IEnumerator BossAttack()
    {
        while (boss != null)
        {
            // Example attack: Shoot projectiles
            yield return new WaitForSeconds(2f);
            if (boss != null)
            {
                boss.GetComponent<Boss>().ShootProjectile(); // Boss script handles its shooting
            }
        }
    }

    public void HitBoss()
    {
        if (boss == null) return;

        bossHealth--;
        if (bossHealth <= 0)
        {
            Destroy(boss);
            Debug.Log("Boss Defeated!");
            // Add victory logic here
        }
    }
}
