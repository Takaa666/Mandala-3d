using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSound : MonoBehaviour
{
    public List<AudioClip> walkSounds;
    public AudioSource audioSource;

    public int pos;
    public void playRunSound()
    {
        pos = (int)Mathf.Floor(Random.Range(0, walkSounds.Count));
        audioSource.PlayOneShot(walkSounds[pos]);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
