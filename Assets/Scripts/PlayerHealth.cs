using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    private Move movementPlayer;
    public GameObject GameOverScreen;

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        movementPlayer = FindObjectOfType<Move>();
        movementPlayer.enabled = false;
        GameOverScreen.SetActive(true);

        // Implement death logic, e.g., restart the game or show a game over screen
    }
}
