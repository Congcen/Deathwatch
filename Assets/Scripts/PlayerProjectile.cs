using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    private void Start()
    {
        Invoke("AddCollider", 0.2f);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.score += 10;
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    private void AddCollider()
    {
        this.AddComponent<CapsuleCollider2D>();
    }
}