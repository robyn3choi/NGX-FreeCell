using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCell : MonoBehaviour, ICell
{
    public GameObject highlight;
    private Card cardInCell;

    public void DropCardInCell(Card card)
    {
        cardInCell = card;
        card.transform.SetParent(transform);
        card.transform.localPosition = Vector3.zero;
        StopHighlight();
    }

    public Card GetFrontCard()
    {
        return cardInCell;
    }

    public void Highlight()
    {
        highlight.SetActive(true);
    }

    public void StopHighlight()
    {
        highlight.SetActive(false);
    }

    public bool IsInCardDropDistance(Card card)
    {
        return Vector3.Distance(card.transform.position, transform.position) < Card.DROP_DISTANCE;
    }

    public bool IsPotentialCardDrop(Card card)
    {
        return cardInCell == null;
    }

    public void RemoveFrontCard()
    {
        cardInCell = null;
    }

}
