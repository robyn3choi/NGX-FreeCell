using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script allows buttons to access GameManager functions 
// even if the GameManager is carried over from the previous scene
public class GameManagerProxy : MonoBehaviour
{
    public void Restart()
    {
        GameManager.inst.Play();
    }

    public void Win()
    {
        GameManager.inst.Win();
    }
}
