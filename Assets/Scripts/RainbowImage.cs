using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowImage : MonoBehaviour
{
    private Image image;
    private Color[] colors = new Color[] {
        Color.red,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.magenta
    };
    private int colorIndex = 0;
    private float timer = 0;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.red;
    }

    private void Update()
    {
        int nextColorIndex = colorIndex == 4 ? 0 : colorIndex + 1;
        image.color = Color.Lerp(colors[colorIndex], colors[nextColorIndex], timer);
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            if (colorIndex == 4)
            {
                colorIndex = 0;
            }
            else
            {
                colorIndex++;
            }
            timer = 0;
        }
    }
}
