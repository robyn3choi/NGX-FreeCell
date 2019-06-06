using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    int number;
    int suit;

    public void Initialize(int number, int suit, Sprite suitSprite)
    {
        this.number = number;
        this.suit = suit;
        GetComponentInChildren<Text>().text = GetStringFromNumber(number);
        GetComponentsInChildren<Image>()[1].sprite = suitSprite;
    }

    private string GetStringFromNumber(int number)
    {
        switch (number)
        {
            case 1:
                return "A";
            case 11:
                return "J";
            case 12:
                return "Q";
            case 13:
                return "K";
            default:
                return number.ToString();
        }
    }
}
