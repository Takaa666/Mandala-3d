using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject graphicPanel;
    public GameObject audioPanel;
    public GameObject optionPanel;

    [Header("Buttons")]
    public Button graphicButton;
    public Button audioButton;
    public Button lowButton;
    public Button mediumButton;
    public Button highButton;
    public Button backButton;
    public Button returnToMainMenuButton;
    public Button exitButton;
    public Button resumeButton;
    public Button optionButton;
    public Button backButtonOnGraphicPanel;
    public Button backButtonOnAudioPanel;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixer mixer;

    [Header("Audio")]
    public AudioSource buttonClickSFX; // AudioSource for button click SFX

    private bool isPaused = false;
    private Move playerMovement; // Reference to the player's input script

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize buttons and panels as in the original script
        pausePanel = GameObject.Find("Pause Menu");
        resumeButton = GameObject.Find("Return Game Button").GetComponent<Button>();
        optionButton = GameObject.Find("Option").GetComponent<Button>();
        returnToMainMenuButton = GameObject.Find("Return To Main Menu").GetComponent<Button>();
        exitButton = GameObject.Find("Exit Game").GetComponent<Button>();

        pausePanel.SetActive(false);

        // Add listeners for button clicks
        resumeButton.onClick.AddListener(() => { PlayButtonSFX(); ResumeGame(); });
        exitButton.onClick.AddListener(() => { PlayButtonSFX(); ExitGame(); });
        graphicButton.onClick.AddListener(() => { PlayButtonSFX(); ShowGraphicPanel(); });
        audioButton.onClick.AddListener(() => { PlayButtonSFX(); ShowAudioPanel(); });
        optionButton.onClick.AddListener(() => { PlayButtonSFX(); ShowOptionPanel(); });
        backButton.onClick.AddListener(() => { PlayButtonSFX(); ShowPausePanel(); });
        backButtonOnGraphicPanel.onClick.AddListener(() => { PlayButtonSFX(); ShowOptionPanel(); });
        backButtonOnAudioPanel.onClick.AddListener(() => { PlayButtonSFX(); ShowOptionPanel(); });
        returnToMainMenuButton.onClick.AddListener(() => { PlayButtonSFX(); MainMenu(); });

        playerMovement = FindObjectOfType<Move>();

        // Setup sliders and audio settings as before
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1.0f); // Default to 75% volume
        SetMusicVolume(musicSlider.value); // Apply the volume at the start

        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1.0f); // Default to 75% volume
        SetSFXVolume(sfxSlider.value); // Apply the volume at the start

        optionPanel.SetActive(false);
        audioPanel.SetActive(false);
        graphicPanel.SetActive(false);
    }

    // Function to play button SFX
    private void PlayButtonSFX()
    {
        if (buttonClickSFX != null)
        {
            buttonClickSFX.Play();
        }
    }

    // Rest of the code remains the same, including the methods for handling game logic and panels.

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        if (playerMovement != null)
        {
            playerMovement.enabled = true; // Enable player input
        }
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowAudioPanel()
    {
        SetActivePanel(audioPanel);
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player input
        }
    }

    public void ShowGraphicPanel()
    {
        SetActivePanel(graphicPanel);
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player input
        }
    }

    public void ShowOptionPanel()
    {
        SetActivePanel(optionPanel);
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player input
        }
    }

    public void ShowPausePanel()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CloseOptionMainMenu();
            Debug.Log("Tidak dapat membuka Pause Panel di Main Menu");
            return;
        }
        SetActivePanel(pausePanel);
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player input
        }
    }
public void CloseOptionMainMenu()
    {
        optionPanel.SetActive(false);
    }
void Update()
    {
        if (pausePanel && resumeButton && optionButton && returnToMainMenuButton && exitButton == null)
        {
            pausePanel = GameObject.Find("Pause Menu");
            resumeButton = GameObject.Find("Return Game Button").GetComponent<Button>();
            optionButton = GameObject.Find("Option").GetComponent<Button>();
            returnToMainMenuButton = GameObject.Find("Main Menu").GetComponent<Button>();
            exitButton = GameObject.Find("Exit Game").GetComponent<Button>();
            pausePanel.SetActive(false);
            Debug.Log("Sedang Kosong");
        }
        if (returnToMainMenuButton && optionButton == null)
        {
            optionButton = GameObject.Find("Option").GetComponent<Button>();
            returnToMainMenuButton = GameObject.Find("Main Menu").GetComponent<Button>();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("Tidak dapat membuka Pause Panel di Main Menu");
            return;
        }
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0;  // Pause the game
        }
        else
        {
            Time.timeScale = 1;  // Resume the game
        }
    }

    private void SetActivePanel(GameObject activePanel)
    {
        pausePanel.SetActive(activePanel == pausePanel);
        graphicPanel.SetActive(activePanel == graphicPanel);
        audioPanel.SetActive(activePanel == audioPanel);
        optionPanel.SetActive(activePanel == optionPanel);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == 0)
        {
            mixer.SetFloat("Music", -80f); // Mute when slider is 0
        }
        else
        {
            mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        }

        // Save the volume setting for the next session
        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        if (volume == 0)
        {
            mixer.SetFloat("SFX", -80f); // Mute when slider is 0
        }
        else
        {
            mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }

        // Save the volume setting for the next session
        PlayerPrefs.SetFloat("SFX", volume);
        PlayerPrefs.Save();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu 1");
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
