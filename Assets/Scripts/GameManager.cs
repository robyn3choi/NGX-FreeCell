using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<Foundation> foundations;

    public static GameManager inst = null;
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foundations = new List<Foundation>(FindObjectsOfType<Foundation>());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            print("YOU WIN");
        }
    }
}
