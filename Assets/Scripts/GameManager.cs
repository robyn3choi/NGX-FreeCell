using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<Foundation> foundations;

    public static GameManager inst = null;
    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        SceneManager.LoadScene("Play");
    }

    public void SetFoundations(Foundation[] foundations)
    {
        this.foundations = new List<Foundation>(foundations);
    }

    public void CheckIfAllFoundationsComplete()
    {
        bool areAllComplete = true;
        foreach (Foundation foundation in foundations)
        {
            if (!foundation.IsComplete())
            {
                areAllComplete = false;
                break;
            }
        }
        if (areAllComplete)
        {
            Win();
        }
    }

    public void Win()
    {
        SceneManager.LoadScene("End");
    }
}
