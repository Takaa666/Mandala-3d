using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class GameOver : MonoBehaviour
{
    public static bool New = false;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        New = true;
        StartCoroutine(StartGame());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator StartGame()
    {
        yield return SceneManager.LoadSceneAsync("BismillahFinal");
        yield return new WaitForEndOfFrame(); // Ensure the scene is fully loaded before proceeding

        if (SceneStateManager.Instance != null)
        {
            SceneStateManager.Instance.ClearSceneData();
            SceneStateManager.Instance.SaveSceneData();
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = new Vector3(-53.11f, -0.081f, 2.88f); // Set starting position
                SceneStateManager.Instance.SaveSceneData(); // Save this starting position
            }
        }
        else
        {
            Debug.LogError("SceneStateManager instance not found.");
        }
    }
}