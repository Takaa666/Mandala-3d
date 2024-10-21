using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextToBissmillah : MonoBehaviour
{
    public Button buttonSkip;
    
    public void Skip()
    {
        
        SceneManager.LoadScene("BismillahFinal");

        
    }
}
