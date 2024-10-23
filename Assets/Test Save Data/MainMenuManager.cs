using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    public static bool Load = false;
    public GameObject optionPanel;
    public Button optionButton;

    private void Start()
    {
        optionButton = GameObject.Find("Option Main Menu").GetComponent<Button>();
        optionButton.onClick.AddListener(ShowOptionPanel);

    }

    private void Update()
    {
        if (optionPanel == null)
        {
            optionPanel = GameObject.Find("Option Menu");
        }
    }

    public void ShowOptionPanel()
    {
        SetActivePanel(optionPanel);
        
    }
    private void SetActivePanel(GameObject activePanel)
    {
        
        optionPanel.SetActive(activePanel == optionPanel);
    }

    public void Continue()
    {
        Load = true;

        StartCoroutine(SceneContinue());
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Mandala Awal Jeje");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("BismillahFinal");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    private IEnumerator SceneContinue()
    {
        yield return SceneManager.LoadSceneAsync("BismillahFinal");
        yield return new WaitForEndOfFrame(); // Ensure the scene is fully loaded before proceeding

        // Ensure the player is instantiated before applying the loaded position
        if (SceneStateManager.Instance != null)
        {
            SceneStateManager.Instance.LoadContinue();
            
        }
        else
        {
            Debug.LogError("SceneStateManager instance not found.");
        }
    }


    
}
