using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject target;
    public Collider triggerObject;
    private void Start(){
            triggerObject.enabled = false;
    }

    private void Update(){
        if(target == null){
            triggerObject.enabled = true;

        }
    }
    void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah objek yang bertabrakan memiliki tag "Player"
        if (other.CompareTag("Player"))
        {
            // Mengganti scene ke "Cutscene"
            SceneManager.LoadScene("Cutscene");
        }
    }
}
