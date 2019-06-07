using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static float STACK_OFFSET = -26;
    public static float DROP_DISTANCE = 35;

    public GameObject highlight;
    public Image suitImage;
    public bool isFrontCard;

    private int number;
    private int suit;
    private Transform canvas;
    private bool hasDragged = false;
    private bool hasStartedClick = false;

    public void Initialize(int number, int suit, Sprite suitSprite, Transform canvas)
    {
        this.number = number;
        this.suit = suit;
        GetComponentInChildren<Text>().text = GetStringFromNumber(number);
        suitImage.sprite = suitSprite;
        this.canvas = canvas;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        hasDragged = true;
        if (isFrontCard)
        {
            transform.SetParent(canvas);
            CardManager.inst.StartCardDrag(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isFrontCard)
        {
            transform.position = Input.mousePosition;
            CardManager.inst.CardDrag(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        hasDragged = false;
        if (isFrontCard)
        {
            CardManager.inst.EndCardDrag(this);
        }
    }

    public void Highlight()
    {
        highlight.SetActive(true);
    }

    public void StopHighlight()
    {
        highlight.SetActive(false);
    }

    public bool IsRed()
    {
        return suit == 0 || suit == 2;
    }

    public int GetSuit()
    {
        return suit;
    }

    public int GetNumber()
    {
        return number;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        hasStartedClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (hasStartedClick && !hasDragged && isFrontCard)
        {
            CardManager.inst.TryAddCardToFreeCell(this);
        }
    }
}
