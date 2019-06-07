using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour, ICell
{
    public GameObject highlight;
    public int suit;
    private List<Card> cards = new List<Card>();

    public void DropCardInCell(Card card)
    {
        cards.Add(card);
        card.isFrontCard = true;
        card.transform.SetParent(transform);
        card.transform.localPosition = Vector3.zero;
        StopHighlight();

        if (cards.Count == 13)
        {
            GameManager.inst.CheckIfAllFoundationsComplete();
        }
    }

    public Card GetFrontCard()
    {
        if (cards.Count > 0)
        {
            return cards[cards.Count - 1];
        }
        return null;
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
        if (card.GetSuit() == suit)
        {
            if (GetFrontCard() == null && card.GetNumber() == 1)
            {
                return true;
            }
            else if (GetFrontCard() != null && card.GetNumber() == GetFrontCard().GetNumber()+1)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveFrontCard()
    {
        cards.RemoveAt(cards.Count - 1);
        Card front = GetFrontCard();
        if (front != null)
        {
            front.isFrontCard = true;
        }
    }

    public bool IsComplete()
    {
        return cards.Count == 13;
    }
}
