using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXButton : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip buttonSFX;      // Audio clip that will play when the button is pressed

    // This method is called when the button is pressed
    public void PlaySFXButton()
    {
        if (audioSource != null && buttonSFX != null)
        {
            audioSource.PlayOneShot(buttonSFX);  // Play the assigned audio clip
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned.");
        }
    }
}
