using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class FlavorText
{
    public List<string> welcomeTexts;
    public List<string> endTexts;
}

public class FlavorTextLoader : MonoBehaviour
{
    private string flavorTextJson = "data.json";
    private FlavorText flavorText;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);

        string filePath = Path.Combine(Application.streamingAssetsPath, flavorTextJson);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            flavorText = JsonUtility.FromJson<FlavorText>(dataAsJson);
        }
        else
        {
            print("no flavor text data!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Welcome")
        {
            GameObject welcomeTextGO = GameObject.FindGameObjectWithTag("FlavorText");
            Text welcomeText = welcomeTextGO.GetComponent<Text>();
            welcomeText.text = GetRandomWelcomeText();
        }
        else if (scene.name == "End")
        {
            GameObject endTextGO = GameObject.FindGameObjectWithTag("FlavorText");
            Text endText = endTextGO.GetComponent<Text>();
            endText.text = GetRandomEndText();
        }
    }

    private string GetRandomWelcomeText()
    {
        return GetRandomStringFromList(flavorText.welcomeTexts);
    }

    private string GetRandomEndText()
    {
        return GetRandomStringFromList(flavorText.endTexts);
    }

    private string GetRandomStringFromList(List<string> list)
    {
        int index = Random.Range(0, list.Count);
        return list[index];
    }
}