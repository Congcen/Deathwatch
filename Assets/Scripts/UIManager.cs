using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI gameOverText;

    [SerializeField] private PlayerController player;

    private void Update()
    {
        livesText.text = $"Lives : {player.lives}";
        ScoreText.text = $"Score: {player.score}";

        if (player.lives == 0)
        {
            livesText.enabled = false;
            ScoreText.enabled = false;
            gameOverText.text = "Game Over !";
        }
    }
}
