using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cascade : MonoBehaviour, ICell
{
    public GameObject highlight;
    private List<Card> cards = new List<Card>();
    private int cascadeIndex;

    public void Initialize(int index)
    {
        cascadeIndex = index;
    }

    public bool AddInitialCard(Card card)
    {
        AddCard(card);
        // if this pile is full
        if (cascadeIndex < 4 && cards.Count == 7 ||
            cascadeIndex >= 4 && cards.Count == 6)
        {
            return false;
        }
        return true;
    }

    private void AddCard(Card card)
    {
        if (cards.Count > 0)
        {
            GetFrontCard().isFrontCard = false;
        }
        cards.Add(card);
        card.isFrontCard = true;
        card.transform.SetParent(transform);
        card.transform.localPosition =
            new Vector3(0, Card.STACK_OFFSET * (cards.Count - 1), 0);
    }

    public Card GetFrontCard()
    {
        if (cards.Count > 0)
        {
            return cards[cards.Count - 1];
        }
        return null;
    }

    public bool IsPotentialCardDrop(Card card)
    {
        Card frontCard = GetFrontCard();
        if (frontCard == null)
        {
            return true;
        }
        return AreDifferentColors(card, frontCard) &&
            card.GetNumber() == frontCard.GetNumber() - 1;
    }

    private bool AreDifferentColors(Card card1, Card card2)
    {
        return (!card1.IsRed() && card2.IsRed()) || (card1.IsRed() && !card2.IsRed());
    }

    public bool IsInCardDropDistance(Card card)
    {
        Card frontCard = GetFrontCard();
        if (frontCard == null)
        {
            return Vector3.Distance(card.transform.position, transform.position) < Card.DROP_DISTANCE;
        }
        else
        {
            Vector3 frontCardPos = frontCard.transform.position;
            frontCardPos.y += Card.STACK_OFFSET;
            return Vector3.Distance(card.transform.position, frontCardPos) < Card.DROP_DISTANCE;
        }
    }

    public void Highlight()
    {
        Card front = GetFrontCard();
        if (front != null)
        {
            GetFrontCard().Highlight();
        }
        else
        {
            highlight.SetActive(true);
        }
    }

    public void StopHighlight()
    {
        Card front = GetFrontCard();
        if (front != null)
        {
            GetFrontCard().StopHighlight();
        }
        highlight.SetActive(false);
    }

    public void DropCardInCell(Card card)
    {
        StopHighlight();
        AddCard(card);
    }

    public void RemoveFrontCard()
    {
        cards.RemoveAt(cards.Count - 1);
        Card newFront = GetFrontCard();
        if (newFront != null)
        {
            newFront.isFrontCard = true;
        }
    }
}
